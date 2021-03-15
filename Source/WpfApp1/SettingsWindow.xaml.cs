using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void testConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            var server = serverTextBox.Text;
            var database = databaseTextBox.Text;
            var username = usernameTextBox.Text;
            var password = PasswordTextBox.Password;

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.InitialCatalog = database;
            builder.UserID = username;
            builder.Password = password;

            var connectionString = builder.ConnectionString;

            var db = new MyStoreEntities3(connectionString);
            var (ok, message) = db.TestConnection();
            if (ok)
            {
                MessageBox.Show(message);
            
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            var server = serverTextBox.Text;
            var database = databaseTextBox.Text;
            var username = usernameTextBox.Text;
            var password = PasswordTextBox.Password;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["server"].Value = server;
            config.AppSettings.Settings["database"].Value = database;
            config.AppSettings.Settings["username"].Value = username;

            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            var cypherText = ProtectedData.Protect(passwordInBytes, entropy, DataProtectionScope.CurrentUser);
            
            config.AppSettings.Settings["password"].Value = Convert.ToBase64String(cypherText);
            config.AppSettings.Settings["password"].Value = Convert.ToBase64String(entropy);

            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
