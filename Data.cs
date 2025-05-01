using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public static class Data
    {
        public static List<User> Users = new List<User>();
        public static List<Room> Rooms = new List<Room>();
        public static List<Reservation> Reservations = new List<Reservation>();
        public static List<Payment> Payments = new List<Payment>();
        
        /// <summary>
        /// Method to get data from the database
        /// </summary>
        public static void GetData() 
        {
            Users = DataBase.GetAllUsers(DataBase.connectionString);
            Rooms = DataBase.GetAllRooms(DataBase.connectionString);
            Payments = DataBase.GetAllPayments(DataBase.connectionString);
            Reservations = DataBase.GetAllReservations(DataBase.connectionString);            
        }
    }
}
