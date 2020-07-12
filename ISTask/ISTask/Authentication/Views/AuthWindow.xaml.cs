using ISTask.Authentication;
using ISTask.Authentication.Views;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ISTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        Brush _default;
        IAuthStorage accountManager { get; set; }

        public AuthWindow()
        {
            InitializeComponent();
            _default = GuestButton.Background;
            try
            {
                accountManager = new LocalAccountManager(Properties.Settings.Default.Server);
            }
            catch
            {
                new MessageBlock("Can't connect to datastore") {WindowStartupLocation = WindowStartupLocation.CenterScreen }.ShowDialog();
                this.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) => AuthButtonHandlerAsync(LoginText.Text, PasswordText.Password);

        private void Chip_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
            this.Close();
        }

        private async void AuthButtonHandlerAsync(string name, string password)
        {
            LockWindow();
            if (await AuthAsync(name, password))
            {
                if (RememberMe.IsChecked ?? false)
                {
                    UpdateSettings(name, password, true);
                }
                else
                {
                    UpdateSettings(null, null, false);
                }
                this.Close();
            }
            else
            {
                _ = new MessageBlock("Wrong password or name") { Owner = this }.ShowDialog();
                UnlockWindow();
            }
        }

        private void LockWindow()
        {
            GuestButton.Opacity = 0.5;
            MainContainer.IsEnabled = false;
            Progress.Visibility = Visibility.Visible;
        }

        private void UnlockWindow()
        {
            GuestButton.Opacity = 1;
            Progress.Visibility = Visibility.Hidden;
            MainContainer.IsEnabled = true;
        }

        private void UpdateSettings(string login,string password,bool IsRemember)
        {
            Properties.Settings.Default.Login = login;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.IsRemember = IsRemember;
            Properties.Settings.Default.Save();
        }

        private bool Auth(string name, string password)
        {
            try
            {
                return accountManager.VerifyPassword(name, password);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async ValueTask<bool> AuthAsync(string name, string password) => await Task.Run(() => Auth(name, password));

        private void GuestButton_MouseEnter(object sender, MouseEventArgs e) => GuestButton.Background = Brushes.LightGray;

        private void GuestButton_MouseLeave(object sender, MouseEventArgs e) => GuestButton.Background = _default;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.IsRemember)
            {
                LoginText.Text = Properties.Settings.Default.Login ?? string.Empty;
                PasswordText.Password = Properties.Settings.Default.Password ?? string.Empty;
                RememberMe.IsChecked = Properties.Settings.Default.IsRemember;
            }
        }
    }


    public class NotEmptyValidationRule : ValidationRule
    {
        public string Message { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(false, Message ?? "A value is required");
            }
            return ValidationResult.ValidResult;
        }
    }
}
