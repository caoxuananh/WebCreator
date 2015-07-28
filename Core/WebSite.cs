using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWebCreator
{
    public static class WebSite
    {
        public static string HomePath;
        public static string BodyHeader;
        public static string BodyCenter;
        public static string BodyRight;
        public static string BodyFooter;
        public static Dictionary<string, string> MenuLink;
        public static string HeadTag;
        public static string BodyCenterProduct;

        public static void Init()
        {
            // init head tag
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\headtag.html"))
            {
                HeadTag = reader.ReadToEnd();
            }

            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\product.html"))
            {
                BodyCenterProduct = reader.ReadToEnd();
            }

            HomePath = @"C:\WebEditor\yoursite\";

            BodyHeader = ReadBody(@"C:\WebEditor\site\header.html");
            BodyFooter = ReadBody(@"C:\WebEditor\site\footer.html");
            BodyCenter = ReadBody(@"C:\WebEditor\site\center.html");
            BodyRight = ReadBody(@"C:\WebEditor\site\right.html");

            MenuLink = new Dictionary<string, string>();

            MenuLink.Add("index.html", "Home");
            MenuLink.Add("product.html", "Product");            
        }

        public static void WriteOpen(TextWriter writer)
        {            
            writer.Write(HeadTag);            
        }

        public static void WriteMenu(TextWriter writer)
        {
            writer.Write("<body>");
            writer.Write("<nav class=\"navbar navbar-inverse\">");
            writer.Write("<div class=\"container\">");
            writer.Write("<ul class=\"nav navbar-nav\">");
            foreach (KeyValuePair<string, string> kvp in MenuLink)
            {
                writer.Write("<li><a href=\"" + kvp.Key + "\">" + kvp.Value + "</a></li>");
            }
            writer.Write("</ul>");
            writer.Write("</div>");
            writer.Write("</nav>");            
        }

        public static void WriteHeader(TextWriter writer)
        {            
            writer.Write("<div class=\"jumbotron\">");
            writer.Write("<div class=\"container\">");                
            writer.Write(BodyHeader);                
            writer.Write("</div>");
            writer.Write("</div>");            
        }

        public static void WriteFooter(TextWriter writer)
        {            
            writer.Write("<footer class=\"default-footer\">");
            writer.Write("<div class=\"container\">");
            writer.Write(BodyFooter);
            writer.Write("</div>");
            writer.Write("</footer>");         
        }

        public static void WriteEnd(TextWriter writer)
        {            
            writer.Write("<script type=\"text/javascript\" src=\"jquery-1.11.3.min.js\"></script><script type=\"text/javascript\" src=\"bootstrap.min.js\"></script></body></html>");            
        }

        public static void WriteBody(TextWriter writer)
        {            
            writer.Write("<div class=\"row\">");
            writer.Write("<div class=\"container\">");
            // body center
            writer.Write("<div class=\"col-md-8\">");
            writer.Write("<div class=\"panel panel-primary\">");
            writer.Write("<div class=\"panel-body\">");
            writer.Write(BodyCenter);
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("</div>");
            // body right
            writer.Write("<div class=\"col-md-4\">");
            writer.Write("<div class=\"panel panel-primary\">");
            writer.Write("<div class=\"panel-body\">");
            writer.Write(BodyRight);
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("</div>");

            writer.Write("</div>");
            writer.Write("</div>");
        }

        public static void WriteBodyProduct(TextWriter writer)
        {            
            writer.Write("<div class=\"row\">");
            writer.Write("<div class=\"container\">");
            // body center
            writer.Write("<div class=\"col-md-8\">");                
            writer.Write(BodyCenterProduct);                                
            writer.Write("</div>");
            // body right
            writer.Write("<div class=\"col-md-4\">");
            writer.Write("<div class=\"panel panel-primary\">");
            writer.Write("<div class=\"panel-body\">");
            writer.Write(BodyRight);
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("</div>");

            writer.Write("</div>");
            writer.Write("</div>");            
        }

        public static void GenerateSite()
        {
            // index.html
            string filename = HomePath + "index.html";
            using (TextWriter twriter = File.CreateText(filename)) 
            {
                WriteOpen(twriter);
                WriteMenu(twriter);
                WriteHeader(twriter);
                WriteBody(twriter);
                WriteFooter(twriter);
                WriteEnd(twriter);
            }

            // product.html
            filename = HomePath + "product.html";
            using (TextWriter twriter = File.CreateText(filename))
            {
                WriteOpen(twriter);
                WriteMenu(twriter);
                WriteHeader(twriter);
                WriteBodyProduct(twriter);
                WriteFooter(twriter);
                WriteEnd(twriter);
            }            
        }

        private static string ReadBody(string filename)
        {
            string res;
            using (TextReader reader = File.OpenText(filename))
            {
                res = reader.ReadToEnd();

                int start = res.IndexOf("<body>", StringComparison.CurrentCultureIgnoreCase);
                int end = res.IndexOf("</body>", StringComparison.CurrentCultureIgnoreCase);
                res = res.Substring(start + 6, end - start - 6);
            }

            return res;
        }

    }
}
