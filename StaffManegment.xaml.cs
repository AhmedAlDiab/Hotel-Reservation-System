using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
     public static ObservableCollection<Staff> staffs {  get; set; }= new ObservableCollection<Staff>();
        public StaffManegment()
        {
            InitializeComponent();
            staffmaneg.ItemsSource = staffs;
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (staffmaneg.SelectedItem is Staff selected)
            {
                staffs.Remove(selected);
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            Contantadd.Navigate(new Uri("ADDUSER.xaml", UriKind.Relative));

        }



        private void Back(object sender, RoutedEventArgs e)
        {
            STAFF staff = new STAFF();
            staff.Show();
            Window.GetWindow(this).Close();
        }

        private void staffmaneg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Contantadd_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
