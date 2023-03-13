using ChatWindow.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ChatWindow.Selectors {
    public class ChatTemplateSelector : DataTemplateSelector {
        public DataTemplate InputTemplate { get; set; }
        public DataTemplate AiInputTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item) {
            UIMessage message = (UIMessage)item;
            if (message.From == MessageType.Input) {
                return InputTemplate;
            } else {
                return AiInputTemplate;
            }
        }
    }
}
