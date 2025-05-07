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
        /// <summary>
        /// Private field to store the customer's Reservation History.
        /// </summary>
        private List<Reservation> reservationHistory;

        /// <summary>
        /// Private field to store the customer's preferred type of room.
        /// </summary>
        private ERoomType preferredRoomType;
        public ERoomType PreferredRoomType
        {
            get { return preferredRoomType; }
            set { preferredRoomType = value; }
        }
        
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
         
        /// <summary>
        /// Default constructor for Customer.
        /// </summary>
        public Customer() : base()
        {
            // Initialize the preferred room type to a default value.
            PreferredRoomType = ERoomType.Standard;
            // Initialize the reservation history to an empty list.
            ReservationHistory = new List<Reservation>(); 
            IsAdmin = false; // Customers are not admins by default.
        }
        /// <summary>
        /// Parameterized constructor to initialize a Customer with user details and preferred room type.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fullname"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="preferredRoomType"></param>
        public Customer(int userID, string fullname, string phoneNumber, string email, string username, string password,ERoomType preferredRoomType) : base(userID, fullname, phoneNumber, email, username, password)
        {            
            PreferredRoomType = preferredRoomType;
            IsAdmin = false; // Customers are not admins by default.
        }
    }
}
