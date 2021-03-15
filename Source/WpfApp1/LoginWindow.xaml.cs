using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsWindow();
            settings.ShowDialog();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var server = ConfigurationManager.AppSettings["server"];
            var database = ConfigurationManager.AppSettings["database"];
            var username = usernameTextBox.Text;
            var password = passwordBox.Password;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (rememberCheckBox.IsChecked == true)
            {
                config.AppSettings.Settings["username"].Value = username;
                var passwordInBytes = Encoding.UTF8.GetBytes(password);
                var entropy = new byte[20];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(entropy);
                }
                var cypherText = ProtectedData.Protect(passwordInBytes, entropy, DataProtectionScope.CurrentUser);

                config.AppSettings.Settings["password"].Value = Convert.ToBase64String(cypherText);
                config.AppSettings.Settings["entropy"].Value = Convert.ToBase64String(entropy);

            }
            else
            {
                config.AppSettings.Settings["username"].Value = "";
                config.AppSettings.Settings["password"].Value = "";
                config.AppSettings.Settings["entropy"].Value = "";
            }
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");

            var db = new MyStoreEntities3();
            var account = db.Accounts.Find(username);
            var screen = new MainWindow();
            screen.ShowDialog();
            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var server = ConfigurationManager.AppSettings["server"];
            var database = ConfigurationManager.AppSettings["database"];
            var username = ConfigurationManager.AppSettings["username"];
            var encryptedPassword = Convert.FromBase64String(ConfigurationManager.AppSettings["password"]);


            if (encryptedPassword.Length != 0)
            {
                var entropy = Convert.FromBase64String(ConfigurationManager.AppSettings["entropy"]);

                var passwordInBytes = ProtectedData.Unprotect(encryptedPassword, entropy, DataProtectionScope.CurrentUser);
                var password = Encoding.UTF8.GetString(passwordInBytes);
                usernameTextBox.Text = username;
                passwordBox.Password = password;
            }
        }
    }
}
