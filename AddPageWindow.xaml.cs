using System.Windows;
using System.IO;

namespace WPFWebCreator
{
    /// <summary>
    /// Interaction logic for AddPageWindow.xaml
    /// </summary>
    public partial class AddPageWindow : Window
    {        
        public AddPageWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // check if all field must be filled.
            if (TxtName.Text == "" || TxtTitle.Text == "")
                // if not, show a error to users.
                MessageBox.Show("You must enter all field.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                // make url by homepath + filename
                string url = WebSite.HomePath + TxtName.Text;
                // create file if it isn't existed
                if (!File.Exists(url))
                    File.Create(url);                

                // if yes, add page to our list of page in WEBSITE.
                WebSite.ListOfPage.Add( new Page(TxtName.Text, TxtTitle.Text));                
                // close addpagewindow, back to main window.
                this.Hide();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
 
        }

        private void BtnEditor_Click(object sender, RoutedEventArgs e)
        {
            // make url by homepath + filename
            string url = WebSite.HomePath + TxtName.Text;
            // create file if it isn't existed
            if (!File.Exists(url))
                File.Create(url);
            // open webeditor and load file for editing...
            WebEditor we = new WebEditor();
            we.Show();
            we.LoadSite(url);
        }
    }
}
