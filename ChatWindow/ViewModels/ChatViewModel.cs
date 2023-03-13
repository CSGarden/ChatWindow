using ChatWindow.Helper;
using ChatWindow.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;


namespace ChatWindow.ViewModels {
    public partial class ChatViewModel : ObservableObject {
        [ObservableProperty]
        private ObservableCollection<UIMessage> messagesList = new ObservableCollection<UIMessage>();
        [ObservableProperty]
        private string userText;
        [RelayCommand]
        private async void OnMessageSend() {
            OnSendAsync();

        }

        [RelayCommand]
        private void OnMessageEnterSend(KeyRoutedEventArgs args) {
            if (args.Key == Windows.System.VirtualKey.Enter) {
                OnSendAsync();
            }
        }

        private async Task OnSendAsync() {
            if (!string.IsNullOrEmpty(userText)) {
                var text = userText;
                UserText = "";
                ApiHelper apiHelper = new ApiHelper();
                UIMessage myMessage = new UIMessage();
                myMessage.Message = text;
                myMessage.From = MessageType.Input;
                MessagesList.Add(myMessage);
                myMessage.ProgressIsVisibility = Visibility.Visible;
               // myMessage.ProgressIsActive = true;
                var result = await apiHelper.GetResults(text);
                myMessage.ProgressIsVisibility = Visibility.Collapsed;
                //myMessage.ProgressIsActive = false;

                UIMessage AiMessage = new UIMessage();
                AiMessage.Message = result;
                AiMessage.From = MessageType.AiInput;
                MessagesList.Add(AiMessage);
            }
        }
       
    }
}
