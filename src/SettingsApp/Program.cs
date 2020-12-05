using System;


namespace App2 {
    [AppSettings(FileName = "appsettings.json")]
    partial class AppSettings2 {
    }
}

namespace SettingsGeneratorTest {
    [AppSettings(FileName = "appsettings.json")]
    partial class AppSettings {

    }

    class Program {
        static void Main(string[] args) {
            var settings = new AppSettings();
            var settings2 = new App2.AppSettings2();
        }
    }
}
