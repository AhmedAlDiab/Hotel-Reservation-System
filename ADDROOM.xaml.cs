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
    /// Interaction logic for ADDROOM.xaml
    /// </summary>
    public partial class ADDROOM : UserControl
    {
        public event Action CloseRequested;

        public ADDROOM()
        {
            InitializeComponent();
            comboBedType.ItemsSource = Enum.GetValues(typeof(EBedType));
            comboMealPlan.ItemsSource = Enum.GetValues(typeof(EMealPlan));
            comboRoomType.ItemsSource = Enum.GetValues(typeof(ERoomType));
        }

        private void Button_Click(object sender, RoutedEventArgs e) // add
        {
            try
            {
                int roomID = int.Parse(txtRoomID.Text);
                int capacity = int.Parse(txtCapacity.Text);
                double price = double.Parse(txtPrice.Text);
                bool isAvailable = chkAvailable.IsChecked == true;
                double discount = 0;
                ERoomType roomType = (ERoomType)comboRoomType.SelectedItem;
                EBedType bedType = (EBedType)comboBedType.SelectedItem;
                EMealPlan mealPlan = (EMealPlan)comboMealPlan.SelectedItem;

                Room room;
                if (((ComboBoxItem)comboRoomClass.SelectedItem).Content.ToString() == "Deluxe")
                {
                    discount = double.Parse(txtDiscount.Text);
                    room = new DeluxeRoom(roomID, bedType, isAvailable, capacity, price, mealPlan, roomType, discount);
                }
                else
                {
                    discount = 0;
                    room = new StandardRoom(roomID, bedType, isAvailable, capacity, price, mealPlan, roomType);
                }

                Room_Manegment.rooms.Add(room);
                Data.GetData();
                Staff Admin = Data.Users.FirstOrDefault(u => u.UserID == ActiveUser.UserID) as Staff;
                Admin.AddRoom(roomType, price, isAvailable, capacity, bedType, mealPlan, discount);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //remove
        {
            CloseRequested?.Invoke();

        }
    }
}
