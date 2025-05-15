using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Reservation_System
{
    /// <summary>
    /// Abstract base class representing a user in the hotel reservation system
    /// </summary>
    public abstract class User : INotifyPropertyChanged
    {
        // Private fields for encapsulation
        private int userID;
        private string fullname;
        private string phoneNumber;
        private string email;
        private string username;
        private string password;
        private bool isAdmin;

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
                    OnPropertyChanged(nameof(userID));
                }
                else
                {
                    userID = 0;
                    OnPropertyChanged(nameof(userID));

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
                    OnPropertyChanged(nameof(Fullname));

                }
                else
                {
                    OnPropertyChanged(nameof(Fullname));

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
                    OnPropertyChanged(nameof(PhoneNumber));

                }
                else
                {
                    OnPropertyChanged(nameof(PhoneNumber));

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
                    OnPropertyChanged(nameof(Email));
                }
                else
                {
                    OnPropertyChanged(nameof(Email));

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
                    OnPropertyChanged(nameof(Username));
                }
                else
                {
                    OnPropertyChanged(nameof(Username));

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
                    OnPropertyChanged(nameof(Password));

                }
                else
                {
                    OnPropertyChanged(nameof(Password));

                    throw new ArgumentException("Password must be at least 6 characters long");
                }
            }
        }
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
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
        // data binding 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
