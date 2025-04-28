using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class Staff : User
    {
        public void AddStanderdRoom()
        {                
            // Implementation for adding a standard room
        }
        public void AddDeluxeRoom()
        {
            // Implementation for adding a deluxe room
        }
        public void RemoveRoom()
        {
            // Implementation for removing a room
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Staff() : base()
        {
            IsAdmin = true; // Staff members are considered admins
        }    
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        public Staff(int userID, string fullname, string phoneNumber, string email, string username, string password) : base(userID, fullname, phoneNumber, email, username, password)
        {
            IsAdmin = true; // Staff members are considered admins
        }
    }
}
