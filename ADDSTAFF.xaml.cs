using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.Sec;
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
        private bool isvalidName(string name)
        {
            bool valid = true;
            for (int i = 0; i < name.Length; i++)
            {
                if ((name[i] >= 'A' && name[i] <= 'Z') || (name[i] >= 'a' && name[i] <= 'z'))
                {
                    valid = true;
                }
                else
                {
                    valid= false;
                    break;
                }
            }
            return valid;
        }
        private bool isvalidNumber(string num)
        {
            bool valid = true;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] >= '0' && num[i] <= '9')
                {
                    valid = true;
                }
                else
                {
                    valid = false; break;
                }
            }
            return valid;
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
                if (isvalidName(FN) == true)
                    newstaff.Fullname = FN;
                else
                    MessageBox.Show("you must enter charcter only");
                if(isvalidNumber(PN) == true)
                    newstaff.PhoneNumber = PN;
                else
                    MessageBox.Show("you must enter Numbers only");

                if (isvalidName(EM) == true)
                    newstaff.Email = EM;
                else
                    MessageBox.Show("Value is not valid!");
                    newstaff.IsAdmin = ISAD;
               
                newstaff.Password = Pass;

                if (isvalidName(UN) == true)
                    newstaff.Username = UN;
                else
                    MessageBox.Show("you must enter charcter only");

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
