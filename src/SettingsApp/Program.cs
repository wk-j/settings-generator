using System;

namespace SettingsGeneratorTest {
    [AppSettings(FileName = "appsettings.json")]
    partial class AppSettings {

    }

    class Program {
        static void Main(string[] args) {
            var settings = new AppSettings();
            Console.WriteLine(settings.Database == null);
        }
    }
}
