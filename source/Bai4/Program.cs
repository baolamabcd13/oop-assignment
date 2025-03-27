class Program
{
    static void Main(string[] args)
    {
        HotelManagement hotelManagement = new HotelManagement();

        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Book Room");
            Console.WriteLine("4. Check Out");
            Console.WriteLine("5. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    hotelManagement.AddRoom();
                    break;
                case 2:
                    hotelManagement.AddCustomer();
                    break;
                case 3:
                    hotelManagement.BookRoom();
                    break;
                case 4:
                    hotelManagement.CheckOut();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}