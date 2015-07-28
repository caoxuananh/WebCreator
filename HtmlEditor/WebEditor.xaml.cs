using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace WPFWebCreator
{
    /// <summary>
    /// Interaction logic for WebEditor.xaml
    /// </summary>
    public partial class WebEditor : Window, IDisposable
    {
        public int Mode; //0: newdocument 1: header, 2: center, 3: right, 4: footer

        public WebEditor()
        {
            InitializeComponent();            
        }

        public void LoadHeader()
        {
            Gui.newdocumentPath(@"C:\WebEditor\site\header.html");
            Mode = 1;
        }

        public void LoadFooter()
        {
            Gui.newdocumentPath(@"C:\WebEditor\site\footer.html");
            Mode = 4;
        }

        public void LoadCenter()
        {
            Gui.newdocumentPath(@"C:\WebEditor\site\center.html");
            Mode = 2;
        }

        public void LoadRight()
        {
            Gui.newdocumentPath(@"C:\WebEditor\site\right.html");
            Mode = 3;
        }

        private void SettingsBold_Click(object sender, RoutedEventArgs e)
        {
            Format.bold();
        }

        private void SettingsItalic_Click(object sender, RoutedEventArgs e)
        {
            Format.Italic();
        }

        private void SettingsUnderLine_Click(object sender, RoutedEventArgs e)
        {
            Format.Underline();
        }

        private void SettingsRightAlign_Click(object sender, RoutedEventArgs e)
        {
            Format.Underline();
        }

        private void SettingsLeftAlign_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyLeft();
        }

        private void SettingsCenter2_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyCenter();
        }

        private void SettingsJustifyRight_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyRight();
        }

        private void SettingsJustifyFull_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyFull();
        }

        private void SettingsInsertOrderedList_Click(object sender, RoutedEventArgs e)
        {
            Format.InsertOrderedList();
        }

        private void SettingsBullets_Click(object sender, RoutedEventArgs e)
        {
            Format.InsertUnorderedList();
        }

        private void SettingsOutIdent_Click(object sender, RoutedEventArgs e)
        {
            Format.Outdent();
        }

        private void SettingsIdent_Click(object sender, RoutedEventArgs e)
        {
            Format.Indent();
        }

        private void RibbonButtonNew_Click(object sender, RoutedEventArgs e)
        {
            Gui.newdocument();            
        }

        private void RibbonButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            Gui.newdocumentFile();
        }

        private void RibbonButtonOpenweb_Click(object sender, RoutedEventArgs e)
        {
            webBrowserEditor.newWb(@"");
        }

        private void SettingsFontColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsFontColor();
        }

        private void SettingsBackColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsBackColor();
        }

        private void SettingsAddLink_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddLink();
        }

        private void SettingsAddImage_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddImage();
        }

        private void RibbonButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Gui.RibbonButtonSave();
        }

        private void RibbonComboboxFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFonts(RibbonComboboxFonts);
        }

        private void RibbonComboboxFontHeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFontHeight(RibbonComboboxFontHeight);
        }

        private void RibbonComboboxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFormat(RibbonComboboxFormat);
        }

        private void EditWeb_Click(object sender, RoutedEventArgs e)
        {
            Gui.EditWeb();
        }

        private void ViewHTML_Click(object sender, RoutedEventArgs e)
        {
            Gui.ViewHTML();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gui.webBrowser = webBrowserEditor;
            Gui.htmlEditor = HtmlEditor1;
            Initialisation.webeditor = this;
            Gui.newdocument();

            Initialisation.RibbonComboboxFontsInitialisation();
            Initialisation.RibbonComboboxFontSizeInitialisation();
            Initialisation.RibbonComboboxFormatInitionalisation();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filename = @"C:\WebEditor\site\";
            switch (Mode)
            {
                case 1:
                    filename += "header.html";
                    break;
                case 2:
                    filename += "center.html";
                    break;
                case 3:
                    filename += "right.html";
                    break;
                case 4:
                    filename += "footer.html";
                    break;
                case 5:
                    return;
                default:
                    break;
            }
            
            using (TextWriter writer = File.CreateText(filename))
            {
                writer.Write(Gui.GetHTML());
            }
        }

        internal void LoadSite()
        {
            Gui.newdocumentPath(@"C:\WebEditor\yoursite\index.html");
            Mode = 5;
        }
    }
}
