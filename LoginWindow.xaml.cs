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

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Data.GetData();
        }
        private void LoginB_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text.Trim();
            string password = Password.Password.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                bool loginSuccess = DataBase.LogIn(username, password, DataBase.connectionString);

                if (loginSuccess)
                {
                    if (Data.Users.FirstOrDefault(u => u.UserID == ActiveUser.UserID).IsAdmin == true)
                    {
                        var staffWindow = new STAFF();
                        staffWindow.Show();
                        this.Close();
                    } else
                    {
                        var reservation_managementWindow = new ResManagementWindow();
                        reservation_managementWindow.Show();
                        this.Close();
                    }


                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while connecting to the database.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Lregister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            this.Hide();
            registerWindow.ShowDialog();
            this.Close();
        }
    }
}
