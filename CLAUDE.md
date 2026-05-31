# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

InformationBox is a Windows Forms library providing a customizable alternative to `MessageBox`. It's published as a NuGet package supporting .NET 4.8 and .NET 8/9/10.

## Build Commands

The solution multi-targets .NET Framework 4.8 and .NET 8/9/10. Since the InfoBoxCore consolidation (net48 folded into the SDK-style `InfoBoxCore.csproj`), **`dotnet` is the primary build tool** and works for the whole solution:

```bash
# Build all projects
dotnet build InfoBox.sln -c Release

# Rebuild all (force a full, non-incremental build)
dotnet build InfoBox.sln -c Release --no-incremental

# Run all tests (both InfoBoxCore.Tests and InfoBoxCore.Designer.Tests)
dotnet test InfoBox.sln -c Release

# Pack the NuGet package (multi-target; pulls all 4 TFMs from InfoBoxCore/bin/Release/)
nuget pack InfoBox/InfoBox.nuspec
```

CI is the dotnet-based pipeline in `azure-pipelines.yml` (no Visual Studio dependency).

VS MSBuild still works if you specifically need it (it is **not** required):
`"P:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\msbuild.exe" InfoBox.sln -p:Configuration=Release`

## Project Structure

The library is built from a single multi-targeted project over shared source:

- **InfoBox/** - Shared library *source* (`.cs`, `.resx`, icon resources, signing key `key.snk`, and `InfoBox.nuspec`). This folder is **no longer a project** - it has no `.csproj`. Its files are compiled by InfoBoxCore.
- **InfoBoxCore/** - The single library project (SDK-style). Multi-targets `net48;net8.0-windows;net9.0-windows;net10.0-windows`, compiling the shared source via `<Compile Include="..\InfoBox\**\*.cs" />`. Produces the strong-name-signed `InfoBox.dll` (namespace `InfoBox`) for every target framework, including the localization satellite assemblies. The net48 target sets `GenerateResourceUsePreserializedResources=true` + references `System.Resources.Extensions` for the embedded icon resources.
- **InfoBox.Designer/** - Shared designer *source* (`.cs`, `.resx`, `app.ico`). Like InfoBox/, this folder is **no longer a project** - it has no `.csproj`. Its files are compiled by InfoBoxCore.Designer.
- **InfoBoxCore.Designer/** - The single designer project (.NET 8/9/10), compiling the designer source via `<Compile Include="..\InfoBox.Designer\**\*.cs" />`. The designer is a dev tool and is not shipped in the NuGet package, so it does not target net48.
- **InfoBoxCore.Designer.Tests/** - NUnit tests for code generation (Roslyn-compiles the generated code).
- **InfoBoxCore.Tests/** - NUnit tests for the library (params parser, scope lifecycle, text helpers).

The NuGet package (`InfoBox.nuspec`) pulls all four target frameworks from `InfoBoxCore/bin/Release/<tfm>/`.

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

### Code Generation
The Designer tool generates C# or VB.NET code:
- `InfoBox.Designer/CodeGeneration/CSharpGenerator.cs`
- `InfoBox.Designer/CodeGeneration/VbNetGenerator.cs`

Tests use Roslyn to compile and validate generated code.

## Localization

Resources are in `InfoBox/Properties/Resources.*.resx` with support for: English, German, Spanish, French, Portuguese, Arabic, Farsi, Dutch.
