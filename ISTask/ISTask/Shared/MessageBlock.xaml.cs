using System;
using System.Collections.Generic;
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

namespace ISTask
{

    /// <summary>
    /// Interaction logic for ProgressModal.xaml
    /// </summary>
    public partial class MessageBlock : Window
    {

        public MessageBlock() => InitializeComponent();

        public MessageBlock(string msg) : this()
        {
            Text.Text = msg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
