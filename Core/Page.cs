using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWebCreator
{
    public class Page
    {
        public string FileName {get; set;}      // file name (*.html)

        public string PageTitle {get; set;}     // page title (to display in menu)        

        public Page()
        {
 
        }

        public Page(string fl, string title)
        {            
            PageTitle = title;
            FileName = fl;
        }

        public override string ToString()
        {
            return this.PageTitle + "|" + this.FileName;
        }
    }
}
