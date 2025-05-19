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
        List<StandardRoom> standardRooms = new List<StandardRoom>();
        List<DeluxeRoom> deluxeRooms = new List<DeluxeRoom>();
        public ReservationWindow()
        {
            InitializeComponent();
            Data.GetData();
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
        DateTime checkInDate;
        DateTime checkOutDate;
        int numberOfNights;
        
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
        private void roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomType.SelectedItem != null)
            {
                ERoomType selectedRoomType = (ERoomType)roomType.SelectedItem;
                if (selectedRoomType == ERoomType.Standard)
                {
                    roomType.ItemsSource = standardRooms;
                }
                else if (selectedRoomType == ERoomType.Deluxe)
                {
                    roomType.ItemsSource = deluxeRooms;
                }
            }
        }
        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            Calendar2Popup.IsOpen = true;
        }

        private void showNumberOfNights_Click(object sender, RoutedEventArgs e)
        {
            if (checkInDate < checkOutDate)
            {
                numberOfNights = (checkOutDate - checkInDate).Days;
                DisplayNumberOfNights.Text = $"Number of nights is : {numberOfNights}";
            }
            else
            {
                MessageBox.Show("Check-out date must be after check-in date.", "Invalid Dates", MessageBoxButton.OK, MessageBoxImage.Warning);
                DisplayNumberOfNights.Text = $"Number of nights is : ";
                return;
            }
        }

        private void bookRoom_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
