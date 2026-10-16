namespace Part2_HotelReservationSystem;

public class Reservation
{
    public int ReservationId { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public Room Room { get; }                       
    public ReservationStatus Status { get; private set; }
    public int Nights => (CheckOutDate - CheckInDate).Days;
    public decimal TotalCost => Nights * Room.NightlyRate;

    public bool IsActive => Status != ReservationStatus.Cancelled && Status != ReservationStatus.CheckedOut;

    internal Reservation(int reservationId, Room room, DateTime checkIn, DateTime checkOut)
    {
        if (room == null)
            throw new ArgumentNullException(nameof(room), "A reservation needs a room.");

        if (checkOut.Date <= checkIn.Date)
            throw new ArgumentException("Check-out date must be after the check-in date.");

        if (room.IsUnderMaintenance)
            throw new InvalidOperationException("Room " + room.RoomNumber + " is under maintenance and cannot be booked.");

        if (room.HasConflict(checkIn, checkOut))
            throw new InvalidOperationException("Room " + room.RoomNumber + " is already booked for these dates.");

        ReservationId = reservationId;
        CheckInDate = checkIn.Date;
        CheckOutDate = checkOut.Date;
        Room = room;
        Status = ReservationStatus.Pending;

        room.AddReservation(this);
    }

    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new InvalidOperationException("Only a Pending reservation can be confirmed (current: " + Status + ").");

        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException("Only a Confirmed reservation can be checked in (current: " + Status + ").");

        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        if (Status != ReservationStatus.CheckedIn)
            throw new InvalidOperationException("Only a CheckedIn reservation can be checked out (current: " + Status + ").");

        Status = ReservationStatus.CheckedOut;
    }

    public void Cancel()
    {
        if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
            throw new InvalidOperationException("Only a Pending or Confirmed reservation can be cancelled (current: " + Status + ").");

        Status = ReservationStatus.Cancelled;
    }
}
