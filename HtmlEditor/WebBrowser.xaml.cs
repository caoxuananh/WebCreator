using mshtml;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPFWebCreator
{
    /// <summary>
    /// Interaction logic for WebBrowser.xaml
    /// </summary>
    public partial class WPFWebBrowser : UserControl
    {
        public HTMLDocument doc;
        public WebBrowser webBrowser;

        public WPFWebBrowser()
        {
            InitializeComponent();            
        }

        public void newWb(string url)
        {            

            if (webBrowser != null)
            {
                webBrowser.LoadCompleted -= completed;
                webBrowser.Dispose();
                gridwebBrowser.Children.Remove(webBrowser);
            }

            if (doc != null)
            {
                doc.clear();                
            }

            webBrowser = new WebBrowser();            
            webBrowser.LoadCompleted += completed;
            gridwebBrowser.Children.Add(webBrowser);

            Script.HideScriptErrors(webBrowser, true);

            if (url == "")
            {
                webBrowser.NavigateToString(Properties.Resources.New);
                doc = webBrowser.Document as HTMLDocument;                
                doc.designMode = "On";
                Format.doc = doc;
                return;
            }
            else
            {
                webBrowser.Navigate(url);
            }

            doc = webBrowser.Document as HTMLDocument;            
            Format.doc = doc;
        }

        private void completed(object sender, NavigationEventArgs e)
        {
            doc = webBrowser.Document as HTMLDocument;            
            doc.designMode = "On";
        }
    }
}