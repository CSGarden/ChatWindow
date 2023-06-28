using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ColorCode.Compilation.Languages;
using ChatWindow.Models.Translation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatWindow.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TranslationPage : Page
    {
        public TranslationPage()
        {
            this.InitializeComponent();
            
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var selectedItems = ((ListView)sender).SelectedItems;
            string xml = "";
            foreach (var item in selectedItems)
            {
                var name = (item as ApowersoftItem).Name;
                var value = (item as ApowersoftItem).Value;
                xml += $"<item name=\"{name}\">{value}</item>\r\n";
            }
        }
    }
}
