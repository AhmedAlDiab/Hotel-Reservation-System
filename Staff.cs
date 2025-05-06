using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public class Staff : User
    {              
        /// <summary>
        /// A method to add a new room and then save it to the database
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="pricePerNight"></param>
        /// <param name="isAvailable"></param>
        /// <param name="capacity"></param>
        /// <param name="bedType"></param>
        /// <param name="mealPlan"></param>
        public void AddRoom(ERoomType roomType,double pricePerNight,bool isAvailable,int capacity,EBedType bedType,EMealPlan mealPlan)
        {                
            DataBase.AddRoom(roomType,pricePerNight,isAvailable,capacity,bedType,mealPlan,DataBase.connectionString);
            Data.GetData();
        }        
        /// <summary>
        /// A method to delete a room from the database
        /// </summary>
        /// <param name="roomID"></param>
        public void RemoveRoom(int roomID)
        {
            DataBase.DeleteRoomByID(roomID,DataBase.connectionString);
            Data.GetData();
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
