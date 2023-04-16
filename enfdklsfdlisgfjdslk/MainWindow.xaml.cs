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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace enfdklsfdlisgfjdslk
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int num;
        public MainWindow()
        {
            InitializeComponent();
            Stil.Items.Add("Regular");
            Stil.Items.Add("Underline");
            Stil.Items.Add("Italic");
            Stil.Items.Add("Bold");
            Stil.SelectedIndex = 0;
            Font_change.ItemsSource = Fonts.SystemFontFamilies.OrderBy(s =>s.Source);
            paragraph.Items.Add(1);
            paragraph.Items.Add(4);
            paragraph.Items.Add(10);

        }

        private void Stil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Stil.SelectedIndex)
            {
                case 0:
                    Jopa.Selection.ClearAllProperties();
                    break;
                case 1:
                    Jopa.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                    break;
                case 2:Jopa.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
                    break;
                case 3:
                    Jopa.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
                    break;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            num = Convert.ToInt32(Size.Text);
            if (num < 0)
            {
                
                Size.Text = "0";
            }
            else if (num > 72)
            {
                Size.Text = "72";
            }
            else if (Size.Text is null)
            {
                num = 0;
                Size.Text = "0";
            }

            Jopa.Selection.ApplyPropertyValue (TextElement.FontSizeProperty, Convert.ToDouble(num));

        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            num = Convert.ToInt32(Size.Text);
            num--;
            if (num < 0)
            {
                num = 0;
                Size.Text = "0";
                
            }
            Size.Text = num.ToString();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            num = Convert.ToInt32(Size.Text);
            num++;
            if (num > 72)
            {
                num = 72;
                Size.Text = "72";
            }
            Size.Text = num.ToString();
        }

        private void set_color(object sender, RoutedEventArgs e)
        {
            ToggleButton but = sender as ToggleButton;
            Color col = (but.Background as SolidColorBrush).Color;
            Jopa.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(col));
        }

        private void Font_change_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jopa.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, Font_change.SelectedItem.ToString());
        }

        private void Zagruz(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(Jopa.Document.ContentStart, Jopa.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private  void Save(object sender, RoutedEventArgs e)
        {
            

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(Jopa.Document.ContentStart, Jopa.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }


        }

        private void paragraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jopa.Selection.ApplyPropertyValue(TextBlock.LineHeightProperty, Convert.ToDouble(paragraph.SelectedItem));
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
