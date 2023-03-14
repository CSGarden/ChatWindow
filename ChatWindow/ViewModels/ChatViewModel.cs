using ChatWindow.Helper;
using ChatWindow.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;

namespace ChatWindow.ViewModels {
    public partial class ChatViewModel : ObservableObject {
        public ChatViewModel() {
             this.dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            this.speechRecognizer = new SpeechRecognizer();
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
        }

        private SpeechRecognizer speechRecognizer;
        private StringBuilder dictatedTextBuilder;
        private CoreDispatcher dispatcher;

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

        [RelayCommand]
        private async Task SpeechRecognitionAsync() {
            string Result = "";
            try {
                #region 第一种短语输入
                //using (SpeechRecognizer recognizer = new SpeechRecognizer()) {
                //    SpeechRecognitionCompilationResult compilationResult = await recognizer.CompileConstraintsAsync();
                //    recognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
                //    if (compilationResult.Status == SpeechRecognitionResultStatus.Success) {

                //        recognizer.UIOptions.IsReadBackEnabled = false;
                //        recognizer.UIOptions.ShowConfirmation = false;
                //        recognizer.UIOptions.AudiblePrompt = "我在听,请说...";
                //        SpeechRecognitionResult recognitionResult = await recognizer.RecognizeWithUIAsync();
                //        //SpeechRecognitionResult recognitionResult = await recognizer.RecognizeAsync();
                //        if (recognitionResult.Status == SpeechRecognitionResultStatus.Success) {
                //            Result = recognitionResult.Text;
                //        }
                //    }

                //}
                #endregion

                #region 第二种方式
                SpeechRecognitionCompilationResult result = await speechRecognizer.CompileConstraintsAsync();
                if (result.Status == SpeechRecognitionResultStatus.Success) {
                    speechRecognizer.UIOptions.IsReadBackEnabled = false;
                    speechRecognizer.UIOptions.ShowConfirmation = false;
                    speechRecognizer.UIOptions.AudiblePrompt = "我在听,请说...";
                    SpeechRecognitionResult recognitionResult = await speechRecognizer.RecognizeWithUIAsync();
                    if (recognitionResult.Status == SpeechRecognitionResultStatus.Success) {
                        Result = recognitionResult.Text;
                    }
                }
                #endregion
            } catch (Exception ex) {
                Result = ex.Message;
            }
            UserText = Result;
        }

        private void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args) {
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium || args.Result.Confidence == SpeechRecognitionConfidence.High) {
                dictatedTextBuilder.Append(args.Result.Text + " ");
                UserText = dictatedTextBuilder.ToString();
                //btnClearText.IsEnabled = true;
            } else {
                UserText = dictatedTextBuilder.ToString();

            }
        }



    }
}
