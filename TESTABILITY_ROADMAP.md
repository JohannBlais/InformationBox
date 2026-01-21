# Testability Improvements Roadmap

## Overview

The InformationBox codebase dates from 2007 with tightly-coupled Windows Forms architecture. Current testability score: **1.7/10**. Only existing tests cover code generation in the Designer tool.

This document outlines a phased approach to improve testability while maintaining backward compatibility.

---

## Current Testability Challenges

1. **Windows Forms Coupling**: `InformationBoxForm` inherits from `Form`, requires UI thread, window handles, and graphics resources
2. **Static State**: `InformationBoxScope.Current` uses static `Stack<InformationBoxScope>` (not thread-safe)
3. **Hardcoded Dependencies**: Direct usage of `SystemFonts`, `SystemSounds`, `CreateGraphics()`, `Screen.PrimaryScreen`
4. **Complex Initialization**: 33-parameter constructor with runtime type detection on `params object[]`
5. **Layout Calculation**: 110-line `SetLayout()` method with graphics-dependent calculations (lines 999-1110)
6. **Event-Driven Logic**: Button clicks, timer events (115-line auto-close method, lines 1677-1791) hard to test
7. **No Abstraction Layers**: Direct instantiation of all dependencies

---

## Priority 0 (High Impact, Medium Effort) - Foundation

### P0.1: Extract Presenter Logic from InformationBoxForm

**Goal**: Separate business logic from UI to enable pure unit testing.

**Status**: ⏳ Not Started

**Changes**:
- Create `InformationBoxModel.cs` - pure data class with all configuration
- Create `InformationBoxPresenter.cs` - testable business logic class
- Extract methods from `InformationBoxForm.cs`:
  - `CalculateLayout()` - extract lines 999-1110 from `SetLayout()`
  - `GetButtons()` - extract lines 1323-1408 from `SetButtons()`
  - `UpdateAutoClose()` - extract lines 1677-1791 from `TmrAutoClose_Tick()`
  - `FormatText()` - extract text measurement and regex splitting logic

**Files to create**:
```
InfoBox/Presentation/InformationBoxModel.cs
InfoBox/Presentation/InformationBoxPresenter.cs
InfoBox/Presentation/LayoutCalculation.cs
InfoBox/Presentation/ButtonDefinition.cs
InfoBox/Presentation/AutoCloseState.cs
```

**Code Example**:
```csharp
// Model: Pure data class
public class InformationBoxModel
{
    public string Text { get; set; }
    public string Title { get; set; }
    public InformationBoxButtons Buttons { get; set; }
    public InformationBoxIcon Icon { get; set; }
    public AutoCloseParameters AutoClose { get; set; }
    // ... all configuration

    // Testable validation
    public ValidationResult Validate() { ... }
}

// Presenter: Testable business logic
public class InformationBoxPresenter
{
    private readonly InformationBoxModel _model;
    private readonly ITextMeasurement _textMeasurement;

    public InformationBoxPresenter(InformationBoxModel model, ITextMeasurement textMeasurement)
    {
        _model = model;
        _textMeasurement = textMeasurement;
    }

    // Extract complex layout logic - now fully testable!
    public LayoutCalculation CalculateLayout(int maxWidth, int maxHeight)
    {
        // Lines 999-1110 from SetLayout() extracted here
        // No WinForms dependencies, pure calculation
        var result = new LayoutCalculation();

        var textSize = _textMeasurement.MeasureString(_model.Text, _model.Font, maxWidth);
        result.TextHeight = (int)textSize.Height;
        result.RequiredWidth = (int)textSize.Width + 50; // margins

        // ... all layout calculations without touching controls
        return result;
    }

    // Extract button generation logic - testable!
    public List<ButtonDefinition> GetButtons()
    {
        // Lines 1323-1408 logic extracted
        var buttons = new List<ButtonDefinition>();

        if (_model.Buttons.HasFlag(InformationBoxButtons.OK))
            buttons.Add(new ButtonDefinition("OK", InformationBoxResult.OK, isDefault: true));

        // ... button logic
        return buttons;
    }

    // Extract auto-close logic - testable!
    public AutoCloseState UpdateAutoClose(TimeSpan elapsed)
    {
        // Lines 1677-1791 timer logic extracted - no Timer dependency!
        // Pure function: given elapsed time, return new state
        return new AutoCloseState {
            RemainingSeconds = ...,
            DefaultButton = ...,
            ShouldClose = ...
        };
    }
}

// View: Thin WinForms wrapper
public partial class InformationBoxForm : Form
{
    private readonly InformationBoxPresenter _presenter;

    public void ApplyLayout(LayoutCalculation layout)
    {
        // Apply calculated values to controls
        this.Width = layout.RequiredWidth;
        this.Height = layout.RequiredHeight;
        this.messageText.Size = new Size(layout.TextWidth, layout.TextHeight);
    }
}
```

**Benefits**:
- Pure unit tests for layout, button, and auto-close logic
- No WinForms dependencies in business logic
- Testable without UI thread or graphics resources

---

### P0.2: Introduce ITextMeasurement Interface

**Goal**: Abstract graphics-dependent text measurement to enable headless testing.

**Status**: ⏳ Not Started

**Changes**:
```csharp
// New interface
public interface ITextMeasurement
{
    SizeF MeasureString(string text, Font font, int width);
    int GetLineHeight(Font font);
}

// Production implementation
public class GraphicsTextMeasurement : ITextMeasurement
{
    private readonly Graphics _graphics;

    public GraphicsTextMeasurement(Graphics graphics)
    {
        _graphics = graphics;
    }

    public SizeF MeasureString(string text, Font font, int width)
    {
        return _graphics.MeasureString(text, font, width);
    }

    public int GetLineHeight(Font font)
    {
        return (int)Math.Ceiling(_graphics.MeasureString("X", font).Height);
    }
}

// Test implementation
public class MockTextMeasurement : ITextMeasurement
{
    private readonly Dictionary<string, SizeF> _measurements = new();

    public void SetMeasuredSize(string text, SizeF size)
    {
        _measurements[text] = size;
    }

    public SizeF MeasureString(string text, Font font, int width)
    {
        return _measurements.TryGetValue(text, out var size) ? size : new SizeF(100, 20);
    }

    public int GetLineHeight(Font font)
    {
        return 20; // Fixed height for testing
    }
}
```

**Files to create**:
```
InfoBox/Abstractions/ITextMeasurement.cs
InfoBox/Implementation/GraphicsTextMeasurement.cs
InfoBox.Tests/Mocks/MockTextMeasurement.cs (new test project)
```

**Files to modify**:
- `InfoBox/Form/InformationBoxForm.cs` - inject `ITextMeasurement` instead of using `measureGraphics` directly (line 291)
- `InformationBoxPresenter.cs` - accept `ITextMeasurement` in constructor

**Benefits**:
- Tests can run without graphics context
- Predictable, deterministic text sizing in tests
- Enables CI/CD on headless servers

---

## Priority 1 (High Impact, Low Effort) - Quick Wins

### P1.1: Replace Static Scope with AsyncLocal

**Goal**: Make `InformationBoxScope` thread-safe for concurrent test execution.

**Status**: ⏳ Not Started

**Changes**:
```csharp
// In InfoBox/Context/InformationBoxScope.cs
private static readonly AsyncLocal<Stack<InformationBoxScope>> _scopeStack
    = new AsyncLocal<Stack<InformationBoxScope>>();

private static Stack<InformationBoxScope> ScopeStack
{
    get
    {
        if (_scopeStack.Value == null)
            _scopeStack.Value = new Stack<InformationBoxScope>();
        return _scopeStack.Value;
    }
}

// Add test hook
internal static Func<IInformationBoxScope> TestScopeProvider { get; set; }

public static IInformationBoxScope Current
{
    get
    {
        if (TestScopeProvider != null)
            return TestScopeProvider();
        return ScopeStack.Count > 0 ? ScopeStack.Peek() : null;
    }
}
```

**Files to modify**:
- `InfoBox/Context/InformationBoxScope.cs`

**Files to create**:
- `InfoBox/Abstractions/IInformationBoxScope.cs`

**Benefits**:
- Tests can run in parallel without interference
- Scopes isolated per async context
- Testable via `TestScopeProvider` injection

---

### P1.2: Add Factory Pattern for Form Creation

**Goal**: Enable mocking of form instantiation in tests.

**Status**: ⏳ Not Started

**Changes**:
```csharp
// New interface
public interface IInformationBoxFactory
{
    IInformationBoxDisplay Create(string text, params object[] parameters);
}

// New interface for form
public interface IInformationBoxDisplay
{
    InformationBoxResult ShowModal();
    void ShowModeless();
    InformationBoxResult Result { get; }
    event EventHandler Closed;
}

// Production factory
public class InformationBoxFactory : IInformationBoxFactory
{
    public IInformationBoxDisplay Create(string text, params object[] parameters)
    {
        return new InformationBoxForm(text, parameters);
    }
}

// Refactor static API (backward compatible)
public static class InformationBox
{
    // Internal for testing - allows injection of mock factory
    internal static IInformationBoxFactory Factory { get; set; }
        = new InformationBoxFactory();

    public static InformationBoxResult Show(string text, params object[] parameters)
    {
        var display = Factory.Create(text, parameters);
        return display.ShowModal();
    }
}
```

**Files to create**:
```
InfoBox/Abstractions/IInformationBoxFactory.cs
InfoBox/Abstractions/IInformationBoxDisplay.cs
InfoBox/Implementation/InformationBoxFactory.cs
```

**Files to modify**:
- `InfoBox/InformationBox.cs` - add Factory property and use it (lines 150-424)
- `InfoBox/Form/InformationBoxForm.cs` - implement `IInformationBoxDisplay`

**Benefits**:
- Code that calls `InformationBox.Show()` can be tested with mock factory
- No breaking changes to public API
- Simple dependency injection point

---

### P1.3: Add ISystemResources Interface

**Goal**: Abstract system dependencies (fonts, sounds, screen metrics).

**Status**: ⏳ Not Started

**Changes**:
```csharp
public interface ISystemResources
{
    Font GetMessageBoxFont();
    void PlaySound(InformationBoxSound sound);
    Rectangle GetWorkingArea();
    Rectangle GetWorkingArea(Form form); // For multi-monitor support
}

public class WindowsSystemResources : ISystemResources
{
    public Font GetMessageBoxFont() => SystemFonts.MessageBoxFont;

    public void PlaySound(InformationBoxSound sound)
    {
        // Lines 654-662 logic from InformationBoxForm.PlaySound()
        // Note: Using traditional switch statement for .NET 4.8 compatibility
        SystemSound systemSound;
        switch (sound)
        {
            case InformationBoxSound.Beep:
                systemSound = SystemSounds.Beep;
                break;
            case InformationBoxSound.Asterisk:
                systemSound = SystemSounds.Asterisk;
                break;
            case InformationBoxSound.Exclamation:
                systemSound = SystemSounds.Exclamation;
                break;
            case InformationBoxSound.Hand:
                systemSound = SystemSounds.Hand;
                break;
            case InformationBoxSound.Question:
                systemSound = SystemSounds.Question;
                break;
            default:
                systemSound = null;
                break;
        }

        if (systemSound != null)
        {
            systemSound.Play();
        }
    }

    public Rectangle GetWorkingArea() => Screen.PrimaryScreen.WorkingArea;

    public Rectangle GetWorkingArea(Form form) => Screen.FromControl(form).WorkingArea;
}

// Test mock
public class MockSystemResources : ISystemResources
{
    public Font MessageBoxFont { get; set; } = new Font("Arial", 10);
    public Rectangle WorkingArea { get; set; } = new Rectangle(0, 0, 1920, 1080);
    public List<InformationBoxSound> PlayedSounds { get; } = new List<InformationBoxSound>();

    public Font GetMessageBoxFont() => MessageBoxFont;

    public void PlaySound(InformationBoxSound sound) => PlayedSounds.Add(sound);

    public Rectangle GetWorkingArea() => WorkingArea;

    public Rectangle GetWorkingArea(Form form) => WorkingArea;
}
```

**Files to create**:
```
InfoBox/Abstractions/ISystemResources.cs
InfoBox/Implementation/WindowsSystemResources.cs
InfoBox.Tests/Mocks/MockSystemResources.cs
```

**Files to modify**:
- `InfoBox/Form/InformationBoxForm.cs` - inject `ISystemResources`:
  - Line 295-296: Replace `SystemFonts.MessageBoxFont` with `_systemResources.GetMessageBoxFont()`
  - Lines 654-662: Replace `PlaySound()` method with `_systemResources.PlaySound()`
  - Line 1023: Replace `Screen.PrimaryScreen.WorkingArea` with `_systemResources.GetWorkingArea()`

**Benefits**:
- Tests don't require system fonts installed
- No audio playback during tests
- Mock screen dimensions for layout tests

---

## Priority 2 (Medium Impact, Medium Effort) - Enhanced Testing

### P2.1: Add Async/Await API for Modeless Dialogs

**Goal**: Modern async pattern for testable modeless behavior.

**Status**: ⏳ Not Started

**Changes**:
```csharp
public static class InformationBox
{
    public static Task<InformationBoxResult> ShowAsync(string text, params object[] parameters)
    {
        var tcs = new TaskCompletionSource<InformationBoxResult>();

        // Ensure we're on UI thread for WinForms
        if (Application.OpenForms.Count > 0)
        {
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                var display = Factory.Create(text, parameters);
                display.Closed += (s, e) => tcs.SetResult(display.Result);
                display.ShowModeless();
            }));
        }
        else
        {
            var display = Factory.Create(text, parameters);
            display.Closed += (s, e) => tcs.SetResult(display.Result);
            display.ShowModeless();
        }

        return tcs.Task;
    }
}
```

**Files to modify**:
- `InfoBox/InformationBox.cs` - add `ShowAsync()` method
- `InfoBox/Form/InformationBoxForm.cs` - ensure `Closed` event fires correctly

**Benefits**:
- Tests can await modeless dialogs
- Modern async/await pattern
- No callback complexity in tests

---

### P2.2: Set Up FlaUI Integration Tests

**Goal**: End-to-end UI automation testing for real form behavior.

**Status**: ⏳ Not Started

**Steps**:
1. Create new test project: `InfoBox.IntegrationTests`
2. Add NuGet packages: `FlaUI.Core`, `FlaUI.UIA3`, `NUnit`
3. Create test helper: `InfoBoxTestHelper.cs` for launching forms on STA thread
4. Write integration tests:
   - Button click tests
   - Keyboard navigation (Enter, Escape, Tab)
   - Auto-close timer behavior
   - Layout verification with different text lengths
   - Icon display verification

**Files to create**:
```
InfoBox.IntegrationTests/InfoBox.IntegrationTests.csproj
InfoBox.IntegrationTests/Helpers/InfoBoxTestHelper.cs
InfoBox.IntegrationTests/BasicTests.cs
InfoBox.IntegrationTests/ButtonTests.cs
InfoBox.IntegrationTests/AutoCloseTests.cs
InfoBox.IntegrationTests/LayoutTests.cs
```

**Example test**:
```csharp
[Test]
[Apartment(ApartmentState.STA)]
public void InformationBox_ClickOK_ReturnsOKResult()
{
    InformationBoxResult result = InformationBoxResult.None;

    var formTask = Task.Run(() =>
    {
        result = InformationBox.Show(
            "Test message",
            InformationBoxButtons.OKCancel);
    });

    Thread.Sleep(500); // Wait for form to appear

    using (var automation = new UIA3Automation())
    {
        var window = automation.GetDesktop()
            .FindFirstDescendant(cf => cf.ByControlType(ControlType.Window))
            ?.AsWindow();

        var okButton = window.FindFirstDescendant(cf => cf.ByText("OK"))?.AsButton();
        okButton.Click();
    }

    formTask.Wait();
    Assert.AreEqual(InformationBoxResult.OK, result);
}
```

**Benefits**:
- Tests real UI behavior (not just logic)
- Catches rendering/layout bugs
- Validates keyboard/mouse interaction
- Can run in CI/CD with display server

---

### P2.3: Create Unit Test Project Structure

**Goal**: Establish comprehensive unit test coverage for presenter logic.

**Status**: ⏳ Not Started

**Structure**:
```
InfoBox.Tests/
  ├── InfoBox.Tests.csproj
  ├── Mocks/
  │   ├── MockTextMeasurement.cs
  │   ├── MockSystemResources.cs
  │   └── MockInformationBoxScope.cs
  ├── Presentation/
  │   ├── InformationBoxPresenterTests.cs
  │   ├── LayoutCalculationTests.cs
  │   ├── ButtonGenerationTests.cs
  │   └── AutoCloseLogicTests.cs
  ├── Scope/
  │   └── InformationBoxScopeTests.cs
  └── ParameterParsing/
      └── ParameterDetectionTests.cs
```

**Test coverage targets**:
- Layout calculations with various text lengths (short, medium, long, multi-line)
- Button generation for all `InformationBoxButtons` enum combinations
- Auto-close countdown logic (tick-by-tick verification)
- Parameter parsing for all supported types
- Scope inheritance and parameter merging

**Example test**:
```csharp
[Test]
public void Presenter_CalculateLayout_LongText_ProducesExpectedDimensions()
{
    var model = new InformationBoxModel
    {
        Text = "Very long text that spans multiple lines and requires careful layout calculation",
        Buttons = InformationBoxButtons.OKCancel,
        Icon = InformationBoxIcon.Information
    };

    var mockTextMeasurement = new MockTextMeasurement();
    mockTextMeasurement.SetMeasuredSize(model.Text, new SizeF(400, 80));

    var presenter = new InformationBoxPresenter(model, mockTextMeasurement);
    var layout = presenter.CalculateLayout(maxWidth: 500, maxHeight: 600);

    Assert.AreEqual(450, layout.RequiredWidth);
    Assert.AreEqual(220, layout.RequiredHeight);
    Assert.AreEqual(400, layout.TextWidth);
    Assert.AreEqual(80, layout.TextHeight);
}
```

---

## Priority 3 (Complete Architecture, High Effort) - Long-term

### P3.1: Full Model-View-Presenter (MVP) Refactoring

**Goal**: Complete separation of concerns with thin view layer.

**Status**: ⏳ Not Started

**Architecture**:
```
Model (pure data)
  ↓
Presenter (business logic, testable)
  ↓
View Interface (IInformationBoxView)
  ↓
InformationBoxForm (thin WinForms wrapper)
```

**View interface**:
```csharp
public interface IInformationBoxView
{
    // Data binding
    string Text { get; set; }
    string Title { get; set; }
    Font Font { get; set; }

    // Layout
    void SetSize(int width, int height);
    void SetTextAreaSize(int width, int height);
    void SetPosition(Point location);

    // Components
    void AddButton(ButtonDefinition button);
    void SetIcon(Icon icon);
    void ShowCheckBox(string text, bool isChecked);

    // Behavior
    void Close(InformationBoxResult result);
    void StartAutoCloseTimer(int milliseconds);
    void UpdateButtonText(string buttonName, string newText);

    // Events
    event EventHandler<ButtonClickedEventArgs> ButtonClicked;
    event EventHandler<KeyEventArgs> KeyPressed;
    event EventHandler Load;
}
```

**Benefits**:
- 100% testable business logic
- View is pure data binding (no logic)
- Can create mock view for fast tests
- Can swap view implementation (e.g., WPF, Avalonia)

**Effort**: High - requires restructuring 1,796 lines of `InformationBoxForm.cs`

---

### P3.2: Parameter Builder Pattern

**Goal**: Replace `params object[]` with type-safe fluent API (optional, backward compatible).

**Status**: ⏳ Not Started

**Design**:
```csharp
public class InformationBoxParameters
{
    public static InformationBoxParameters Create(string text)
        => new InformationBoxParameters { Text = text };

    public string Text { get; private set; }
    public string Title { get; private set; }
    public InformationBoxButtons Buttons { get; private set; } = InformationBoxButtons.OK;

    public InformationBoxParameters WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public InformationBoxParameters WithButtons(InformationBoxButtons buttons)
    {
        Buttons = buttons;
        return this;
    }

    public InformationBoxParameters WithIcon(InformationBoxIcon icon)
    {
        Icon = icon;
        return this;
    }

    public InformationBoxParameters WithAutoClose(int milliseconds)
    {
        AutoClose = new AutoCloseParameters(milliseconds);
        return this;
    }

    // ... all parameters with fluent API
}

// Usage (backward compatible - keep existing API)
var result = InformationBox.Show(
    InformationBoxParameters.Create("Save changes?")
        .WithTitle("Confirmation")
        .WithButtons(InformationBoxButtons.YesNoCancel)
        .WithIcon(InformationBoxIcon.Question)
        .WithAutoClose(5000)
);
```

**Benefits**:
- Type-safe parameter construction
- IntelliSense discoverable
- Testable parameter validation
- Keeps existing `params object[]` API for backward compatibility

---

## Implementation Phases

### Phase 1 (Weeks 1-2): P0 - Foundation
- [ ] P0.1: Extract presenter logic
- [ ] P0.2: Add `ITextMeasurement` interface
- [ ] Create first unit test project

**Expected Outcome**: Testability 4/10
- Layout, button, auto-close logic unit tested
- Headless testing possible

---

### Phase 2 (Weeks 3-4): P1 - Quick Wins
- [ ] P1.1: Thread-safe scope with `AsyncLocal`
- [ ] P1.2: Factory pattern
- [ ] P1.3: `ISystemResources` interface

**Expected Outcome**: Testability 6/10
- Thread-safe, mockable dependencies
- Code calling `InformationBox.Show()` testable

---

### Phase 3 (Weeks 5-8): P2 - Enhanced Testing
- [ ] P2.1: Async/await API
- [ ] P2.2: FlaUI integration tests
- [ ] P2.3: Comprehensive unit test coverage

**Expected Outcome**: Testability 8/10
- Integration tests cover real UI behavior
- Async API tested
- 60%+ code coverage

---

### Phase 4 (Months 3-6): P3 - Architecture (Optional)
- [ ] P3.1: Full MVP refactoring
- [ ] P3.2: Builder pattern API

**Expected Outcome**: Testability 9/10
- Full MVP separation
- 80%+ code coverage
- Type-safe builder API

---

## Testing Tools Recommendations

### Unit Testing
- **NUnit** (already used in Designer tests)
- **Moq** for mocking
- **FluentAssertions** for readable assertions

### Integration Testing
- **FlaUI** (modern UI automation for Windows)
- Alternative: TestStack.White (older, less maintained)

### CI/CD
- GitHub Actions with Windows runners (for UI tests)
- xUnit for parallel test execution (optional migration from NUnit)

### Code Coverage
- **Coverlet** for .NET Core coverage
- **ReportGenerator** for HTML reports
- Target: 80%+ coverage for presenter logic

---

## Backward Compatibility Notes

All improvements should maintain backward compatibility:
- ✅ Keep existing `InformationBox.Show()` signatures
- ✅ Internal refactoring only
- ✅ New interfaces have default implementations
- ✅ Factory pattern uses default factory if not set
- ✅ No breaking changes to public API

### .NET 4.8 Compatibility

All proposed code changes are compatible with .NET Framework 4.8:
- ✅ **AsyncLocal<T>**: Available since .NET 4.6
- ✅ **Task/async-await**: Available since .NET 4.5
- ✅ **Expression-bodied members**: C# 6.0 feature, works with .NET 4.8
- ✅ **Null-conditional operators** (`?.`): C# 6.0 feature, works with .NET 4.8
- ✅ **Lambda expressions**: Available since .NET 3.5
- ⚠️ **Avoid C# 8.0+ features**: No switch expressions, use traditional switch statements
- 📝 **See NET48_COMPATIBILITY_REVIEW.md** for detailed compatibility analysis

**Recommended project settings for dual-targeting**:
```xml
<TargetFrameworks>net48;net8.0-windows</TargetFrameworks>
<LangVersion>7.3</LangVersion>  <!-- Maximum safe version for .NET 4.8 -->
```

---

## Success Metrics

| Phase | Testability Score | Key Achievements |
|-------|-------------------|------------------|
| **Current** | 1.7/10 | Only Designer code generation tested |
| **After P0** | 4/10 | Layout, button, auto-close logic unit tested; headless testing possible |
| **After P1** | 6/10 | Thread-safe, mockable dependencies; code calling `InformationBox.Show()` testable |
| **After P2** | 8/10 | Integration tests cover real UI behavior; 60%+ code coverage |
| **After P3** | 9/10 | Full MVP separation; 80%+ code coverage; type-safe builder API |

---

## Notes

- This roadmap was created on 2026-01-21 based on codebase analysis
- All changes should be implemented incrementally
- Each phase should include updating documentation
- Consider creating feature branches for each priority level
- Run existing Designer tests after each change to ensure no regressions
