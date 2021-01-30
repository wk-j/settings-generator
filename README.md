## Settings Generator for ASP.NET

[![Actions](https://github.com/wk-j/dotnet-settings-generator/workflows/NuGet/badge.svg)](https://github.com/wk-j/dotnet-settings-generator/actions)
[![NuGet](https://img.shields.io/nuget/v/wk.SettingsGenerator.svg)](https://www.nuget.org/packages/wk.SettingsGenerator)

## Installation

```bash
dotnet add package wk.SettingsGenerator
```

## Usage

1. Add configuration into `appsettings.json`

```json
{
    "Alfresco": {
        "Url": "http://localhost:8080",
        "User": "admin",
        "Password": "admin"
    },
    "Database": {
        "ConnectionString": "Host=localhost"
    }
}
```

2. Generate settings class

```csharp
[AppSettings(FileName = "appsettings.json")]
public partial class AppSettings { }
```

3. Load settings

```csharp
public void ConfigureServices(IServiceCollection services) {
    var settings = Configuration.Get<AppSettings>();

    Console.WriteLine(settings.Alfresco.Url);
    Console.WriteLine(settings.Database.ConnectionString);
    ...
}
```