﻿using System;
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
        public string SavePath; // link to save html code

        public WebEditor()
        {
            InitializeComponent();            
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
            webBrowserEditor.newWb(@"http:\\www.anhcx.com");
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
            using (TextWriter writer = File.CreateText(SavePath))
            {
                writer.Write(Gui.GetHTML());
            }            
        }

        internal void LoadSite(string url)
        {            
            Gui.newdocumentPath(url);
            SavePath = url;
        }
    }
}
