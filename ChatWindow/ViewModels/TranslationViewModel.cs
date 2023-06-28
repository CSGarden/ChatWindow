using ChatWindow.Models.Translation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage.Pickers;

namespace ChatWindow.ViewModels
{
    public partial class TranslationViewModel: ObservableObject {
        [ObservableProperty]
        private Apowersoft apowersoft;

        [ObservableProperty]
        private ApowersoftItem isSelectedItem=new ApowersoftItem();
       

    [RelayCommand]
        private async void OnXMLTextSend() {
            var openPicker = new FileOpenPicker();
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(MainWindow.Instance);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".xml");
            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null) {
                string filePath = file.Path;
                //XDocument xdoc = XDocument.Load(filePath);
                //选的特定的行
                XmlSerializer serializer = new XmlSerializer(typeof(Apowersoft));
                using (StreamReader reader = new StreamReader(filePath)) {
                    Apowersoft = (Apowersoft)serializer.Deserialize(reader);
                }
            }
        }
    }
}
