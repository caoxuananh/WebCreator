using System.Windows;
using System.Windows.Forms;

namespace WPFWebCreator
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public Product item;

        public AddProductWindow()
        {
            InitializeComponent();            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TxtName.Text == "" || TxtPrice.Text == "" || TxtUrlPic.Text == "")
                // warn user to fill all field needed.
                System.Windows.MessageBox.Show("Надо заполнить все поля, которые имеет \"*\" знак.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                // update item
                item.Name = TxtName.Text;
                item.Price = double.Parse(TxtPrice.Text);
                item.Info = TxtInfo.Text;
                item.UrlOfPic = TxtUrlPic.Text;
                // come back to main window.
                this.Close();
            }                                                    
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TxtName.Text == "" || TxtPrice.Text == "" || TxtUrlPic.Text == "")
                // warn user to fill all field needed.
                System.Windows.MessageBox.Show("Надо заполнить все поля, которые имеет \"*\" знак.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {              
                // add new product to collection.
                WebSite.ListOfProduct.Add(new Product() { Name=TxtName.Text, Price=double.Parse(TxtPrice.Text), Info=TxtInfo.Text, UrlOfPic=TxtUrlPic.Text });
                // come back to main window.
                this.Close();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // just come back to main window
            this.Close();
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            // open dialog for choose file
            using (OpenFileDialog opendialog = new OpenFileDialog())
            {
                // add filter for files
                opendialog.RestoreDirectory = true;
                opendialog.Filter = "JPEG files|*.jpg|PNG files|*.png";
                // collect result
                DialogResult res = opendialog.ShowDialog();
                // if user confirm ok, add text to read-only field.
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    TxtUrlPic.Text = opendialog.FileName;
                }
            }
        }
    }
}
