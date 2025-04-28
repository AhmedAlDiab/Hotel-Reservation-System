using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
namespace Hotel_Reservation_System
{
    public class Reservation
    {
        private int reservationID;
        private Customer customer;
        private Room room;
        private DateTime checkInDate;
        private DateTime checkOutDate;
        private double totalCost;
        private EReservationStatus reservationStatus;

        // Properties to access Data members
        public int ReservationID
        {
            get { return reservationID }
            set
            {
                if (value > 0)
                    reservationID = value;
                else
                    reservationID = 0;
            }
        }

        public Customer PCustomer
        {
            get { return customer}
            set
            {
                customer.UserID
            }
        }

    }
}
