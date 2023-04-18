// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatWindow.Helper;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatWindow.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page {
        public SettingsPage() {
            this.InitializeComponent();
            switch (MainWindow.Grid.RequestedTheme) {
                case ElementTheme.Default:
                    themeMode.SelectedIndex = 2;
                    break;
                case ElementTheme.Light:
                    themeMode.SelectedIndex = 1;
                    break;
                case ElementTheme.Dark:
                    themeMode.SelectedIndex = 0;
                    break;
            }
        }
        /// <summary>
        /// 根据选择<see langword="selectedTheme"/>的种类，改变系统的主题模式<see cref="ElementTheme"/> 对应的<see cref="ElementTheme.Default"/>枚举选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void themeMode_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedTheme = ((ComboBoxItem)themeMode.SelectedItem)?.Tag?.ToString();
            if (selectedTheme != null) {
                if (selectedTheme == "Dark") {
                    MainWindow.Grid.RequestedTheme = ElementTheme.Dark;
                } else if (selectedTheme == "Light") {
                    MainWindow.Grid.RequestedTheme = ElementTheme.Light;
                } else {
                    MainWindow.Grid.RequestedTheme = ElementTheme.Default;
                }
            }
        }
    }
}
