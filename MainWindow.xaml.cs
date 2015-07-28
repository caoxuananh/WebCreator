using System.Windows;

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

            WebSite.Init();
            WebSite.GenerateSite();
            System.Diagnostics.Process.Start(@"C:\WebEditor\yoursite\index.html");            
        }
    }
}
