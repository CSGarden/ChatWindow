using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ChatWindow.AppClasses {
    public static class AppSettings {
        private static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private static int themesIndex = 2;
      
        public static void Init() {
            string AppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ProjectFolderPath = Path.Combine(AppDataFolderPath, "ChatWindow");
            if (!AppDataFolderPath.Equals(Directory.Exists(ProjectFolderPath))) {
                RunAsAdministrator("mkdir " + ProjectFolderPath);
            }
            string configFilepath = Path.Combine(ProjectFolderPath, "config.ini");
            string configContent = "[Config]\nurl=Value1key=Value2";
            File.WriteAllText(configFilepath, configContent);
        }

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

        public static bool CheckChatWindowFolderExists() {
            string appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string projectFolderPath = Path.Combine(appDataFolderPath, "ChatWindow");
            return Directory.Exists(projectFolderPath);
        }
        private static void RunAsAdministrator(string command) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = "cmd.exe";
            startInfo.Verb = "runas";
            startInfo.Arguments = "/c " + command;

            try {
                Process.Start(startInfo);
            } catch (System.ComponentModel.Win32Exception ex) {
                // 处理管理员权限启动失败的异常
                Console.WriteLine(ex.Message);
            }
        }


    }
}
