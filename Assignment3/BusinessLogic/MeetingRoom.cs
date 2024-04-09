using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.BusinessLogic
{
    public class MeetingRoom
    {

        private string _roomNumber;

        private int _seatingCapacity;

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
        public RoomLayoutType RoomLayoutType { get; set; }

        public string RoomImageFile => $"{RoomLayoutType}".ToLower() + "_img.png";
        public string RoomTypeIcon
        {
            get { return $"{RoomLayoutType.ToString().ToLower()}_icon.png"; }
        }

        public string DisplayText
        {
            get { return $"Layout: {RoomLayoutType}, Capacity: {SeatingCapacity}"; }
        }
        public string DisplayLayout
        {
            get { return $"Layout: {RoomLayoutType}"; }
        }
        public string DisplayCapacity
        {
            get { return $"Capacity: {SeatingCapacity}"; }
        }

        public MeetingRoom(string roomNumber, int seatingCapacity, RoomLayoutType roomLayoutType)
        {
            RoomNumber = roomNumber;
            SeatingCapacity = seatingCapacity;
            RoomLayoutType = roomLayoutType;
        }
    }
}
