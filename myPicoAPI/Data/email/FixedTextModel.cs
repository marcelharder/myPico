using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace myPicoAPI.Data.email {
    public class FixedTextModel {
        public string Line_1 { get; set; }
        public string Line_2 { get; set; }
        public string Line_3 { get; set; }
        public string Line_4 { get; set; }
        public string Line_5 { get; set; }
        public string Line_6 { get; set; }
        public string Line_7 { get; set; }
        public string Line_8 { get; set; }
        public string Line_9 { get; set; }
        public string Line_10 { get; set; }
        public string Line_11 { get; set; }

        public FixedTextModel GetFixedText (int id) {
            var help = new FixedTextModel ();
            XDocument mail = XDocument.Load ("Data/email/email.xml", LoadOptions.None);
            var q = from c in mail.Descendants ("language") where (int) c.Attribute ("id") == id select c;
            foreach (XElement x in q) {
                help.Line_1 = x.Element ("fixed_text").Element ("line_1").Value;
                help.Line_2 = x.Element ("fixed_text").Element ("line_2").Value;
                help.Line_3 = x.Element ("fixed_text").Element ("line_3").Value;
                help.Line_4 = x.Element ("fixed_text").Element ("line_4").Value;
                help.Line_5 = x.Element ("fixed_text").Element ("line_5").Value;
                help.Line_6 = x.Element ("fixed_text").Element ("line_6").Value;
                help.Line_7 = x.Element ("fixed_text").Element ("line_7").Value;
                help.Line_8 = x.Element ("fixed_text").Element ("line_8").Value;
                help.Line_9 = x.Element ("fixed_text").Element ("line_9").Value;
                help.Line_10 = x.Element ("fixed_text").Element ("line_10").Value;
                help.Line_11 = x.Element ("fixed_text").Element ("line_11").Value;
            }

            return help;

        }
    }
}