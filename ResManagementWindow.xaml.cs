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
using System.Windows.Shapes;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Interaction logic for ResManagementWindow.xaml
    /// </summary>
    public partial class ResManagementWindow : Window
    {
        public ResManagementWindow()
        {
            InitializeComponent();
            LoadReservationsFromDatabase();
        }
        private void LoadReservationsFromDatabase()
        {
            try
            {
                List<ReservationDisplayInfo> reservationViews = new List<ReservationDisplayInfo>();

                Data.GetData();

                foreach (var reservation in Data.Reservations)
                {
                    if (reservation.ReservationID != ActiveUser.CurrentReservationID)
                        continue;
                    var payment = Data.Payments.FirstOrDefault(p => p.ReservationID == reservation.ReservationID);
                    if (payment != null)
                    {
                        var Room = Data.Rooms.FirstOrDefault(p => p.RoomID == reservation.RoomID);
                        reservationViews.Add(new ReservationDisplayInfo
                        {
                            ReservationID = reservation.ReservationID,
                            PaymentID = payment.PaymentID,
                            PaymentDate = payment.PaymentDate,
                            PaymentMethod = payment.PaymentMethod.ToString(),
                            CheckInDate = reservation.CheckInDate,
                            CheckOutDate = reservation.CheckOutDate,
                            RoomType = Room.Roomtype.ToString(),
                            Capacity = Room.Capacity,
                            TotalPrice = payment.TotalAmount,
                            Discount = Room is DeluxeRoom d ? d.Discount : 0
                        });
                    }
                }

                ReservationsList.ItemsSource = reservationViews;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reservations: " + ex.Message);
            }
        }

    }
    public class ReservationDisplayInfo
    {
        public int ReservationID { get; set; }
        public int PaymentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
    }

}
