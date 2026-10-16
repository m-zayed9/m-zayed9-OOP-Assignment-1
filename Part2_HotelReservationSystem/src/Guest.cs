namespace Part2_HotelReservationSystem;

public class Guest
{
    private readonly List<Reservation> _reservations = new List<Reservation>();

    public int GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations;

    public Guest(int guestId, string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Guest full name cannot be empty.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Guest phone number cannot be empty.");

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public Reservation MakeReservation(int reservationId, Room room, DateTime checkIn, DateTime checkOut)
    {
        Reservation reservation = new Reservation(reservationId, room, checkIn, checkOut);
        _reservations.Add(reservation);
        return reservation;
    }
}
