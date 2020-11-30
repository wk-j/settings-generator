# Settings Generator

[![Actions](https://github.com/wk-j/dotnet-settings-generator/workflows/Tests/badge.svg)](https://github.com/wk-j/dotnet-settings-generator/actions)
[![Actions](https://github.com/wk-j/dotnet-settings-generator/workflows/Build/badge.svg)](https://github.com/wk-j/dotnet-settings-generator/actions)

# Build

```
dotnet pack src/SettingsGenerator -o .packages /p:Version=0.0.6
dotnet msbuild -verbosity:diag src/MyApp
dotnet msbuild -verbosity:detail src/MyApp
```

# Resource

- https://github.com/dotnet/roslyn/issues/49075
- https://github.com/dotnet/roslyn/discussions/47517#discussioncomment-64145