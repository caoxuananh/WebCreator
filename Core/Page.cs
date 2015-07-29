using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWebCreator
{
    public class Page
    {
        public string FileName {get; set;}
        public string PageTitle {get; set;}

        public string BgColorCode;
        public string HtmlCode;

        public Page(string fl, string title)
        {
            HtmlCode = "";
            PageTitle = title;
            FileName = fl;
        }

        public override string ToString()
        {
            return this.PageTitle + "|" + this.FileName;
        }
    }
}
