public class Booking
{
    public int BookingID { get; set; }
    public int RoomID { get; set; }
    public int CustomerID { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal EstimatedTotal { get; set; }
    public decimal ActualTotal { get; set; }
}