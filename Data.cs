using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public static class Data
    {
        /// <summary>
        /// List of all users
        /// </summary>
        public static List<User> Users = new List<User>();
        /// <summary>
        /// List of all rooms
        /// </summary>
        public static List<Room> Rooms = new List<Room>();
        /// <summary>
        /// List of all reservations
        /// </summary>
        public static List<Reservation> Reservations = new List<Reservation>();
        /// <summary>
        /// List of all payments
        /// </summary>
        public static List<Payment> Payments = new List<Payment>();

        /// <summary>
        /// Method to get data from the database use this method before using the data and after adding or removing data from the database.
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
