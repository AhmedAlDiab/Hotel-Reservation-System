using Mysqlx.Crud;
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

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for ADDUSER.xaml
    /// </summary>
    public partial class ADDUSER : UserControl
    {

        public ADDUSER()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullName = (txtFullName.Text);
                string PN = (txtPhoneNumber.Text);
                string EM = (txtEmail.Text);
                bool isadmin = ISadmin.IsChecked == true;
                string UN = (txtUserName.Text);
                string pass = (txtPassword.Text);

              Staff newstaff = new Staff();
                newstaff.Fullname = fullName;
                newstaff.PhoneNumber = PN;
                newstaff.Email = EM;
                newstaff.IsAdmin = isadmin;
                newstaff.Email = EM;
                newstaff.Password = pass;

                StaffManegment.staffs.Add(newstaff);
                foreach (var usr in Data.Users)
                {
                    if (usr.IsAdmin)
                    {
                        StaffManegment.staffs.Add(usr as Staff);
                    }
                }
                
                DataBase.AddUser(newstaff.Fullname, newstaff.PhoneNumber, newstaff.Email, newstaff.Username, newstaff.Password, newstaff.IsAdmin, DataBase.connectionString);
                Data.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            Window.GetWindow(this)?.Close();



        }
    }
}

