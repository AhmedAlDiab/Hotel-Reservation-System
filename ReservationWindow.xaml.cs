using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Shapes;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window
    {
        List<StandardRoom> standardRooms = new List<StandardRoom>();
        List<DeluxeRoom> deluxeRooms = new List<DeluxeRoom>();
        DateTime checkInDate;
        DateTime checkOutDate;
        int numberOfNights;
        string fullname;
        string email;
        string phoneNumber;
        double totalCost;        
        Room selectedRoom;
        public int reservationId;
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
        public ReservationWindow()
        {
            InitializeComponent();
            Data.GetData();
            roomsBox.ItemsSource = Data.Rooms;
            for (int i = 0; i < Data.Rooms.Count; i++)
            {
                if (Data.Rooms[i].Roomtype == ERoomType.Standard)
                {
                    standardRooms.Add((StandardRoom)Data.Rooms[i]);
                }
                else if (Data.Rooms[i].Roomtype == ERoomType.Deluxe)
                {
                    deluxeRooms.Add((DeluxeRoom)Data.Rooms[i]);
                }
            }
            roomType.ItemsSource = Enum.GetValues(typeof(ERoomType)).Cast<ERoomType>().ToList();
        }
        private void CheckInPopup_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CheckInPopup.SelectedDate.HasValue)
            {
                checkInDate = CheckInPopup.SelectedDate.Value;
                // Make sure that checkin date not before today
                if (checkInDate >= DateTime.Today)
                {
                    DisplayCheckIn.Text = CheckInPopup.SelectedDate.Value.ToString("yyyy-MM-dd");
                    CalendarPopup.IsOpen = false; // Hide the popup
                }
                else
                {
                    MessageBox.Show("You cannot select a date before today.", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = true;
        }

        private void CheckOutPopup_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CheckOutPopup.SelectedDate.HasValue)
            {
                checkOutDate = CheckOutPopup.SelectedDate.Value;
                // Make sure that checkout date is after today not before
                if (checkOutDate > DateTime.Today)
                {
                    DisplayCheckOut.Text = CheckOutPopup.SelectedDate.Value.ToString("yyyy-MM-dd");
                    Calendar2Popup.IsOpen = false; // Hide the popup
                }
                else
                {
                    MessageBox.Show("You cannot select a date before tomorrow.", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }
        
        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            Calendar2Popup.IsOpen = true;
        }
        
        private void roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomType.SelectedItem != null)
            {
                ERoomType selectedRoomType = (ERoomType)roomType.SelectedItem;
                if (selectedRoomType == ERoomType.Standard)
                {
                    roomsBox.ItemsSource = standardRooms;
                }
                else if (selectedRoomType == ERoomType.Deluxe)
                {
                    roomsBox.ItemsSource = deluxeRooms;
                }
            }
        }
        private void roomsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomsBox.SelectedItem != null) {
                Room selectedRoom = (Room)roomsBox.SelectedItem;
                if (selectedRoom.Roomtype == ERoomType.Standard)
                {
                    this.selectedRoom = (StandardRoom)selectedRoom;
                }
                else if (selectedRoom.Roomtype == ERoomType.Deluxe)
                {
                    this.selectedRoom = (DeluxeRoom)selectedRoom;
                }
            }
        }

        private void showNumberOfNights_Click(object sender, RoutedEventArgs e)
        {
            if (checkInDate < checkOutDate && selectedRoom != null)
            {
                numberOfNights = (checkOutDate - checkInDate).Days;
                totalCost = selectedRoom.Calculatetotalcost(numberOfNights);
                DisplayNumberOfNights.Text = $"Number of nights is : {numberOfNights}";
                displayTotalCost.Text = $"Total cost is : {totalCost}";
            }
            else
            {
                MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                DisplayNumberOfNights.Text = $"Number of nights is : ";
                return;
            }
            
        }

        private void bookRoom_Click(object sender, RoutedEventArgs e)
        {
            fullname = Fullname.Text.Trim();
            email = Email.Text.Trim();
            phoneNumber = Phone.Text.Trim();
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please enter all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (numberOfGuests.SelectedItem == null)
            {
                MessageBox.Show("Please select number of guests.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool isValidNumber = true;
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (!char.IsDigit(phoneNumber[i]))
                {
                    isValidNumber = false;
                    break;
                }
            }
            if (!isValidNumber)
            {
                MessageBox.Show("Phone number must contain only digits.", "Ivalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(DisplayCheckIn.Text.Trim()) || string.IsNullOrEmpty(DisplayCheckOut.Text.Trim()))
            {
                MessageBox.Show("Please select check-in and check-out dates.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (roomType.SelectedItem == null)
            {
                MessageBox.Show("Please select room type.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (roomsBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a room.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Reservation Res = new Reservation();
                reservationId = Res.BookRoom(ActiveUser.UserID, selectedRoom.RoomID, checkInDate, checkOutDate, totalCost, EReservationStatus.Pending);
                ActiveUser.CurrentReservationID = reservationId;
                Data.GetData();
                if (reservationId != -1)
                {                    
                    var paymentWindow = new PaymentWindow();
                    paymentWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to book the room. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while connecting to the database.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
