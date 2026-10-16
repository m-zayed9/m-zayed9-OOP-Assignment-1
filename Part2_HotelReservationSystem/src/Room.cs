namespace Part2_HotelReservationSystem;

public class Room
{
    private readonly List<Reservation> _reservations = new List<Reservation>();

    public string RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public IReadOnlyList<Reservation> Reservations => _reservations;

    public Room(string roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new ArgumentException("Room number cannot be empty.");

        RoomNumber = roomNumber;
        RoomType = roomType;
        ChangeNightlyRate(nightlyRate);
    }

    public void ChangeNightlyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("Nightly rate must be greater than zero.");

        NightlyRate = newRate;
    }

    public void StartMaintenance()
    {
        if (IsUnderMaintenance)
            throw new InvalidOperationException("Room " + RoomNumber + " is already under maintenance.");

        IsUnderMaintenance = true;
    }

    public void EndMaintenance()
    {
        if (!IsUnderMaintenance)
            throw new InvalidOperationException("Room " + RoomNumber + " is not under maintenance.");

        IsUnderMaintenance = false;
    }

    public bool HasConflict(DateTime checkIn, DateTime checkOut)
    {
        foreach (Reservation existing in _reservations)
        {
            if (!existing.IsActive)
                continue;   

            bool overlaps = checkIn.Date < existing.CheckOutDate && existing.CheckInDate < checkOut.Date;
            if (overlaps)
                return true;
        }

        return false;
    }
    internal void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }
}
