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
        public static string BodyCenterProduct; // code body tag in product.html        
        
        public static ObservableCollection<Page> ListOfPage;    // list of pages, its will be showed in middle of page, and add to menu link
        public static ObservableCollection<Product> ListOfProduct;  // list of products in product.html

        public static void Init()
        {
            // init head tag
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\headtag.html"))
            {
                HeadTag = reader.ReadToEnd();
            }
            // khong hieu cho nay de lam gi, sau nay se quay lai sau
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\product.html"))
            {
                BodyCenterProduct = reader.ReadToEnd();
            }

            // Init temp path
            TempPath = @"C:\WebEditor\site\";
            // Init home path
            HomePath = @"C:\WebEditor\yoursite\";
            // Init header
            BodyHeader = ReadBody(TempPath + "header.html");
            // Init footer
            BodyFooter = ReadBody(TempPath + "footer.html");            
            // Init right panel
            BodyRight = ReadBody(TempPath + "right.html");

            // Init list of page.
            ListOfPage = new ObservableCollection<Page>();
            // Read data is saved from file: site.txt
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\site.txt"))
            {
                int n = int.Parse(reader.ReadLine());           // read number of pages
                for (int i = 0; i < n; i++)
                {
                    string str1 = reader.ReadLine();            // read filename
                    string str2 = reader.ReadLine();            // read title
                    WebSite.ListOfPage.Add(new Page(str2, str1));
                }
            }
            // Init list of product
            ListOfProduct = new ObservableCollection<Product>();
            // Read data is saved from file: product.txt
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\product.txt"))
            {
                int n = int.Parse(reader.ReadLine());       // read number of products
                for (int i = 0; i < n; i++)
                {
                    string str1 = reader.ReadLine();        // read name
                    string str2 = reader.ReadLine();        // read price
                    string str3 = reader.ReadLine();        // read info
                    string str4 = reader.ReadLine();        // read url of pic
                    WebSite.ListOfProduct.Add(new Product() { Name = str1, Price = double.Parse(str2), Info = str3, UrlOfPic = str4 });
                }
            }
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

        public static void WriteBody(TextWriter writer, string filename)
        {            
            // read body code from file
            string BodyCenter = ReadBody(filename);

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
            // build every page
            foreach (Page p in ListOfPage)
            {
                string url = HomePath + p.FileName;
                string tempUlr = TempPath + p.FileName;
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
