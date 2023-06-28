using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChatWindow.Models.Translation
{
    [XmlRoot("Apowersoft")]
    public class Apowersoft {
        [XmlAttribute("LangName")]
        public string LangName { get; set; }

        [XmlElement("item")]
        public List<ApowersoftItem> Items { get; set; }
    }
}
