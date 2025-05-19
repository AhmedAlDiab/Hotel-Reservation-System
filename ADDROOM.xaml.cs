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
        public ADDROOM()
        {
            InitializeComponent();
        }

        private void confirmroome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int roomID = int.Parse(txtRoomID.Text);
                int capacity = int.Parse(txtCapacity.Text);
                double price = double.Parse(txtPrice.Text);
               
                bool isAvailable = chkAvailable.IsChecked == true;
                ERoomType roomType = (ERoomType)comboRoomType.SelectedItem;
                EBedType bedType = (EBedType)comboBedType.SelectedItem;
                EMealPlan mealPlan = (EMealPlan)comboMealPlan.SelectedItem;

                Room room;
                if (((ComboBoxItem)comboRoomClass.SelectedItem).Content.ToString() == "Deluxe")
                {
                    double discount = double.Parse(txtDiscount.Text);
                    room = new DeluxeRoom(roomID, bedType, isAvailable, capacity, price, mealPlan, roomType, discount);
                }
                else
                {
                    room = new StandardRoom(roomID, bedType, isAvailable, capacity, price, mealPlan, roomType);
                }

              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
