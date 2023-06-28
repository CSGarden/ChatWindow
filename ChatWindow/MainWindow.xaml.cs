// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatWindow.Pages;
using Microsoft.UI;
using Microsoft.UI.Windowing;
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
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatWindow {
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window {
        public MainWindow() {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            MyFrame.SourcePageType = typeof(ChatPage);
            navigation.SelectedItem = 0;
            Grid = mainGird;
            Grid.Loaded += Grid_Loaded;
            Instance = this;
        }

        public static MainWindow Instance { get; private set; }

        private void Grid_Loaded(object sender, RoutedEventArgs e) {
            if (AppClasses.AppSettings.ThemesIndex == 0) {
                MainWindow.Grid.RequestedTheme = ElementTheme.Light;
            } else if (AppClasses.AppSettings.ThemesIndex == 1) {
                MainWindow.Grid.RequestedTheme = ElementTheme.Dark;
            } else {
                MainWindow.Grid.RequestedTheme = ElementTheme.Default;
            }
        }

        public static Grid Grid { get; set; }
        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
            if (args.InvokedItem.ToString() == "ChatEntrance") {
                MyFrame.SourcePageType = typeof(ChatPage);
            } else if (args.InvokedItem.ToString() == "…Ë÷√") {
                MyFrame.SourcePageType = typeof(SettingsPage);
            } else if (args.InvokedItem.ToString() == "Translation") {
                MyFrame.SourcePageType = typeof(TranslationPage);
            } else {
                MyFrame.SourcePageType = typeof(HomePage);
            }
        }


    }
}
