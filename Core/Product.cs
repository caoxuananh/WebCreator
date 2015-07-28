using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWebCreator
{    
    public class Product
    {       
        public string Name {get; set;}
        
        public string UrlOfPic {get; set;}

        public string Info {get; set;}

        public string ToHtmlCode()
        {
            string htmlcode = "<div class=\"col-sm-6\"> <div class=\"thumbnail\">";
            htmlcode = htmlcode + "<img class=\"img-product\" src=\"file:" + "\\\\\\" + UrlOfPic + "\" /> <div class=\"caption\"> <h4>" + Name + "</h4>" + "<p>" + Info + "</p>";
            htmlcode += "</div> </div> </div>";
            return htmlcode;
        }

        public override string ToString()
        {
            return this.Name + "|" + this.Info + "|" + this.UrlOfPic;
        }
    }
}
