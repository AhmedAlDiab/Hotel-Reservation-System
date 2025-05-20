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
        private ADDSTAFF aDDSTAFF;
        public static ObservableCollection<Staff>staffs = new ObservableCollection<Staff>();
        public StaffManegment()
        {
            InitializeComponent();
            staffs=new ObservableCollection<Staff>();
            // calling rows of information of staffs
            foreach (var usr in Data.Users)
            {
                if (usr.IsAdmin)
                {
                    StaffManegment.staffs.Add(usr as Staff);
                }
            }
            // binding between staffs collection to data grid
            dataGrid.ItemsSource=staffs;           

        }

        private void Button_Click(object sender, RoutedEventArgs e) //add
        {
            // Here open user control to enter inputs of details of staff
            if (aDDSTAFF == null)
            {
                aDDSTAFF = new ADDSTAFF();
                aDDSTAFF.CloseRequested += () =>
                {
                    UserControlHost.Content = null;
                    aDDSTAFF = null;
                };
            }

            UserControlHost.Content = aDDSTAFF;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //remove
        {
            if (dataGrid.SelectedItem is Staff selected)
            {
                staffs.Remove(selected);

            }
        }

        
        private void Button_Click_3(object sender, RoutedEventArgs e)//back
        {
            STAFF sTAFF = new STAFF();
            sTAFF.Show();
            Window currentWindow = Window.GetWindow(this);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }
    }
}
