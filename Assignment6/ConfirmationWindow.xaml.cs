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

namespace Assignment6
{
    /// <summary>
    /// Interaction logic for Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        private string _message;
        public Confirmation(string message)
        {
            _message = message;
            InitializeComponent();
            InitializeGui();
        }

        private void InitializeGui()
        {
            lblMessage.Content = _message;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
