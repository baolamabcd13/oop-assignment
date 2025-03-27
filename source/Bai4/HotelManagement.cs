using Npgsql;
using System;

public class HotelManagement
{
    private string connectionString = "Host=localhost;Username=postgres;Password=your_password;Database=HotelManagement";

    public void AddRoom()
    {
        Console.Write("Enter room type: ");
        string roomType = Console.ReadLine();
        Console.Write("Enter price per day: ");
        decimal pricePerDay = decimal.Parse(Console.ReadLine());

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var checkCmd = new NpgsqlCommand("SELECT COUNT(*) FROM Room WHERE RoomType = @roomType AND PricePerDay = @pricePerDay", conn))
                {
                    checkCmd.Parameters.AddWithValue("roomType", roomType);
                    checkCmd.Parameters.AddWithValue("pricePerDay", pricePerDay);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Console.WriteLine("Room already exists.");
                        return;
                    }
                }

                using (var cmd = new NpgsqlCommand("INSERT INTO Room (RoomType, PricePerDay, Status) VALUES (@roomType, @pricePerDay, 'Available')", conn))
                {
                    cmd.Parameters.AddWithValue("roomType", roomType);
                    cmd.Parameters.AddWithValue("pricePerDay", pricePerDay);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Room added successfully!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void AddCustomer()
    {
        Console.Write("Enter customer name: ");
        string name = Console.ReadLine();
        Console.Write("Enter phone: ");
        string phone = Console.ReadLine();
        Console.Write("Enter address: ");
        string address = Console.ReadLine();

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var checkCmd = new NpgsqlCommand("SELECT COUNT(*) FROM Customer WHERE Name = @name AND Phone = @phone", conn))
                {
                    checkCmd.Parameters.AddWithValue("name", name);
                    checkCmd.Parameters.AddWithValue("phone", phone);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Console.WriteLine("Customer already exists.");
                        return;
                    }
                }

                using (var cmd = new NpgsqlCommand("INSERT INTO Customer (Name, Phone, Address) VALUES (@name, @phone, @address)", conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("phone", phone);
                    cmd.Parameters.AddWithValue("address", address);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Customer added successfully!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public bool RoomExists(int roomId)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM Room WHERE RoomID = @roomId", conn))
            {
                cmd.Parameters.AddWithValue("roomId", roomId);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }
    }

    public bool CustomerExists(int customerId)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM Customer WHERE CustomerID = @customerId", conn))
            {
                cmd.Parameters.AddWithValue("customerId", customerId);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }
    }

    public void BookRoom()
    {
        Console.Write("Enter room ID: ");
        int roomId = int.Parse(Console.ReadLine());
        Console.Write("Enter customer ID: ");
        int customerId = int.Parse(Console.ReadLine());

        if (!RoomExists(roomId))
        {
            Console.WriteLine("Room does not exist.");
            return;
        }

        if (!CustomerExists(customerId))
        {
            Console.WriteLine("Customer does not exist.");
            return;
        }

        Console.Write("Enter check-in date (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime checkInDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }

        Console.Write("Enter check-out date (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime checkOutDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT PricePerDay FROM Room WHERE RoomID = @roomId", conn))
                {
                    cmd.Parameters.AddWithValue("roomId", roomId);
                    var result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        Console.WriteLine("Room does not exist.");
                        return;
                    }

                    decimal pricePerDay = (decimal)result;
                    decimal estimatedTotal = (checkOutDate - checkInDate).Days * pricePerDay;

                    using (var insertCmd = new NpgsqlCommand("INSERT INTO Booking (RoomID, CustomerID, CheckInDate, CheckOutDate, EstimatedTotal) VALUES (@roomId, @customerId, @checkInDate, @checkOutDate, @estimatedTotal)", conn))
                    {
                        insertCmd.Parameters.AddWithValue("roomId", roomId);
                        insertCmd.Parameters.AddWithValue("customerId", customerId);
                        insertCmd.Parameters.AddWithValue("checkInDate", checkInDate);
                        insertCmd.Parameters.AddWithValue("checkOutDate", checkOutDate);
                        insertCmd.Parameters.AddWithValue("estimatedTotal", estimatedTotal);
                        insertCmd.ExecuteNonQuery();
                        Console.WriteLine("Room booked successfully!");
                    }

                    using (var updateCmd = new NpgsqlCommand("UPDATE Room SET Status = 'Booked' WHERE RoomID = @roomId", conn))
                    {
                        updateCmd.Parameters.AddWithValue("roomId", roomId);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void CheckOut()
    {
        Console.Write("Enter booking ID: ");
        int bookingId = int.Parse(Console.ReadLine());
        Console.Write("Enter additional services cost: ");
        decimal servicesCost = decimal.Parse(Console.ReadLine());

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT RoomID FROM Booking WHERE BookingID = @bookingId", conn))
                {
                    cmd.Parameters.AddWithValue("bookingId", bookingId);
                    var roomIdResult = cmd.ExecuteScalar();

                    if (roomIdResult == null)
                    {
                        Console.WriteLine("Booking does not exist.");
                        return;
                    }

                    using (var updateCmd = new NpgsqlCommand("UPDATE Booking SET ActualTotal = EstimatedTotal + @servicesCost WHERE BookingID = @bookingId", conn))
                    {
                        updateCmd.Parameters.AddWithValue("bookingId", bookingId);
                        updateCmd.Parameters.AddWithValue("servicesCost", servicesCost);
                        updateCmd.ExecuteNonQuery();
                        Console.WriteLine("Checked out successfully!");
                    }

                    using (var roomUpdateCmd = new NpgsqlCommand("UPDATE Room SET Status = 'Available' WHERE RoomID = @roomId", conn))
                    {
                        roomUpdateCmd.Parameters.AddWithValue("roomId", (int)roomIdResult);
                        roomUpdateCmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}