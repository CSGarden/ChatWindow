using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;

namespace ChatWindow.AppClasses {
    public static class AppSettings {
        private static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private static int themesIndex = 2;

        public static async Task Init() {
            var AppDataFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            string configPath = Path.Combine(AppDataFolder.Path, "Config.ini");
            if (!File.Exists(configPath)) {
                string configContent = "[API]\nurl=Value1\nkey=Value2";
                File.WriteAllText(configPath, configContent);
                Process.Start("NotePad.exe", configPath);
            }
            ConfigManager configManager = new ConfigManager(configPath);
            URL = await configManager.GetApiUrlAsync();
            Key = await configManager.GetApiKeyAsync();
        }
        public static string URL { get; set; }
        public static string Key { get; set; }

        public static int ThemesIndex {
            get {
                if (localSettings.Values["ThemesIndex"] != null) {
                    themesIndex = (int)localSettings.Values["ThemesIndex"];
                }
                return themesIndex;
            }
            set {
                localSettings.Values["ThemesIndex"] = value;
                themesIndex = value;
            }
        }
    }
    public class ConfigManager {
        private readonly string configPath;

        public ConfigManager(string configPath) {
            this.configPath = configPath;
        }
        public async Task<string> GetApiUrlAsync() {
            StorageFile configFile = await StorageFile.GetFileFromPathAsync(configPath);
            string configFileContent = await FileIO.ReadTextAsync(configFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            var data = ParseIniFileAsync(configFileContent);
            string apiUrl = data["API"]["url"];
            return apiUrl;
        }

        public async Task<string> GetApiKeyAsync() {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile configFile = await localFolder.GetFileAsync("config.ini");
            string configFileContent = await FileIO.ReadTextAsync(configFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            var data = ParseIniFileAsync(configFileContent);
            string apiKey = data["API"]["key"];
            return apiKey;
        }

        public Dictionary<string, Dictionary<string, string>> ParseIniFileAsync(string fileContent) {
            var data = new Dictionary<string, Dictionary<string, string>>();
            var text = fileContent;

            var sectionPattern = @"\[(.*?)\]";
            var keyValuePattern = @"(.*?)=(.*)";

            string currentSection = "";

            var lines = text.Split(new[] { "\r", "\r", "\n" }, StringSplitOptions.None);

            foreach (var line in lines) {
                var sectionMatch = Regex.Match(line, sectionPattern);
                if (sectionMatch.Success) {
                    currentSection = sectionMatch.Groups[1].Value;
                    data[currentSection] = new Dictionary<string, string>();
                } else {
                    var keyValueMatch = Regex.Match(line, keyValuePattern);
                    if (keyValueMatch.Success) {
                        var key = keyValueMatch.Groups[1].Value.Trim();
                        var value = keyValueMatch.Groups[2].Value.Trim();
                        data[currentSection][key] = value;
                    }
                }
            }

            return data;
        }
    }

}
