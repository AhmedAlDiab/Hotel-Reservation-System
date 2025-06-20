﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Hotel_Reservation_System
{
    public static class DataBase
    {
        /// <summary>
        ///  Connection string to connect to the database Add it when it is needed
        /// </summary>
        public static string connectionString;
        /// <summary>
        /// Connect to the database used once when the program starts
        /// </summary>
        /// <param name="Hostname"></param>
        /// <param name="Databasename"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Port"></param>
        public static bool ConnectToDataBase(string Hostname, string Databasename, string Username, string Password, string Port)
        {
            connectionString = $"Server={Hostname};Database={Databasename};User ID={Username};Password={Password};Port={Port};";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Data.GetData();
                    return true;
                }
                catch
                {                    
                    MessageBox.Show("Error connecting to the database. Please check your connection settings.","DataBase Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    return false;                    
                }
            }
        }
        /// <summary>
        /// Used to check if the user is present in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool LogIn(string username, string password, string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {                
                string query = "SELECT userID FROM Users WHERE username = @username AND password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                var result = command.ExecuteScalar();

                if (result != null)
                {
                    // Set the active user ID if login is successful
                    ActiveUser.UserID = Convert.ToInt32(result);
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// is called when we add a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool CheckUsername(string username, string connectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT 1 FROM Users WHERE username = @username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();
                var result = command.ExecuteScalar();
                return result != null;
            }
        }
        /// <summary>
        /// Adds a new user to the database.        
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fullname"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="isAdmin"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool AddUser(string fullname, string phoneNumber, string email,
                          string username, string password, bool isAdmin,
                          string connectionString)
        {
            // Check if username already exists
            if (CheckUsername(username, connectionString))
            {            
                MessageBox.Show("Username already exists. Please choose a different username.", "Log In Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Modified query to exclude userID from columns and values
                    string query = @"INSERT INTO Users 
                         (fullname, phoneNumber, email, username, password, isAdmin)
                         VALUES 
                         (@fullname, @phoneNumber, @email, @username, @password, @isAdmin)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Removed the userID parameter
                        command.Parameters.AddWithValue("@fullname", fullname);
                        command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@isAdmin", isAdmin ? 1 : 0);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {        
                MessageBox.Show($"Error adding user: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        /// <summary>
        /// adds a new reservation to the database
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="roomID"></param>
        /// <param name="checkInDate"></param>
        /// <param name="checkOutDate"></param>
        /// <param name="totalCost"></param>
        /// <param name="status"></param>
        /// <param name="numberOfNights"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static int AddReservation(int customerID, int roomID,
                DateTime checkInDate, DateTime checkOutDate,
                double totalCost, EReservationStatus status,
                int numberOfNights, string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Reservations 
                  (customerID, roomID, checkInDate, checkOutDate, 
                   totalCost, reservationStatus, numberOfNights)
                  VALUES 
                  (@customerID, @roomID, @checkInDate, @checkOutDate, 
                   @totalCost, @reservationStatus, @numberOfNights);
                  SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@customerID", customerID);
                        command.Parameters.AddWithValue("@roomID", roomID);
                        command.Parameters.AddWithValue("@checkInDate", checkInDate);
                        command.Parameters.AddWithValue("@checkOutDate", checkOutDate);
                        command.Parameters.AddWithValue("@totalCost", totalCost);
                        command.Parameters.AddWithValue("@reservationStatus", (int)status);
                        command.Parameters.AddWithValue("@numberOfNights", numberOfNights);

                        connection.Open();
                        int newReservationId = Convert.ToInt32(command.ExecuteScalar());
                        return newReservationId;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error adding reservation: {ex.Message}",
                              "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding reservation: {ex.Message}",
                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
        }
        /// <summary>
        /// add a new room to the database
        /// </summary>
        /// <param name="roomType"></param>
        /// <param name="pricePerNight"></param>
        /// <param name="isAvailable"></param>
        /// <param name="capacity"></param>
        /// <param name="bedType"></param>
        /// <param name="mealPlan"></param>
        /// <param name="connectionString"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public static bool AddRoom(ERoomType roomType, double pricePerNight, bool isAvailable,
                        int capacity, EBedType bedType, EMealPlan mealPlan,
                        double discount, string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Rooms 
                          (roomType, pricePerNight, isAvailable, capacity, 
                           bedType, mealPlans, discount)
                          VALUES 
                          (@roomType, @pricePerNight, @isAvailable, @capacity, 
                           @bedType, @mealPlans, @discount)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomType", (int)roomType);
                        command.Parameters.AddWithValue("@pricePerNight", pricePerNight);
                        command.Parameters.AddWithValue("@isAvailable", isAvailable ? 1 : 0);
                        command.Parameters.AddWithValue("@capacity", capacity);
                        command.Parameters.AddWithValue("@bedType", (int)bedType);
                        command.Parameters.AddWithValue("@mealPlans", (int)mealPlan);
                        command.Parameters.AddWithValue("@discount", discount);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error adding room: {ex.Message}",
                              "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding room: {ex.Message}",
                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        /// <summary>
        /// add a new payment to the database
        /// </summary>
        /// <param name="paymentDate"></param>
        /// <param name="totalAmount"></param>
        /// <param name="method"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool AddPayment(DateTime paymentDate, int reservationID, double totalAmount,
                    EPaymentMethod method, string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Payments 
                  (paymentDate, reservationID, totalAmount, paymentMethod)
                  VALUES 
                  (@paymentDate, @reservationID, @totalAmount, @paymentMethod)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@paymentDate", paymentDate);
                        command.Parameters.AddWithValue("@reservationID", reservationID);
                        command.Parameters.AddWithValue("@totalAmount", totalAmount);
                        command.Parameters.AddWithValue("@paymentMethod", (int)method);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error adding payment: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        /// <summary>
        /// get all users from the database and return them as a list
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<User> GetAllUsers(string connectionString)
        {
            List<User> users = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT userID, fullName, phoneNumber, email, username, password, isAdmin FROM Users";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool isAdmin = reader.GetBoolean("isAdmin");

                            User user = isAdmin ? new Staff() : new Customer();

                            user.UserID = reader.GetInt32("userID");
                            user.Fullname = reader.GetString("fullName");
                            user.PhoneNumber = reader.GetString("phoneNumber");
                            user.Email = reader.GetString("email");
                            user.Username = reader.GetString("username");
                            user.Password = reader.GetString("password");
                            user.IsAdmin = isAdmin;

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
        /// <summary>
        /// get all rooms from the database and return them as a list
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static List<Room> GetAllRooms(string connectionString)
        {
            List<Room> rooms = new List<Room>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT roomID, roomType, pricePerNight, isAvailable, capacity, bedType, mealPlans, discount FROM Rooms";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int roomID = reader.GetInt32("roomID");
                            ERoomType roomType = (ERoomType)reader.GetInt32("roomType");
                            double pricePerNight = reader.GetDouble("pricePerNight");
                            bool isAvailable = reader.GetBoolean("isAvailable");
                            int capacity = reader.GetInt32("capacity");
                            EBedType bedType = (EBedType)reader.GetInt32("bedType");
                            EMealPlan mealPlan = (EMealPlan)reader.GetInt32("mealPlans");
                            double discountForDeluxe = reader.GetDouble("discount");

                            Room room;
                            switch (roomType)
                            {
                                case ERoomType.Standard:
                                    room = new StandardRoom(roomID, bedType, isAvailable, capacity, pricePerNight, mealPlan, roomType);
                                    break;
                                case ERoomType.Deluxe:
                                    room = new DeluxeRoom(roomID, bedType, isAvailable, capacity, pricePerNight, mealPlan, roomType, discountForDeluxe);
                                    break;
                                default:
                                    throw new InvalidOperationException("Unknown room type.");
                            }

                            rooms.Add(room);
                        }
                    }
                }
            }

            return rooms;
        }
        /// <summary>
        /// get all reservations from the database and return them as a list
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<Reservation> GetAllReservations(string connectionString)
        {
            var reservations = new List<Reservation>();

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    Reservations.reservationID,
                    Reservations.checkInDate,
                    Reservations.checkOutDate,
                    Reservations.totalCost,
                    Reservations.reservationStatus,
                    Reservations.numberOfNights,

                    Users.userID,
                    Users.fullName,
                    Users.phoneNumber,
                    Users.email,
                    Users.username,
                    Users.password,
                    Users.isAdmin,

                    Rooms.roomID,
                    Rooms.roomType,
                    Rooms.pricePerNight,
                    Rooms.isAvailable,
                    Rooms.capacity,
                    Rooms.bedType,
                    Rooms.mealPlans,
                    Rooms.discount

                FROM Reservations
                JOIN Users ON Reservations.customerID = Users.userID
                JOIN Rooms ON Reservations.roomID = Rooms.roomID";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    // Handle User/Customer
                                    int userId = reader.GetInt32("userID");
                                    Customer customer;

                                    var existingUser = Data.Users.FirstOrDefault(u => u.UserID == userId);
                                    if (existingUser != null)
                                    {
                                        if (existingUser is Customer existingCustomer)
                                        {
                                            customer = existingCustomer;
                                        }
                                        else
                                        {
                                            throw new InvalidOperationException($"User with ID {userId} is not a Customer");
                                        }
                                    }
                                    else
                                    {
                                        customer = new Customer
                                        {
                                            UserID = userId,
                                            Fullname = reader.GetString("fullName"),
                                            PhoneNumber = reader.GetString("phoneNumber"),
                                            Email = reader.GetString("email"),
                                            Username = reader.GetString("username"),
                                            Password = reader.GetString("password"),
                                            IsAdmin = reader.GetBoolean("isAdmin")
                                        };
                                        Data.Users.Add(customer);
                                    }

                                    // Handle Room (StandardRoom or DeluxeRoom)
                                    int roomId = reader.GetInt32("roomID");                                    
                                    var reservation = new Reservation
                                    {
                                        ReservationID = reader.GetInt32("reservationID"),
                                        PCustomer = customer,
                                        RoomID = roomId,
                                        CheckInDate = reader.GetDateTime("checkInDate"),
                                        CheckOutDate = reader.GetDateTime("checkOutDate"),
                                        TotalCost = reader.GetDouble("totalCost"),
                                        ReservationStatus = (EReservationStatus)reader.GetInt32("reservationStatus"),
                                        NumberOfNights = reader.GetInt32("numberOfNights") // Added this line
                                    };

                                    reservations.Add(reservation);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error processing reservation row: {ex.Message}",
                                                  "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error getting reservations: {ex.Message}",
                              "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting reservations: {ex.Message}",
                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return reservations;
        }
        /// <summary>
        /// get all payments from the database and return them as a list
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<Payment> GetAllPayments(string connectionString)
        {
            var payments = new List<Payment>();

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    paymentID,
                    paymentDate,
                    reservationID,
                    totalAmount,
                    PaymentMethod,
                    tax
                FROM Payments";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    int paymentId = reader.GetInt32("paymentID");
                                    DateTime paymentDate = reader.GetDateTime("paymentDate");
                                    int reservationId = reader.GetInt32("reservationID");
                                    double totalAmount = reader.GetDouble("totalAmount");
                                    EPaymentMethod paymentMethod = (EPaymentMethod)reader.GetInt32("PaymentMethod");

                                    Payment payment;

                                    switch (paymentMethod)
                                    {
                                        case EPaymentMethod.Cash:
                                            payment = new CashPayment(
                                                paymentId,
                                                reservationId,
                                                paymentMethod,
                                                paymentDate,
                                                totalAmount);
                                            break;

                                        case EPaymentMethod.CreditCard:
                                            double tax = reader.GetDouble("tax");
                                            payment = new CreditCardPayment(
                                                paymentId,
                                                reservationId,
                                                paymentMethod,
                                                paymentDate,
                                                totalAmount,
                                                tax);
                                            break;

                                        default:
                                            throw new InvalidOperationException($"Unknown payment method: {paymentMethod}");
                                    }

                                    payments.Add(payment);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error processing payment row: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting payments: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return payments;
        }
        public static bool DeleteUserByID(int userId, string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"DELETE FROM Users WHERE userID = @userId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return true if exactly one row was deleted
                        // false means you are cooked (didn't delete anything or deleted more than one row affected)
                        return rowsAffected == 1;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error deleting user: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool DeleteRoomByID(int roomId, string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM Rooms WHERE roomID = @roomId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomId", roomId);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Returns true if any rows were affected (room existed and was deleted)
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error deleting room: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        /// <summary>
        /// you can use this method to update the user data,
        /// the id should be the one in the database, the other parameters is data will be set to the database        
        /// </summary>
        /// <param name="userId">the one in the database</param>
        /// <param name="NewfullName"></param>
        /// <param name="NewphoneNumber"></param>
        /// <param name="Newemail"></param>
        /// <param name="Newusername"></param>
        /// <param name="Newpassword"></param>
        /// <param name="NewisAdmin"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool UpdateUserByID(int userId, string NewfullName, string NewphoneNumber,
                            string Newemail, string Newusername, string Newpassword,
                            bool NewisAdmin, string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                UPDATE Users 
                SET 
                    fullName = @fullName,
                    phoneNumber = @phoneNumber,
                    email = @email,
                    username = @username,
                    password = @password,
                    isAdmin = @isAdmin
                WHERE userID = @userId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@fullName", NewfullName);
                        command.Parameters.AddWithValue("@phoneNumber", NewphoneNumber);
                        command.Parameters.AddWithValue("@email", Newemail);
                        command.Parameters.AddWithValue("@username", Newusername);
                        command.Parameters.AddWithValue("@password", Newpassword);
                        command.Parameters.AddWithValue("@isAdmin", NewisAdmin ? 1 : 0);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        // Returns true if exactly one row was updated
                        return rowsAffected == 1; 
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error updating user: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool UpdateRoomByID(int roomId, ERoomType NewroomType, double NewpricePerNight,
                            bool NewisAvailable, int Newcapacity, EBedType NewbedType,
                            EMealPlan NewmealPlan, string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                UPDATE Rooms 
                SET 
                    roomType = @roomType,
                    pricePerNight = @pricePerNight,
                    isAvailable = @isAvailable,
                    capacity = @capacity,
                    bedType = @bedType,
                    mealPlans = @mealPlans
                WHERE roomID = @roomId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomId", roomId);
                        command.Parameters.AddWithValue("@roomType", (int)NewroomType);
                        command.Parameters.AddWithValue("@pricePerNight", NewpricePerNight);
                        command.Parameters.AddWithValue("@isAvailable", NewisAvailable ? 1 : 0);
                        command.Parameters.AddWithValue("@capacity", Newcapacity);
                        command.Parameters.AddWithValue("@bedType", (int)NewbedType);
                        command.Parameters.AddWithValue("@mealPlans", (int)NewmealPlan);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        // Returns true if exactly one row was updated
                        return rowsAffected == 1; 
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error updating room: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static bool UpdateReservationByID(int reservationId, int NewcustomerId, int NewroomId,
                                   DateTime NewcheckInDate, DateTime NewcheckOutDate,
                                   double NewtotalCost, EReservationStatus Newstatus,
                                   string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                UPDATE Reservations 
                SET 
                    customerID = @customerId,
                    roomID = @roomId,
                    checkInDate = @checkInDate,
                    checkOutDate = @checkOutDate,
                    totalCost = @totalCost,
                    reservationStatus = @status
                WHERE reservationID = @reservationId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@reservationId", reservationId);
                        command.Parameters.AddWithValue("@customerId", NewcustomerId);
                        command.Parameters.AddWithValue("@roomId", NewroomId);
                        command.Parameters.AddWithValue("@checkInDate", NewcheckInDate);
                        command.Parameters.AddWithValue("@checkOutDate", NewcheckOutDate);
                        command.Parameters.AddWithValue("@totalCost", NewtotalCost);
                        command.Parameters.AddWithValue("@status", (int)Newstatus);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected == 1; // Returns true if exactly one row was updated
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error updating reservation: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating reservation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);  
                return false;
            }
        }
        public static List<StandardRoom> GetAvailableStandardRooms(string connectionString)
        {
            var availableRooms = new List<StandardRoom>();

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    roomID,
                    pricePerNight,
                    isAvailable,
                    capacity,
                    bedType,
                    mealPlans
                FROM Rooms
                WHERE roomType = @roomType 
                AND isAvailable = true";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomType", (int)ERoomType.Standard);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var room = new StandardRoom(
                                    reader.GetInt32("roomID"),
                                    (EBedType)reader.GetInt32("bedType"),
                                    reader.GetBoolean("isAvailable"),
                                    reader.GetInt32("capacity"),
                                    reader.GetDouble("pricePerNight"),
                                    (EMealPlan)reader.GetInt32("mealPlans"),
                                    ERoomType.Standard
                                );
                                availableRooms.Add(room);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving available standard rooms: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return availableRooms;
        }
        public static List<DeluxeRoom> GetAvailableDeluxeRooms(string connectionString)
        {
            var availableRooms = new List<DeluxeRoom>();

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = @"
                SELECT 
                    roomID,
                    pricePerNight,
                    isAvailable,
                    capacity,
                    bedType,
                    mealPlans,
                    discount
                FROM Rooms
                WHERE roomType = @roomType 
                AND isAvailable = true";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomType", (int)ERoomType.Deluxe);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var room = new DeluxeRoom(
                                    reader.GetInt32("roomID"),
                                    (EBedType)reader.GetInt32("bedType"),
                                    reader.GetBoolean("isAvailable"),
                                    reader.GetInt32("capacity"),
                                    reader.GetDouble("pricePerNight"),
                                    (EMealPlan)reader.GetInt32("mealPlans"),
                                    ERoomType.Deluxe,
                                    reader.GetDouble("discount")
                                );
                                availableRooms.Add(room);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving available deluxe rooms: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return availableRooms;
        }
    }
}
