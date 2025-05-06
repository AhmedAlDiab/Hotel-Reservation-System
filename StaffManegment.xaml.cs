using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for StaffManegment.xaml
    /// </summary>
    public partial class StaffManegment : UserControl
    {
        public ObservableCollection<Staff> GetStaff { get; set; }
        public StaffManegment()
        {
            InitializeComponent();
            GetStaff = new ObservableCollection<Staff>();
            this.DataContext = this;
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (staffmaneg.SelectedItem is Staff selectStaff)
                GetStaff.Remove(selectStaff);
        }

        private void add(object sender, RoutedEventArgs e)
        {
            try
            {

                Staff newstaff = new Staff { Fullname = "", PhoneNumber = "", Email = "", Username = "", Password = "" };
                GetStaff.Add(newstaff);
                staffmaneg.SelectedItem = newstaff;
                staffmaneg.ScrollIntoView(newstaff);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
