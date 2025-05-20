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
    /// Interaction logic for Room_Manegment.xaml
    /// </summary>
    public partial class Room_Manegment : UserControl
    {
        private ADDROOM aDDROOM;
        
       public static 
            ObservableCollection<Room>rooms = new ObservableCollection<Room>();
        // ctor of page
        public Room_Manegment()
        {
            InitializeComponent();

            rooms = new ObservableCollection<Room>();
            // calling rows of information of rooms of hotels
            foreach (var rms in Data.Rooms)
            {

                Room_Manegment.rooms.Add(rms as Room);
                
            }
            // binding between rooms collection to data grid
            gridRoom.ItemsSource = rooms;
        }

        private void roomGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)// add
        {
            // Here open user control to enter inputs of details of room
            if (aDDROOM == null)
            {
                aDDROOM = new ADDROOM();
                aDDROOM.CloseRequested += () =>
                {
                    ADDROOMHost.Content = null;
                    aDDROOM = null;
                };
            }

            ADDROOMHost.Content = aDDROOM;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // remove
        {
            if (gridRoom.SelectedItem is Room selected)
            {
                // get data
                Data.GetData();
                // use staff method if remove to remove row of detials room
                Staff Admin = Data.Users.FirstOrDefault(u => u.UserID == ActiveUser.UserID) as Staff;

                Admin.RemoveRoom(selected.RoomID);
                rooms.Remove(selected);                
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //back
        {
            // calling STAFF page 
            STAFF sTAFF = new STAFF();
            // open STAFF page
            sTAFF.Show();
            // Close current page 
            Window currentWindow = Window.GetWindow(this);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }
    }
}