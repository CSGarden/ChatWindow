using ChatApi;
using ChatWindow.Models;
using ChatWindow.Models.Translation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage.Pickers;

namespace ChatWindow.ViewModels {
    public partial class TranslationViewModel : ObservableObject {
        [ObservableProperty]
        private Apowersoft apowersoft;
        [ObservableProperty]
        private bool isAllChecked;
        [ObservableProperty]
        private ObservableCollection<UIMessage> msList = new ObservableCollection<UIMessage>();

        [ObservableProperty]
        private Visibility isVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private ObservableCollection<LangItem> langItems;
        partial void OnIsAllCheckedChanged(bool Value) {
            SelectAll(Value);
        }

        private void SelectAll(bool isChecked) {
            foreach (var item in LangItems) {
                item.IsChecked = isChecked;
            }

        }
        public TranslationViewModel() {
            LangItems = new ObservableCollection<LangItem> {
                new LangItem{ LangName = "Tw" , IsChecked = false},
                new LangItem{ LangName = "En" , IsChecked = false},
                new LangItem{ LangName = "Fr" , IsChecked = false},
                new LangItem{ LangName = "De" , IsChecked = false},
                new LangItem{ LangName = "Jp" , IsChecked = false},
                new LangItem{ LangName = "Pt" , IsChecked = false},
                new LangItem{ LangName = "Es" , IsChecked = false},
            };
            LangItems.CollectionChanged += LangItems_CollectionChanged;
        }
        private Microsoft.UI.Dispatching.DispatcherQueue dispatcher;
        #region 关于全选
        private void LangItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null) {
                foreach (LangItem item in e.NewItems) {
                    item.PropertyChanged += LangItem_PropertyChanged;
                }
            }

            if (e.OldItems != null) {
                foreach (LangItem item in e.OldItems) {
                    item.PropertyChanged -= LangItem_PropertyChanged;
                }
            }

            CheckIsAllChecked();
        }
        private void LangItem_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(LangItem.IsChecked)) {
                CheckIsAllChecked();
            }
        }
        private void CheckIsAllChecked() {
            IsAllChecked = LangItems.All(x => x.IsChecked);
        }
        #endregion

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

        [RelayCommand]
        private void OnConfirm(IList<object> SelectedItems) {
            string xml = "";
            foreach (var item in SelectedItems) {
                var name = (item as ApowersoftItem).Name;
                var value = (item as ApowersoftItem).Value;
                xml += $"<item name=\"{name}\">{value}</item>\r\n";
            }
            OnLangSendCaht(xml);
        }

        private async Task OnLangSendCaht(string chatText) {
            if (!string.IsNullOrEmpty(chatText)) {
                var questionText = "";
                string[] lang = LangItems.Where(x => x.IsChecked).Select(x => x.LangName).ToArray();
                var apiHelper = App.Services.GetService<ChatApiHelper>();
                if (IsAllChecked) {
                    questionText = string.Format("将xml文档翻译成{0}，并将结果按照翻译语言为key，翻译结果xml为值(一定要保证源xml的标签格式)以json格式返回给我，请注意不要有其他任何的提示词，以及不要用markdown语法，我需要纯文本，这是需要翻译的xml文档：{1}", string.Join(" ", lang), chatText);
                }
                IsVisibility=Visibility.Visible;
                var result = await apiHelper.GetResults(questionText);
                var text = result.text;
                IsVisibility = Visibility.Collapsed;
                Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
                try {
                    foreach (KeyValuePair<string, string> entry in dictionary) {
                        // 创建XmlDocument对象
                        XmlDocument doc = new XmlDocument();
                        // 创建主标签
                        XmlElement root = doc.CreateElement("Apowersoft");
                        root.SetAttribute("LangName", entry.Key);
                        root.InnerXml = entry.Value;
                        doc.AppendChild(root);
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        // 确保文件名是有效的
                        string fileName = string.Concat(entry.Key.Where(c => !Path.GetInvalidFileNameChars().Contains(c))) + ".xml";
                        string fullPath = Path.Combine(desktopPath, fileName);
                        doc.Save(fullPath);
                    }
                } catch (System.Xml.XmlException ex) {

                    Console.WriteLine(ex.Message);
                }

            }
        }

        private async void ShowSaveSuccessDialog() {
            ContentDialog saveSuccessDialog = new ContentDialog() {
                Title = "保存成功",
                Content = "您的数据已经成功保存。",
                CloseButtonText = "确定"
            };

            await saveSuccessDialog.ShowAsync();
        }
    }
}
