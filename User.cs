using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    public abstract class User
    {
        private int userID;
        private string fullname;
        private string phoneNumber;
        private string email;
        private string username;
        private string password;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public User() { }
        public User(int userID, string fullname, string phoneNumber, string email, string username, string password)
        {
            UserID = userID;
            Fullname = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
            Username = username;
            Password = password;
        }
        public void DisplayUserInfo()
        {
            Console.WriteLine("User ID is {0},FullName: {1},PhoneNumber");
        }
    }
}
