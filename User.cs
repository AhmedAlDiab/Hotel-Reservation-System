using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Abstract base class representing a user in the hotel reservation system
    /// </summary>
    public abstract class User
    {
        // Private fields for encapsulation
        private int userID;
        private string fullname;
        private string phoneNumber;
        private string email;
        private string username;
        private string password;

        /// <summary>
        /// Property for UserID with basic validation (must be positive)
        /// </summary>
        public int UserID
        {
            get { return userID; }
            set
            {
                if (value > 0)
                {
                    userID = value;
                }
                else
                {
                    userID = 0;
                }                
            }
        }

        /// <summary>
        /// Property for Fullname with validation (cannot be null or empty)
        /// </summary>
        public string Fullname
        {
            get { return fullname; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    fullname = value;
                }
                else
                {
                    throw new ArgumentException("Fullname cannot be empty");
                }
            }
        }

        /// <summary>
        /// Property for PhoneNumber with validation (cannot be null or empty)
        /// </summary>
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    phoneNumber = value;
                }
                else
                {
                    throw new ArgumentException("Phone Number cannot be empty");
                }
            }
        }

        /// <summary>
        /// Property for Email with validation (cannot be null or empty)
        /// </summary>
        // TODO: Implement checking validation after GUI input
        public string Email
        {
            get { return email; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    email = value;
                }
                else
                {
                    throw new ArgumentException("Email cannot be empty");
                }
            }
        }

        /// <summary>
        /// Property for Username with validation (cannot be null or empty)
        /// </summary>
        // TODO: Implement checking against the database for uniqueness
        public string Username
        {
            get { return username; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    username = value;
                }
                else
                {
                    throw new ArgumentException("Username cannot be empty");
                }
            }
        }

        /// <summary>
        /// Property for Password with validation (minimum 6 characters)
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                if (value.Length >= 6)
                {
                    password = value;
                }
                else
                {
                    throw new ArgumentException("Password must be at least 6 characters long");
                }
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public User() 
        {
            // Initialize fields to default values
            userID = 0;
            fullname = "Null";
            phoneNumber = "Null";
            email = "Null";
            username = "Null";
            password = "Null";
        }

        /// <summary>
        /// Parameterized constructor to initialize all fields
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fullname"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public User(int userID, string fullname, string phoneNumber, string email, string username, string password)
        {
            UserID = userID;
            Fullname = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Placeholder method to display user information
        /// </summary>
        // TODO: Implement GUI-based user info display
        public void DisplayUserInfo()
        {
            // Example: Show user details in a form or message box
        }
    }
}
