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
        private int numberOfNights;
        private EReservationStatus reservationStatus;

        // Properties to access Data members
        public int ReservationID
        {
            get { return reservationID; }
            set
            {
                if (value > 0)
                    reservationID = value;
                else
                    reservationID = 0;
            }
        }
        public int NumberOfNights
        {
            get { return numberOfNights; }
            set
            {
                if (value > 0)
                {
                    numberOfNights = value;
                } else
                {
                    throw new AggregateException("Number of nights must be greater than zero");
                }
            }
        }

        public Customer PCustomer
        {
            get { return customer;}
            set
            {
                customer.UserID = value.UserID;
                customer.Fullname = value.Fullname;
                customer.PhoneNumber = value.PhoneNumber;
                customer.Email = value.Email;
                customer.Username = value.Username;
                customer.Password = value.Password;
                customer.PreferredRoomType = value.PreferredRoomType;
            }
        }

        public Room PRoom
        {
            get { return room; }
            set
            {
                room.RoomID = value.RoomID;
                room.Bedtype = value.Bedtype;
                room.ISAvailable = value.ISAvailable;
                room.Capacity = value.Capacity;
                room.Roomtype = value.Roomtype;
                room.PricePerNight = value.PricePerNight;
                room.eMealPlan = value.eMealPlan;
            }
        }

        public DateTime CheckInDate
        {
            get { return checkInDate; }
            set
            {
                checkInDate = value; // Validtion required!
            }
        }

        public DateTime CheckOutDate
        {
            get { return checkOutDate; }
            set
            {
                checkOutDate = value;// Validtion required!
            }
        }

        public double TotalCost
        {
            get { return totalCost; }
            set
            {
                if (value > 0)
                {
                    totalCost = value;
                }
                else
                {
                    throw new AggregateException("Total cost is less than 0");
                }
            }
        }
        
        public EReservationStatus ReservationStatus
        {
            get { return reservationStatus; }
            set
            {
                reservationStatus = value;
            }
        }
        // Constructors With + Without
        public Reservation() {}

        public Reservation(int reservationId, Customer customer, Room room, DateTime checkInDate, DateTime checkOutDate,
            double totalCost, EReservationStatus reservationStatus)
        {
            ReservationID = reservationId;
            PCustomer = customer;
            PRoom = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalCost = totalCost;
            ReservationStatus = reservationStatus;
        }

        public void BookRoom(int roomId)
        {
            // todo Implement needed
        }
    }
}
