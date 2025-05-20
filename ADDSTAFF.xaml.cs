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
    /// Interaction logic for ADDSTAFF.xaml
    /// </summary>
    public partial class ADDSTAFF : UserControl
    {
        public event Action CloseRequested;
        public ADDSTAFF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               string FN=(txtFullName.Text);
                string PN=(txtPhoneNumber.Text);
                string EM=txtEmail.Text;
                string UN=txtUserName.Text;
                string Pass=txtPassword.Text;
                bool ISAD=chadmin.IsChecked==true;
                Staff newstaff = new Staff();
                newstaff.Fullname = FN;
                newstaff.PhoneNumber = PN;
                newstaff.Email = EM;
                newstaff.IsAdmin = ISAD;
                newstaff.Email = EM;
                newstaff.Password = Pass;

                StaffManegment.staffs.Add(newstaff);
                DataBase.AddUser(newstaff.Fullname, newstaff.PhoneNumber, newstaff.Email, newstaff.Username, newstaff.Password, newstaff.IsAdmin, DataBase.connectionString);
                Data.GetData();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke();

        }
    }
}
