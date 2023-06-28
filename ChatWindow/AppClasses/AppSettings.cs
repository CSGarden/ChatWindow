using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ChatWindow.AppClasses {
    public static class AppSettings {
        private static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private static int themesIndex = 2;

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
}
