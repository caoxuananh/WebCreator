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
            LvPage.ItemsSource = WebSite.ListOfPage;
            // Bound data about Product to List View "LvCategoue"                        
            LvCategoue.ItemsSource = WebSite.ListOfProduct;
        }

        private void BtnHeader_Click(object sender, RoutedEventArgs e)
        {
            // create url link to temp file header.html
            string url = WebSite.TempPath + "header.html";
            // open web editor and navigate it to url
            WebEditor we = new WebEditor();
            we.Show();
            we.LoadSite(url);
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            // create url link to temp file header.html
            string url = WebSite.TempPath + "right.html";
            // open web editor and navigate it to url
            WebEditor we = new WebEditor();
            we.Show();
            we.LoadSite(url);
        }

        private void BtnFooter_Click(object sender, RoutedEventArgs e)
        {
            // create url link to temp file header.html
            string url = WebSite.TempPath + "footer.html";
            // open web editor and navigate it to url
            WebEditor we = new WebEditor();
            we.Show();
            we.LoadSite(url);
        }

        private void BtnPage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You can manage your pages in <Pages Manager> tab.", "Infomation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            // change theme (by change css properties)

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

            // build your web site
            WebSite.GenerateSite();

            // create home page link
            string url = WebSite.HomePath + "index.html";

            // call def. browser, navigate to url
            System.Diagnostics.Process.Start(url);            
        }

        private void BtnAddPage_Click(object sender, RoutedEventArgs e)
        {
            // show window for user can add infomation about his new page
            AddPageWindow window = new AddPageWindow();
            window.ShowDialog();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // save list of pages to site.txt            
            using (TextWriter writer = File.CreateText(WebSite.TempPath + "site.txt"))
            {
                writer.WriteLine(WebSite.ListOfPage.Count);
                foreach (Page p in WebSite.ListOfPage)
                {
                    writer.WriteLine(p.PageTitle);
                    writer.WriteLine(p.FileName);
                }
            }
            // save list of products to product.txt
            using (TextWriter writer = File.CreateText(WebSite.TempPath + "product.txt"))
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
    }
}
