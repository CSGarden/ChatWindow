using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWindow.Models {
    public partial class LangItem : ObservableObject {
        public string LangName { get; set; }

        [ObservableProperty]
        private bool isChecked;
    }
}
