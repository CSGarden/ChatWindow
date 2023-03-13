// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ChatWindow.ViewModels;
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
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatWindow.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatPage : Page {
        public ChatPage() {
            this.InitializeComponent();
        }
        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e) {
            var txt = sender as TextBlock;
            var package = new DataPackage();
            package.SetText(txt.Text);
            Clipboard.SetContent(package);
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

        }

    }
}
