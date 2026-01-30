# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

InformationBox is a Windows Forms library providing a customizable alternative to `MessageBox`. It's published as a NuGet package supporting .NET 4.8 and .NET 8/9/10.

## Build Commands

**IMPORTANT:** Always use the full MSBuild path when building this project. MSBuild is located at:
`P:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\msbuild.exe`

Since the solution targets both .NET Framework 4.8 and .NET Core, use MSBuild instead of dotnet:

```bash
# Build all projects
"P:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\msbuild.exe" InfoBox.sln

# Build with specific configuration
"P:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\msbuild.exe" InfoBox.sln -p:Configuration=Release

# Rebuild all (clean + build)
"P:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\msbuild.exe" InfoBox.sln -t:Rebuild

# Run tests
dotnet test

# Pack NuGet package
nuget pack InfoBox/InfoBox.nuspec
```

## Project Structure

The solution uses a dual-build strategy with shared source code:

- **InfoBox/** - Legacy .NET Framework 4.8 library (old .csproj format)
- **InfoBoxCore/** - Modern .NET 8/9/10 library that compiles the same source via `<Compile Include="..\InfoBox\**\*.cs" />`
- **InfoBox.Designer/** - Visual designer tool (.NET Framework 4.8)
- **InfoBoxCore.Designer/** - Visual designer tool (.NET 8/9/10)
- **InfoBoxCore.Designer.Tests/** - NUnit tests for code generation
- **InfoBoxCore.Tests/** - NUnit tests for ViewModel and parameter parsing logic

Both InfoBox and InfoBoxCore compile to the same assembly name (`InfoBox.dll`) and namespace (`InfoBox`).

### Adding New Source Files
**InfoBox/** uses legacy .csproj format — new `.cs` files must be manually added as `<Compile Include="..." />` entries. InfoBoxCore auto-includes them via wildcard `<Compile Include="..\InfoBox\**\*.cs" />`.

## Key Architecture

### Flexible Parameter Pattern
The main API `InformationBox.Show()` accepts parameters in any order via `params object[]`. Parameter types are detected at runtime to configure the dialog:
- `string` - title, help file, help topic (in order)
- `InformationBoxButtons`, `InformationBoxIcon`, etc. - enum-based configuration
- `AutoCloseParameters`, `DesignParameters` - complex configuration objects

### Core Files
- `InfoBox/InformationBox.cs` - Static entry point with `Show()` methods
- `InfoBox/Form/InformationBoxForm.cs` - Internal form implementation
- `InfoBox/Controls/` - Custom WinForms controls with glass styling
- `InfoBox/Enums/` - Configuration enums (buttons, icons, position, etc.)
- `InfoBox/Context/InformationBoxScope.cs` - Scope-based default configuration

### ViewModel Architecture
- `InfoBox/Internals/InformationBoxViewModel.cs` - Configuration state and business logic (testable without WinForms)
- `InfoBox/Internals/ParameterParser.cs` - Parses `params object[]` and named parameters into a ViewModel
- `InfoBox/Internals/ViewModelTypes.cs` - DTOs returned by ViewModel methods (`ButtonDefinition`, `CheckBoxConfiguration`, etc.)
- `InfoBox/Internals/ITextMeasurer.cs` / `IScreenProvider.cs` - Abstractions for testability

### Code Generation
The Designer tool generates C# or VB.NET code:
- `InfoBox.Designer/CodeGeneration/CSharpGenerator.cs`
- `InfoBox.Designer/CodeGeneration/VbNetGenerator.cs`

Tests use Roslyn to compile and validate generated code.

## Localization

Resources are in `InfoBox/Properties/Resources.*.resx` with support for: English, German, Spanish, French, Portuguese, Arabic, Farsi, Dutch.

## Gotchas

- `params object[]` and `string[]`: Passing `string[]` to a `params object[]` method expands each string as a separate parameter. Cast to `(object)` to pass the array as a single element.
- `DesignParameters` constructor order is `(formBackColor, barsBackColor)` — form color first, bars color second.
