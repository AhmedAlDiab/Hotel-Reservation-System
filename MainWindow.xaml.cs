﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string host = Usernametxt.Text;
            string username = Usertxt.Text;
            string password = passwordtxt.Password;
            string database = DBNametxt.Text;
            string port = porttxt.Text;
            if (DataBase.ConnectToDataBase(host, database, username, password, port) == true)
            { 
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
           
        }
    }
}