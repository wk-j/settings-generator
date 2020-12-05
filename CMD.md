# Build

```
rm -rf .packages
dotnet clean src/SettingsGenerator
dotnet pack src/SettingsGenerator -o .packages /p:Version=0.1.2
unzip .packages/wk.SettingsGenerator.0.1.1.nupkg  -d .packages/p

dotnet add tests/MyWeb/MyWeb.csproj package wk.SettingsGenerator

dotnet add
```

# Resource

- https://github.com/dotnet/roslyn/issues/49075
- https://github.com/dotnet/roslyn/discussions/47517#discussioncomment-64145
- https://github.com/amis92/csharp-source-generators