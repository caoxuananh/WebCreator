using System.Windows;
using System.IO;
using System.Collections.ObjectModel;

namespace WPFWebCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Generate default file and initiative some variable.
            WebSite.Init();

            // Bound data about Pages to List View "LvPage".
            // Init
            WebSite.ListOfPage = new ObservableCollection<Page>();
            // Read data is saved from file: site.txt
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\site.txt"))
            {
                int n = int.Parse(reader.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    string str1 = reader.ReadLine();
                    string str2 = reader.ReadLine();
                    WebSite.ListOfPage.Add(new Page(str2, str1));
                }
            }
            // Bound it.
            LvPage.ItemsSource = WebSite.ListOfPage;

            // Bound data about Product to List View "LvCategoue"
            // Init
            WebSite.ListOfProduct = new ObservableCollection<Product>();
            // Read data is saved from file: product.txt
            using (TextReader reader = File.OpenText(@"C:\WebEditor\site\product.txt"))
            {
                int n = int.Parse(reader.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    string str1 = reader.ReadLine();
                    string str2 = reader.ReadLine();
                    string str3 = reader.ReadLine();
                    string str4 = reader.ReadLine();
                    WebSite.ListOfProduct.Add(new Product() { Name=str1, Price=double.Parse(str2), Info=str3, UrlOfPic=str4 });
                }
            }
            // Bound it
            LvCategoue.ItemsSource = WebSite.ListOfProduct;
        }

        private void BtnHeader_Click(object sender, RoutedEventArgs e)
        {
            WebEditor we = new WebEditor();
            we.Show();
            we.LoadHeader();
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            WebEditor we = new WebEditor();
            we.Show();
            we.LoadRight();
        }

        private void BtnFooter_Click(object sender, RoutedEventArgs e)
        {
            WebEditor we = new WebEditor();
            we.Show();
            we.LoadFooter();
        }

        private void BtnPage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You can manage your pages in <Pages Manager> tab.", "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            if (RBDark.IsChecked == true)
            { 
                // play dark css
            }

            if (RBLight.IsChecked == true)
            {
                // play light css
            }

            if (RBBlue.IsChecked == true)
            {
                // play blue css
            }
            
            WebSite.GenerateSite();
            System.Diagnostics.Process.Start(@"C:\WebEditor\yoursite\index.html");            
        }

        private void BtnAddPage_Click(object sender, RoutedEventArgs e)
        {
            // show window for user can add infomation about his new page
            AddPageWindow window = new AddPageWindow();
            window.Show();
            // set selected item to new item
            LvPage.SelectedIndex = WebSite.ListOfPage.Count;
        }

        private void BtnRemovePage_Click(object sender, RoutedEventArgs e)
        {
            // save selected index
            int mark = LvPage.SelectedIndex;
            // if item really exist
            if (mark != -1)
            { 
                // remove it
                WebSite.ListOfPage.RemoveAt(mark);
                // set selected item
                if (mark < WebSite.ListOfPage.Count - 1)
                    LvPage.SelectedIndex = mark;
                else
                    LvPage.SelectedIndex = WebSite.ListOfPage.Count - 1;
            }
            // update site.txt
            using (TextWriter writer = File.CreateText(@"C:\WebEditor\site\site.txt"))
            {
                writer.WriteLine(WebSite.ListOfPage.Count);
                foreach (Page p in WebSite.ListOfPage)
                {
                    writer.WriteLine(p.PageTitle);
                    writer.WriteLine(p.FileName);
                }
            }
        }

        private void BtnEditPage_Click(object sender, RoutedEventArgs e)
        {
            // check if item really exist
            if (LvPage.SelectedIndex != -1)
            {
                // get string from item: form: "Title|Filename"
                string str = LvPage.SelectedItem.ToString();
                char[] sep = { '|' };
                string url = WebSite.HomePath + str.Split(sep)[1];
                WebEditor we = new WebEditor();
                we.Show();
                we.LoadSite(url);
            }
            else
                // warn user to make sure that they choosed page.
                MessageBox.Show("You forgot choose page.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void BtnAddPrd_Click(object sender, RoutedEventArgs e)
        {
            // Show window to user can add infomation about product
        }

        private void BtnRevPrd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdtPrd_Click(object sender, RoutedEventArgs e)
        {

        }        
    }
}
