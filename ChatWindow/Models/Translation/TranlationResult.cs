using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWindow.Models.Translation {
    public class TranlationResult {
        public string LangName { get; set; }
        public Dictionary<string, string> Results { get; set; }

        //public string Tw { get; set; }
        //    public string De { get; set; }
        //    public string Fr { get; set; }
        //    public string En { get; set; }
        //    public string Jp { get; set; }
        //    public string Pt { get; set; }
        //    public string ES { get; set; }

    }
}
