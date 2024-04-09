using System;

namespace Assignment3.BusinessLogic
{
    // Represents a physical meeting room within a facility.
    public class MeetingRoom
    {
        // Backing field for the room number property.
        private string _roomNumber;

        // Backing field for the seating capacity property.
        private int _seatingCapacity;

        // Public property for the room number with validation to ensure it's not null or empty.
        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("A room number must be provided");
                }
                _roomNumber = value;
            }
        }

        // Public property for the seating capacity with validation to ensure it's more than 0.
        public int SeatingCapacity
        {
            get { return _seatingCapacity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Seating capacity should be more than 0");
                }
                _seatingCapacity = value;
            }
        }

        // Auto-implemented property for the layout type of the room.
        public RoomLayoutType RoomLayoutType { get; set; }

        // Read-only property to get the room image file name based on room layout type.
        public string RoomImageFile => $"{RoomLayoutType}".ToLower() + "_img.png";

        // Read-only property to get the room type icon file name based on room layout type.
        public string RoomTypeIcon
        {
            get { return $"{RoomLayoutType.ToString().ToLower()}_icon.png"; }
        }

        // Read-only property to display a formatted string with layout and capacity.
        public string DisplayText
        {
            get { return $"Layout: {RoomLayoutType}, Capacity: {SeatingCapacity}"; }
        }

        // Read-only property to display a formatted string with the layout type.
        public string DisplayLayout
        {
            get { return $"Layout: {RoomLayoutType}"; }
        }

        // Read-only property to display a formatted string with the seating capacity.
        public string DisplayCapacity
        {
            get { return $"Capacity: {SeatingCapacity}"; }
        }

        // Constructor to create a new MeetingRoom instance with provided details.
        // Validates and sets room number, seating capacity, and layout type.
        public MeetingRoom(string roomNumber, int seatingCapacity, RoomLayoutType roomLayoutType)
        {
            RoomNumber = roomNumber; // Sets the room number using the property to ensure validation.
            SeatingCapacity = seatingCapacity; // Sets the seating capacity using the property to ensure validation.
            RoomLayoutType = roomLayoutType; // Sets the layout type directly as there's no validation needed.
        }
    }
}
