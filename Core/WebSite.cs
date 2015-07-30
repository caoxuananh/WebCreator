using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWebCreator
{    
    public static class WebSite
    {
        public static string TempPath;          // temp folder, where stored file *.txt and *.html, *.css, *.js to build website
        public static string HomePath;          // result folder, where stored website
        public static string BodyHeader;        // code header (body in header.html)        
        public static string BodyRight;         // code right panel (body in right.html)
        public static string BodyFooter;        // code footer (body in footer.html)
        public static string HeadTag;           // code tag <head></head>        

        public static string StyleHeader;       // style header
        public static string StyleFooter;       // style footer 
        public static string StyleRight;        // style right
        public static string DefaultText;
        
        public static ObservableCollection<Page> ListOfPage;    // list of pages, its will be showed in middle of page, and add to menu link
        public static ObservableCollection<Product> ListOfProduct;  // list of products in product.html

        public static bool HasIndexFile()
        {
            foreach (Page p in ListOfPage)
            {
                if (p.FileName == "index.html" || p.FileName == "index.htm")
                    return true;
            }
            return false;
        }

        public static void Init()
        {
            // Init temp path
            TempPath = @"C:\WebEditor\site\";
            // Init home path
            HomePath = @"C:\WebEditor\yoursite\";
            // init head tag
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\headtag.html"))
            {
                HeadTag = reader.ReadToEnd();
            }         

            // Init header
            BodyHeader = ReadBody(TempPath + "header.html");
            StyleHeader = ReadStyle(TempPath + "header.html");
            // Init footer
            BodyFooter = ReadBody(TempPath + "footer.html");
            StyleFooter = ReadStyle(TempPath + "footer.html");
            // Init right panel
            BodyRight = ReadBody(TempPath + "right.html");
            StyleRight = ReadStyle(TempPath + "right.html");

            // set default text
            DefaultText = "<HEAD><TITLE>new</TITLE><META content=\"text/html; charset=utf-8\" http-equiv=content-type>";
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
            foreach (Page p in ListOfPage)
            {
                writer.Write("<li><a href=\"" + p.FileName + "\">" + p.PageTitle + "</a></li>");
            }

            writer.Write("<li><a href=\"" + "myproduct.html" + "\">" + "Products" + "</a></li>");
            writer.Write("</ul>");
            writer.Write("</div>");
            writer.Write("</nav>");            
        }

        public static void WriteHeader(TextWriter writer)
        {            
            writer.Write("<div " + StyleHeader + " class=\"jumbotron\">");
            writer.Write("<div class=\"container\">");                
            writer.Write(BodyHeader);                
            writer.Write("</div>");
            writer.Write("</div>");            
        }

        public static void WriteFooter(TextWriter writer)
        {            
            writer.Write("<footer" + StyleFooter + " class=\"default-footer\">");
            writer.Write("<div class=\"container\">");
            writer.Write(BodyFooter);
            writer.Write("</div>");
            writer.Write("</footer>");         
        }

        public static void WriteEnd(TextWriter writer)
        {            
            writer.Write("<script type=\"text/javascript\" src=\"jquery-1.11.3.min.js\"></script><script type=\"text/javascript\" src=\"bootstrap.min.js\"></script></body></html>");            
        }

        public static void WriteBody(TextWriter writer, string filename)
        {            
            // read body code from file
            string BodyCenter = ReadBody(filename);
            string Style = ReadStyle(filename);

            writer.Write("<div class=\"row\">");
            writer.Write("<div class=\"container\">");
            // body center
            writer.Write("<div class=\"col-md-8\">");
            writer.Write("<div class=\"panel panel-primary\">");
            writer.Write("<div " + Style + " class=\"panel-body\">");
            writer.Write(BodyCenter);
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("</div>");
            // body right
            writer.Write("<div class=\"col-md-4\">");
            writer.Write("<div class=\"panel panel-primary\">");
            writer.Write("<div " + StyleRight + " class=\"panel-body\">");
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
            writer.Write("<div class=\"col-md-8\"><div class=\"row\">");
            foreach (Product p in ListOfProduct)
            {
                writer.Write("<div class=\"col-sm-6\"><div class=\"thumbnail\">");
                writer.Write("<img class=\"img-product\" src=\"file:" + "\\\\\\" + p.UrlOfPic + "\" alt = \"" + p.Name + "\" />");
                writer.Write("<div class=\"caption\">");
                writer.Write("<h4>" + p.Name + " - " + p.Price + " rub." + "</h4>");
                writer.Write("<p>" + p.Info + "</p>");
                writer.Write("</div></div></div>");
            }
            writer.Write("</div></div>");
            // body right
            writer.Write("<div class=\"col-md-4\">");
            writer.Write("<div class=\"panel panel-primary\">");
            writer.Write("<div " + StyleRight + " class=\"panel-body\">");
            writer.Write(BodyRight);
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("</div>");

            writer.Write("</div>");
            writer.Write("</div>");            
        }

        public static void GenerateSite()
        {
            string url, tempUlr;

            Init();

            // build every page
            foreach (Page p in ListOfPage)
            {
                url = HomePath + p.FileName;
                tempUlr = TempPath + p.FileName;
                using (TextWriter twriter = File.CreateText(url))
                {                    
                    WriteOpen(twriter);
                    WriteMenu(twriter);
                    WriteHeader(twriter);
                    WriteBody(twriter, tempUlr);
                    WriteFooter(twriter);
                    WriteEnd(twriter);
                }
            }

            // build Product page
            url = HomePath + "myproduct.html";
            
            using (TextWriter twriter = File.CreateText(url))
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
                // read html document
                string temp = reader.ReadToEnd();               
                // find open body tag's position
                int startBodyTag = temp.IndexOf("<body", StringComparison.CurrentCultureIgnoreCase);
                // find end of open body tag's position
                int endBodyTag = -1;
                for (int i = startBodyTag; i < temp.Length; i++)
                    if (temp.ElementAt(i) == '>')
                    {
                        endBodyTag = i;
                        break;
                    }
               
                int end = temp.IndexOf("</body>", StringComparison.CurrentCultureIgnoreCase);
                res = temp.Substring(endBodyTag + 1, end - endBodyTag - 1);
            }

            return res;
        }

        private static string ReadStyle(string filename)
        {
            using (TextReader reader = File.OpenText(filename))
            {
                // read html code
                string temp = reader.ReadToEnd();
                // find code background
                int k = -1;
                k = temp.IndexOf("<body style", StringComparison.CurrentCultureIgnoreCase);
                // if not found
                if (k == -1)
                    return "";
                else
                    return temp.Substring(k + 6, 27);
            }            
        }
    }
}
