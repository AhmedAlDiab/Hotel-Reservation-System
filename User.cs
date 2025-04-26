using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    // Abstract base class representing a user in the hotel reservation system
    public abstract class User
    {
        // Private fields for encapsulation
        private int userID;
        private string fullname;
        private string phoneNumber;
        private string email;
        private string username;
        private string password;

        // Property for UserID with basic validation (must be positive)
        public int UserID
        {
            get { return userID; }
            set
            {
                if (value > 0)
                {
                    userID = value;
                }
                // No else needed; if invalid, the value simply isn't set
            }
        }

        // Property for Fullname with validation (cannot be null or empty)
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

        // Property for PhoneNumber with validation (cannot be null or empty)
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

        // Property for Email with validation (cannot be null or empty)
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

        // Property for Username with validation (cannot be null or empty)
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

        // Property for Password with validation (minimum 6 characters)
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

        // Default constructor
        public User() { }

        // Parameterized constructor to initialize all fields
        public User(int userID, string fullname, string phoneNumber, string email, string username, string password)
        {
            UserID = userID;
            Fullname = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
            Username = username;
            Password = password;
        }

        // Placeholder method to display user information
        // TODO: Implement GUI-based user info display
        public void DisplayUserInfo()
        {
            // Example: Show user details in a form or message box
        }
    }
}
