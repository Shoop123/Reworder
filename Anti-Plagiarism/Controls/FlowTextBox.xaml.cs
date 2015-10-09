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

namespace Anti_Plagiarism
{
    /// <summary>
    /// Interaction logic for FlowTextBox.xaml
    /// </summary>
    public partial class FlowTextBox : UserControl
    {
        public FlowTextBox()
        {
            InitializeComponent();
        }

        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((Rectangle)richTextBox.Template.FindName("rectangle", richTextBox)).Stroke = Brushes.DarkGray;
        }

        private void richTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ((Rectangle)richTextBox.Template.FindName("rectangle", richTextBox)).Stroke = Brushes.LightGray;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((RichTextBox)richTextBox.Template.FindName("textBox", richTextBox)).Document = richTextBox.Document;
        }
    }
}
