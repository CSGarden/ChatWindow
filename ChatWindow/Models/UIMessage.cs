using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Documents;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;

namespace ChatWindow.Models {
    public partial class UIMessage : ObservableObject {
        public UIMessage() {
            dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        }
        Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;
        [ObservableProperty]
        private string message;
        public MessageType From { get; set; }

        [ObservableProperty]
        private bool btnIsEnable = true;

        [ObservableProperty]
        private Visibility progressIsVisibility = Visibility.Visible;
        [ObservableProperty]
        private bool progressIsActive = true;
        [RelayCommand]
        private async Task SpeakAsync() {
            BtnIsEnable = false;
            using (var speech = new SpeechSynthesizer()) {
                // 设置语音输出语言，默认为系统语言
                speech.Voice = SpeechSynthesizer.AllVoices.FirstOrDefault(voice => voice.Description.Contains("yao")) ??
                               SpeechSynthesizer.DefaultVoice;
                // 合成语音
                var stream = await speech.SynthesizeTextToStreamAsync(Message);
                // 播放语音
                var media = new MediaPlayer();
                media.MediaEnded += Media_MediaEnded;
                media.Source = MediaSource.CreateFromStream(stream, stream.ContentType);
                media.PlaybackRate =1.3;
                media.Play();
            }

        }

        private void Media_MediaEnded(MediaPlayer sender, object args) {
            dispatcherQueue.TryEnqueue(() => {
                BtnIsEnable = true;
            });
        }

       
    }
}
