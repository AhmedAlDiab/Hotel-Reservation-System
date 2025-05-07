using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        // method to validate email 
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                // check if the domain contains a dot
                string domainPart = addr.Host;
                return domainPart.Contains('.') && addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        private void RegisterB_Click(object sender, RoutedEventArgs e)
        {
            string fullname = Fullname.Text.Trim();
            string username = Username.Text.Trim();
            string password = Password.Password.Trim();
            string confirmPassword = ConfirmPassword.Password.Trim();
            string email = Email.Text.Trim();
            string phoneNumber = PhoneNumber.Text.Trim();
            bool isAdmin = false;
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please fill in all fields." , "Input Error" , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password.Length< 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool isValidNumber = true;
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (!char.IsDigit(phoneNumber[i]))
                {
                    isValidNumber = false;
                    break;
                }
            }
            if (!isValidNumber)
            {
                MessageBox.Show("Phone number must contain only digits." ,  "Ivalid input" , MessageBoxButton.OK , MessageBoxImage.Error);
                return;
            }
            try
            {
                DataBase.AddUser(fullname, phoneNumber, email, username, password, isAdmin, DataBase.connectionString);
                // Back to login again 
                var loginWindow = new LoginWindow();
                this.Hide();
                loginWindow.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Rlogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            this.Hide();
            loginWindow.ShowDialog();
            this.Close();
        }
    }
}
