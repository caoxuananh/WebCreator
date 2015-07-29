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
                    string str1 = reader.ReadLine();        // read name
                    string str2 = reader.ReadLine();        // read price
                    string str3 = reader.ReadLine();        // read info
                    string str4 = reader.ReadLine();        // read url of pic
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
            window.ShowDialog();
            // set selected item to new item
            LvPage.SelectedIndex = WebSite.ListOfPage.Count;
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
            // save index of item
            int mark = LvPage.SelectedIndex;
            // check if item is existed.
            if (mark != -1)
            {
                // get page from selected item.
                Page p = LvPage.SelectedItem as Page;

                // realize a window
                AddPageWindow window = new AddPageWindow();

                // add infomation for this window
                window.TxtName.Text = p.FileName;               // name
                window.TxtTitle.Text = p.PageTitle;             // page title
                // disable add button, show save button
                window.BtnAdd.Visibility = Visibility.Hidden;
                window.BtnSave.Visibility = Visibility.Visible;
                window.item = p;

                // show dialog
                window.ShowDialog();

                // update item
                WebSite.ListOfPage.RemoveAt(mark);
                WebSite.ListOfPage.Insert(mark, window.item);
            }

        }

        private void BtnAddPrd_Click(object sender, RoutedEventArgs e)
        {
            // Show window to user can add infomation about product
            AddProductWindow window = new AddProductWindow();
            window.ShowDialog();
            // Set selected item
            LvCategoue.SelectedIndex = WebSite.ListOfProduct.Count;
            // update site.txt
            using (TextWriter writer = File.CreateText(@"C:\WebEditor\site\product.txt"))
            {
                writer.WriteLine(WebSite.ListOfProduct.Count);
                foreach (Product p in WebSite.ListOfProduct)
                {
                    writer.WriteLine(p.Name);               // write name
                    writer.WriteLine(p.Price);              // write price
                    writer.WriteLine(p.Info);               // write info
                    writer.WriteLine(p.UrlOfPic);           // write url of pic
                }
            }  
        }

        private void BtnRevPrd_Click(object sender, RoutedEventArgs e)
        {
            // save selected index
            int mark = LvCategoue.SelectedIndex;
            // if item really exist
            if (mark != -1)
            {
                // remove it
                WebSite.ListOfProduct.RemoveAt(mark);
                // set selected item
                if (mark < WebSite.ListOfProduct.Count - 1)
                    LvCategoue.SelectedIndex = mark;
                else
                    LvCategoue.SelectedIndex = WebSite.ListOfProduct.Count - 1;
            }
            // update site.txt
            using (TextWriter writer = File.CreateText(@"C:\WebEditor\site\product.txt"))
            {
                writer.WriteLine(WebSite.ListOfProduct.Count);
                foreach (Product p in WebSite.ListOfProduct)
                {
                    writer.WriteLine(p.Name);               // write name
                    writer.WriteLine(p.Price);              // write price
                    writer.WriteLine(p.Info);               // write info
                    writer.WriteLine(p.UrlOfPic);           // write url of pic
                }
            }            
        }

        private void BtnEdtPrd_Click(object sender, RoutedEventArgs e)
        {
            // save index of item
            int mark = LvCategoue.SelectedIndex;
            // check if item is existed.
            if (mark != -1)
            { 
                // get product from selected item.
                Product p = LvCategoue.SelectedItem as Product;

                // realize a window
                AddProductWindow window = new AddProductWindow();                

                // add infomation for this window
                window.TxtName.Text = p.Name;               // name
                window.TxtPrice.Text = p.Price.ToString();  // price
                window.TxtInfo.Text = p.Info;               // info
                window.TxtUrlPic.Text = p.UrlOfPic;         // url of pic
                // disable add button, show save button
                window.BtnAdd.Visibility = Visibility.Hidden;
                window.BtnSave.Visibility = Visibility.Visible;
                window.item = p;

                // show dialog
                window.ShowDialog();
                
                // update item
                WebSite.ListOfProduct.RemoveAt(mark);
                WebSite.ListOfProduct.Insert(mark, window.item);
            }
        }        
    }
}
