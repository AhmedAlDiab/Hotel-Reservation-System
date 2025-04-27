using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    // The Customer class inherits from the User class.
    public class Customer : User
    {
        // Private field to store the customer's preferred type of room.
        // implement private list<Reservation> reservationHistory = new List<Reservation>();

        // Private field to store the customer's preferred type of room.
        private ERoomType preferredRoomType;
        public ERoomType PreferredRoomType
        {
            get { return preferredRoomType; }
            set { preferredRoomType = value; }
        }
        // implement public list<Reservation> ReservationHistory property
        /*
         public List<Reservation> ReservationHistory
        {
            get { return reservationHistory; }
            set
            {
                if (value != null)
                    reservationHistory = value;
                else
                    reservationHistory = new List<Reservation>(); // Avoid setting to null to maintain data integrity.
            }
        }
         */
        // Default constructor for Customer.
        public Customer() { }
        // Parameterized constructor to initialize a Customer with user details and preferred room type.
        public Customer(int userID, string fullname, string phoneNumber, string email, string username, string password,/*list<Reservation> reservationHistory,*/ERoomType preferredRoomType) : base(userID, fullname, phoneNumber, email, username, password)
        {
            // ReservationHistory = reservationHistory // maybe we do not need to add ReservationHistory to the contractor;
            PreferredRoomType = preferredRoomType;
        }
    }
}
