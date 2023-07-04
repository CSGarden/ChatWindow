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
            btnOk.Visibility=list.SelectedItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }


        //private void SelectAll_Checked(object sender, RoutedEventArgs e) {
        //    TwCheckBox.IsChecked = DeCheckBox.IsChecked =FrCheckBox.IsChecked = EnCheckBox.IsChecked = JpCheckBox.IsChecked = PtCheckBox.IsChecked = PtCheckBox.IsChecked = EsCheckBox.IsChecked = true;
        //}

        //private void SelectAll_Unchecked(object sender, RoutedEventArgs e) {
        //    TwCheckBox.IsChecked = DeCheckBox.IsChecked = FrCheckBox.IsChecked = EnCheckBox.IsChecked = JpCheckBox.IsChecked = PtCheckBox.IsChecked = PtCheckBox.IsChecked = EsCheckBox.IsChecked = false;
        //}

        //private void Option_Unchecked(object sender, RoutedEventArgs e) {
        //    SetCheckedState();
        //}

        //private void SelectAll_Indeterminate(object sender, RoutedEventArgs e) {
        //    if (TwCheckBox.IsChecked == true && DeCheckBox.IsChecked == true && FrCheckBox.IsChecked == true && EnCheckBox.IsChecked == true && JpCheckBox.IsChecked == true && PtCheckBox.IsChecked == true && PtCheckBox.IsChecked == true && EsCheckBox.IsChecked == true) {
        //        OptionsAllCheckBox.IsChecked = false;
        //    }
        //}

        //private void Option_Checked(object sender, RoutedEventArgs e) {
        //    SetCheckedState();
        //}

        //private void SetCheckedState() {
        //    if (TwCheckBox.IsChecked == true && DeCheckBox.IsChecked == true && FrCheckBox.IsChecked == true && EnCheckBox.IsChecked == true && JpCheckBox.IsChecked == true && PtCheckBox.IsChecked == true && PtCheckBox.IsChecked == true && EsCheckBox.IsChecked == true) {
        //        OptionsAllCheckBox.IsChecked = true;
        //    } else if (TwCheckBox.IsChecked == false && DeCheckBox.IsChecked == false && FrCheckBox.IsChecked == false && EnCheckBox.IsChecked == false && JpCheckBox.IsChecked == false && PtCheckBox.IsChecked == false && PtCheckBox.IsChecked == false && EsCheckBox.IsChecked == false) {
        //        OptionsAllCheckBox.IsChecked = false;
        //    } else {
        //        OptionsAllCheckBox.IsChecked = null;
        //    }
        //}
    }
}
