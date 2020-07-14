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

namespace ISTask.Authentication.Views
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        IAuthStorage accountManager { get; set; }

        public RegisterWindow()
        {
            InitializeComponent();
            accountManager = new LocalAccountManager(Properties.Settings.Default.AuthStorage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if( String.IsNullOrEmpty(this.LoginText.Text) || String.IsNullOrEmpty(this.PasswordText.Password) )             
            {
                new MessageBlock("Empty fields are not allowed") { Owner = this }.ShowDialog();
                return;
            }
            if(PasswordText.Password != ConfirmPasswordText.Password)
            {
                new MessageBlock("Passwords are not equals") { Owner = this }.ShowDialog();
                return;
            }
            var login = LoginText.Text;
            var password = PasswordText.Password;
            var role = (AdminRadioButton.IsChecked ?? false) ? Role.Admin : Role.User;
            if (accountManager.Register(login, password, role)) 
            {
                new MessageBlock("User was registered") { Owner = this }.ShowDialog();
                this.Close();
            }
            else
            {
                new MessageBlock("Error") { Owner = this }.ShowDialog();
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new AuthWindow().Show();
        }
    }
}
