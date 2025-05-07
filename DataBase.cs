using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
                    return true;
                }
                catch (MySqlException ex)
                {
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
                string query = "SELECT 1 FROM Users WHERE username = @username AND password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                var result = command.ExecuteScalar();
                return result != null;
            }
        }

        /// <summary>
        /// is called when we add a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static bool CheckUsername(string username, string connectionString)
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
                Console.WriteLine("Username already exists. Please choose a different username.");
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
                Console.WriteLine($"Error adding user: {ex.Message}");
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
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool AddReservation(int customerID, int roomID,
                                DateTime checkInDate, DateTime checkOutDate,
                                double totalCost, EReservationStatus status,
                                string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Removed reservationID from columns and values since it's auto-incremented
                    string query = @"INSERT INTO Reservations 
                          (customerID, roomID, checkInDate, checkOutDate, totalCost, reservationStatus)
                          VALUES 
                          (@customerID, @roomID, @checkInDate, @checkOutDate, @totalCost, @reservationStatus)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Removed reservationID parameter
                        command.Parameters.AddWithValue("@customerID", customerID);
                        command.Parameters.AddWithValue("@roomID", roomID);
                        command.Parameters.AddWithValue("@checkInDate", checkInDate);
                        command.Parameters.AddWithValue("@checkOutDate", checkOutDate);
                        command.Parameters.AddWithValue("@totalCost", totalCost);
                        command.Parameters.AddWithValue("@reservationStatus", (int)status);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error adding reservation: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding reservation: {ex.Message}");
                return false;
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
        /// <returns></returns>
        public static bool AddRoom(ERoomType roomType, double pricePerNight, bool isAvailable,
                         int capacity, EBedType bedType, EMealPlan mealPlan,
                         string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Removed roomID from columns and values since it's auto-incremented
                    string query = @"INSERT INTO Rooms 
                          (roomType, pricePerNight, isAvailable, capacity, bedType, mealPlans)
                          VALUES 
                          (@roomType, @pricePerNight, @isAvailable, @capacity, @bedType, @mealPlans)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Removed roomID parameter
                        command.Parameters.AddWithValue("@roomType", (int)roomType);
                        command.Parameters.AddWithValue("@pricePerNight", pricePerNight);
                        command.Parameters.AddWithValue("@isAvailable", isAvailable ? 1 : 0);
                        command.Parameters.AddWithValue("@capacity", capacity);
                        command.Parameters.AddWithValue("@bedType", (int)bedType);
                        command.Parameters.AddWithValue("@mealPlans", (int)mealPlan);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database error adding room: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding room: {ex.Message}");
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
                Console.WriteLine($"Database error adding payment: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding payment: {ex.Message}");
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
                                    Customer customer = null;

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
                                    Room room = null;
                                    ERoomType roomType = (ERoomType)reader.GetInt32("roomType");

                                    var existingRoom = Data.Rooms.FirstOrDefault(r => r.RoomID == roomId);
                                    if (existingRoom != null)
                                    {
                                        // Verify the existing room matches the expected type
                                        bool typeMatches = (roomType == ERoomType.Standard && existingRoom is StandardRoom) ||
                                                         (roomType == ERoomType.Deluxe && existingRoom is DeluxeRoom);

                                        if (typeMatches)
                                        {
                                            room = existingRoom;
                                        }
                                        else
                                        {
                                            throw new InvalidOperationException(
                                                $"Room type mismatch for room ID {roomId}. Expected {roomType} but found {existingRoom.GetType().Name}");
                                        }
                                    }
                                    else
                                    {
                                        EBedType bedType = (EBedType)reader.GetInt32("bedType");
                                        EMealPlan mealPlan = (EMealPlan)reader.GetInt32("mealPlans");

                                        switch (roomType)
                                        {
                                            case ERoomType.Standard:
                                                room = new StandardRoom(
                                                    roomId,
                                                    bedType,
                                                    reader.GetBoolean("isAvailable"),
                                                    reader.GetInt32("capacity"),
                                                    reader.GetDouble("pricePerNight"),
                                                    mealPlan,
                                                    roomType
                                                );
                                                break;
                                            case ERoomType.Deluxe:
                                                room = new DeluxeRoom(
                                                    roomId,
                                                    bedType,
                                                    reader.GetBoolean("isAvailable"),
                                                    reader.GetInt32("capacity"),
                                                    reader.GetDouble("pricePerNight"),
                                                    mealPlan,
                                                    roomType,
                                                    reader.GetDouble("discount")
                                                );
                                                break;
                                            default:
                                                throw new InvalidOperationException("Unknown room type.");
                                        }
                                        Data.Rooms.Add(room);
                                    }

                                    // Create reservation
                                    var reservation = new Reservation
                                    {
                                        ReservationID = reader.GetInt32("reservationID"),
                                        PCustomer = customer,
                                        PRoom = room,
                                        CheckInDate = reader.GetDateTime("checkInDate"),
                                        CheckOutDate = reader.GetDateTime("checkOutDate"),
                                        TotalCost = reader.GetDouble("totalCost"),
                                        ReservationStatus = (EReservationStatus)reader.GetInt32("reservationStatus")
                                    };

                                    reservations.Add(reservation);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error processing reservation row: {ex.Message}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting reservations: {ex.Message}");
                throw;
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
                                    Console.WriteLine($"Error processing payment row: {ex.Message}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting payments: {ex.Message}");
                throw;
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
                Console.WriteLine($"Database error deleting user: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
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
                Console.WriteLine($"Database error deleting room: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting room: {ex.Message}");
                return false;
            }
        }
    }
}
