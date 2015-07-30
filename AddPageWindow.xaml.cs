using System.Windows;
using System.IO;

namespace WPFWebCreator
{
    /// <summary>
    /// Interaction logic for AddPageWindow.xaml
    /// </summary>
    public partial class AddPageWindow : Window
    {
        public Page item;

        public AddPageWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // check if all field must be filled.
            if (TxtName.Text == "" || TxtTitle.Text == "")
                // if not, show a error to users.
                MessageBox.Show("Надо заполнить все поля, которые имеет \"*\" знак.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                // make url by temp path + filename
                string url = WebSite.TempPath + TxtName.Text;
                // create file if it isn't existed
                if (!File.Exists(url))
                    File.Create(url);

                // if yes, add page to our list of page in WEBSITE.
                WebSite.ListOfPage.Add(new Page() { FileName = TxtName.Text, PageTitle=TxtTitle.Text });                
                // close addpagewindow, back to main window.
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // check if all field must be filled.
            if (TxtName.Text == "" || TxtTitle.Text == "")
                // if not, show a error to users.
                MessageBox.Show("Надо заполнить все поля, которые имеет \"*\" знак.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                // make url by temp path + filename
                string url = WebSite.TempPath + TxtName.Text;
                // create file if it isn't existed
                if (!File.Exists(url))
                {
                    File.Create(url);
                }

                // if yes, edit info of page .
                item.FileName = TxtName.Text;
                item.PageTitle = TxtTitle.Text;
                // close addpagewindow, back to main window.
                this.Close();
            }
        }
        
        private void BtnEditor_Click(object sender, RoutedEventArgs e)
        {
            // open webeditor and load file for editing...            
            WebEditor we = new WebEditor();
            we.Show();

            // make url by homepath + filename
            string url = WebSite.TempPath + TxtName.Text;
            // create file if it isn't existed
            if (!File.Exists(url))
            { 
                File.Create(url);
                Gui.newdocument();
                we.SavePath = url;               
            }
            else
                we.LoadSite(url);
            
        }
    }
}
