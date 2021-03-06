﻿using System;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading;
using System.ComponentModel;

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

            // Init list of page.
            WebSite.ListOfPage = new ObservableCollection<Page>();
            // Read data is saved from file: site.txt
            using (TextReader reader = File.OpenText(WebSite.TempPath + "site.txt"))
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
            WebSite.ListOfProduct = new ObservableCollection<Product>();
            // Read data is saved from file: product.txt
            using (TextReader reader = File.OpenText(WebSite.TempPath + "product.txt"))
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
            MessageBox.Show("Для добавить страницы, нажими \"СТРАНИЦЫ\"", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {           
            if (WebSite.HasIndexFile())
            {                                
                // create website
                WebSite.GenerateSite();

                // create home page link
                string url = Path.Combine(WebSite.HomePath, "index.html");

                // call def. browser, navigate to url
                System.Diagnostics.Process.Start(url);
            }
            else
                MessageBox.Show("У вас нет index.html. Добавите index.html чтобы сайт работает нормально.");
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

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            // check if has index.html
            if (!WebSite.HasIndexFile())
            { 
                MessageBox.Show("У вас нет index.html. Добавите index.html чтобы сайт работает нормально.");
                return;
            }
                
            // check if all text is filled
            if (TxtFTPAdr.Text == "" || TxtPwd.Password == "" || TxtWebAdr.Text == "" || TxtUsrName.Text == "")
            {
                MessageBox.Show("Проверять входить данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // create uploader
            FtpUploader.Init(TxtUsrName.Text, TxtPwd.Password, TxtFTPAdr.Text, TxtFolder.Text);
            // begin upload    
            PBPercent.Value = 5; // reset progress bar
            // create work thread
            BackgroundWorker wkr = new BackgroundWorker();
            wkr.WorkerReportsProgress = true;
            wkr.DoWork += wkr_DoWork;
            wkr.ProgressChanged += wrk_ProgressChanged;
            wkr.RunWorkerCompleted += wrk_RunWorkerCompleted;
            wkr.RunWorkerAsync();
        }

        private void BtnViewOnline_Click(object sender, RoutedEventArgs e)
        {
            string url;
            if (TxtFolder.Text == "")
                url = TxtWebAdr.Text + "/index.html";
            else
                url = TxtWebAdr.Text + "/" + TxtFolder.Text + "/index.html";

            // call def. browser, navigate to url
            System.Diagnostics.Process.Start(url);
        }        

        void wkr_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] allfiles = Directory.GetFiles(WebSite.HomePath); // get folder to upload
            WebSite.GenerateSite();
            FtpUploader.CreateFolder();
            int hundred = allfiles.Length;
            for (int i = 1; i < allfiles.Length + 1; i++)
            { 
                FtpUploader.DoUpload(allfiles[i-1]);
                int percent = Convert.ToInt32((double)i / hundred * 100);
                (sender as BackgroundWorker).ReportProgress(percent);                
            }
        }

        void wrk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PBPercent.Value = e.ProgressPercentage;
        }

        void wrk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Upload done!");
        }

        private void BtnHomePath_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog openDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = openDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WebSite.HomePath = openDialog.SelectedPath;
                    TxtHomePath.Text = WebSite.HomePath;
                    WebSite.CopyCssFile(WebSite.HomePath);
                }                
            }
        }
    }
}
