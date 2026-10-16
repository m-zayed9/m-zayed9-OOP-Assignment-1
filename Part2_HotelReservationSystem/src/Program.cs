namespace Part2_HotelReservationSystem;

public class Program
{
    public static void Main()
    {
        Guest ahmed = new Guest(1, "Ahmed Samir", "01000000000");
        Room room101 = new Room("101", RoomType.Double, 800m);
        Room room202 = new Room("202", RoomType.Suite, 2000m);

    
        Reservation r1 = ahmed.MakeReservation(1, room101, new DateTime(2026, 11, 1), new DateTime(2026, 11, 4));
        Console.WriteLine("Created reservation " + r1.ReservationId + ", status " + r1.Status);
        Console.WriteLine("Nights: " + r1.Nights + ", total cost: " + r1.TotalCost);

        r1.Confirm();
        r1.CheckIn();
        r1.CheckOut();
        Console.WriteLine("After the stay, status: " + r1.Status);

        room101.ChangeNightlyRate(900m);
        Console.WriteLine("New total with the new rate: " + r1.TotalCost);

        try
        {
            r1.CheckIn();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Rejected: " + ex.Message);
        }

        try
        {
            ahmed.MakeReservation(2, room101, new DateTime(2026, 12, 5), new DateTime(2026, 12, 5));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Rejected: " + ex.Message);
        }

        room202.StartMaintenance();
        try
        {
            ahmed.MakeReservation(3, room202, new DateTime(2026, 12, 1), new DateTime(2026, 12, 3));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Rejected: " + ex.Message);
        }
        room202.EndMaintenance();

        try
        {
            room101.ChangeNightlyRate(-5m);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Rejected: " + ex.Message);
        }

        Reservation r4 = ahmed.MakeReservation(4, room202, new DateTime(2026, 12, 10), new DateTime(2026, 12, 15));
        Guest mona = new Guest(2, "Mona Ali", "01111111111");
        try
        {
            mona.MakeReservation(5, room202, new DateTime(2026, 12, 12), new DateTime(2026, 12, 14));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Rejected: " + ex.Message);
        }

        r4.Cancel();
        mona.MakeReservation(5, room202, new DateTime(2026, 12, 12), new DateTime(2026, 12, 14));
        Console.WriteLine("Mona booked room 202 after the cancellation.");

        Console.WriteLine("Ahmed has " + ahmed.Reservations.Count + " reservations in his history.");
    }
}
