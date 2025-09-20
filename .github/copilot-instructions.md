# Irihi.Avalonia.Shared

Irihi.Avalonia.Shared is a .NET utility library for the Avalonia UI framework shared by both IRIHI opensource and commercial products. The library provides common controls, helpers, converters, and markup extensions for Avalonia applications.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

Bootstrap, build, and test the repository:
- `dotnet restore` -- takes ~1-22 seconds (depends on cache). NEVER CANCEL. Set timeout to 60+ seconds.
- `dotnet build --no-restore` -- takes ~9-13 seconds. NEVER CANCEL. Set timeout to 60+ seconds.
- `dotnet test --no-build --verbosity normal` -- takes ~5 seconds with 186 tests. NEVER CANCEL. Set timeout to 60+ seconds.

Build and pack for distribution:
- ALWAYS run the bootstrapping steps first (restore, build, test).
- `dotnet pack ./src/Irihi.Avalonia.Shared -o ./nugets` -- takes ~1-2 seconds.
- `dotnet pack ./src/Irihi.Avalonia.Shared.Contracts -o ./nugets` -- takes ~1-2 seconds.

Clean build (start fresh):
- `dotnet clean` -- removes all build artifacts
- `rm -rf nugets/` -- removes any packed NuGet files

## Validation

- ALWAYS run through the complete build, test, and pack sequence after making changes.
- CRITICAL: The sample application CANNOT run in headless environments - this is expected behavior for Avalonia desktop applications. Do not attempt to run `dotnet run` on the Sample project in CI/headless environments.
- You can build the Sample project successfully with `dotnet build`, but execution will fail with "XOpenDisplay failed" errors in headless environments.
- ALWAYS verify that all 186 tests pass before submitting changes.
- Test coverage includes both unit tests (Irihi.Avalonia.Shared.UnitTest) and headless tests (Irihi.Avalonia.Shared.HeadlessTest).

## Common Tasks

The following are outputs from frequently run commands. Reference them instead of running bash commands to save time.

### Repository Structure
```
├── .github/workflows/          # CI/CD workflows
├── src/
│   ├── Irihi.Avalonia.Shared/              # Main library (.NET Standard 2.0)
│   ├── Irihi.Avalonia.Shared.Contracts/    # Contracts library (.NET Standard 2.0)
│   └── Irihi.Avalonia.Shared.Public/       # Shared source code (projitems)
├── test/
│   ├── Irihi.Avalonia.Shared.UnitTest/         # Unit tests (.NET 8)
│   ├── Irihi.Avalonia.Shared.HeadlessTest/     # Headless UI tests (.NET 8)
│   └── Irihi.Avalonia.Shared.UnitTest.Public/  # Shared test code (projitems)
└── sample/
    └── Sample/                 # Sample Avalonia application (.NET 8)
```

### Key Projects and Their Purpose
- **Irihi.Avalonia.Shared**: Main library containing Avalonia utility classes, published to NuGet
- **Irihi.Avalonia.Shared.Contracts**: Interface contracts library, published to NuGet  
- **Irihi.Avalonia.Shared.Public**: Shared source code included in both main projects via projitems
- **Sample**: Demonstration Avalonia desktop application showing library usage
- **Tests**: Comprehensive test coverage with 186 tests across unit and headless test projects

### Main Library Contents (src/Irihi.Avalonia.Shared.Public/)
- `Common/`: Base classes and command implementations
- `Contracts/`: Interface definitions for controls and components
- `Converters/`: Value converters for data binding
- `Helpers/`: Utility classes and extension methods
- `MarkupExtensions/`: XAML markup extensions (ThicknessMixer, CornerRadiusMixer)
- `Reactive/`: Reactive programming utilities and disposables
- `Shapes/`: Custom shape controls

### Build Output Verification
After a successful build, expect these outputs:
- Main library: `src/Irihi.Avalonia.Shared/bin/Debug/netstandard2.0/Irihi.Avalonia.Shared.dll`
- Contracts: `src/Irihi.Avalonia.Shared.Contracts/bin/Debug/netstandard2.0/Irihi.Avalonia.Shared.Contracts.dll`
- Sample app: `sample/Sample/bin/Debug/net8.0/Sample.dll`
- Test assemblies: `test/*/bin/Debug/net8.0/*.dll`

### NuGet Package Output
After packing, expect these files in `./nugets/`:
- `Irihi.Avalonia.Shared.0.3.1.nupkg` and `Irihi.Avalonia.Shared.0.3.1.snupkg`
- `Irihi.Avalonia.Shared.Contracts.0.3.1.nupkg` and `Irihi.Avalonia.Shared.Contracts.0.3.1.snupkg`

## CI/CD Integration

The repository uses three GitHub Actions workflows:
- **test.yml**: Runs on pull requests, executes build and test sequence
- **pack.yml**: Manual trigger, builds, tests, and creates NuGet packages  
- **release-tag.yml**: Triggers on version tags, creates GitHub releases with packages

ALWAYS ensure your changes pass the same commands used in CI:
1. `dotnet restore`
2. `dotnet build --no-restore` 
3. `dotnet test --no-build --verbosity normal`

## Troubleshooting

### Common Issues
- **Build warnings about nullable reference types**: Expected in test projects, do not block CI
- **Sample app "XOpenDisplay failed"**: Expected in headless environments, ignore for development
- **Missing package readme warnings**: Expected during pack, packages still build successfully

### Dependencies
- Uses Avalonia 11.0.0+ as the primary UI framework dependency
- Targets .NET Standard 2.0 for maximum compatibility in library projects
- Test and sample projects use .NET 8.0
- No custom build tools or scripts required beyond standard .NET CLI

### Performance Expectations
- Cold build (with restore): ~22-35 seconds total
- Incremental build: ~9-13 seconds  
- Test execution: ~5 seconds (186 tests)
- Pack operations: ~1-2 seconds each
- Clean operations: ~1-2 seconds

NEVER CANCEL long-running operations. These timings are normal and operations will complete successfully.