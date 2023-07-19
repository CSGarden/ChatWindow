// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using System.Configuration;
using ChatWindow.AppClasses;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatWindow {
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) {
            AppSettings.Init();
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;

        public class ConfigManager {
            #region 第二种
            //public static async Task<string> GetConfigValue(string key) {
            //    var localFolder = ApplicationData.Current.LocalFolder;
            //    var configFile = await localFolder.GetFileAsync("config.ini");
            //    var lines = await FileIO.ReadLinesAsync(configFile);

            //    foreach (var line in lines) {
            //        if (line.Contains(key)) {
            //            var parts = line.Split('=');
            //            return parts[1].Trim();
            //        }
            //    }

            //    return null;
            //} 
            #endregion

            #region 第一种

            public  async Task<string> GetApiUrlAsync() {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile configFile = await localFolder.GetFileAsync("config.ini");
                string configFileContent = await FileIO.ReadTextAsync(configFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);
                string apiUrl = ExtractValue(configFileContent, "url");
                return apiUrl;
            }

            public async Task<string> GetApiKeyAsync() {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile configFile = await localFolder.GetFileAsync("config.ini");
                string configFileContent = await FileIO.ReadTextAsync(configFile, Windows.Storage.Streams.UnicodeEncoding.Utf8);
                string apiKey = ExtractValue(configFileContent, "key");
                return apiKey;
            }

            private static string ExtractValue(string configFileContent, string key) {
                string pattern = $@"^{key}:(.*)$";
                Regex regex = new Regex(pattern, RegexOptions.Multiline);
                Match match = regex.Match(configFileContent);
                if (match.Success) {
                    return match.Groups[1].Value.Trim();
                }
                return string.Empty;
            }
            #endregion

            public static string GetConfigValue(string key) {
                return ConfigurationManager.AppSettings[key];
            }

            public static void SaveConfigValue(string key, string value) {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }


    }
}
