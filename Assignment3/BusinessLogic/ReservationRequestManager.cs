using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.DocumentsContract;

namespace Assignment3.BusinessLogic
{
    public class ReservationRequestManager
    {
        private List<MeetingRoom> _meetingRooms = new List<MeetingRoom>();
        private List<ReservationRequest> _reservationRequests = new List<ReservationRequest>();

        public void InitializeMeetingRooms()
        {
            AddMeetingRoom("A102", 20, "hollowsquare_room.png", RoomLayoutType.HollowSquare);
            AddMeetingRoom("B103", 20, "ushape_room.png", RoomLayoutType.Ushape);
            AddMeetingRoom("C202", 40, "classroom_room.png", RoomLayoutType.Classroom);
            AddMeetingRoom("C105", 200, "auditorium_room.png", RoomLayoutType.Auditorium);
        }
        public List<MeetingRoom> MeetingRooms => _meetingRooms;

        public List<ReservationRequest> ReservationRequests => _reservationRequests;

        public void AddMeetingRoom(string roomNumber, int seatingCapacity, string roomImageFile, RoomLayoutType roomLayoutType)
        {
            MeetingRoom foundRoom = _meetingRooms.FirstOrDefault(room => room.RoomNumber == tempRoom.RoomNumber);
            if (foundRoom == null)
            {
                throw new ArgumentException("A room with the same room number already exists. ");
            }

            var newRoom = new MeetingRoom(roomNumber, seatingCapacity, roomLayoutType);
            _meetingRooms.Add(newRoom);
        }
        public void AddReservationRequest(string requestedBy, string description, DateTime meetingDate, DateTime startTime, DateTime endTime, int participantCount, MeetingRoom tempRoom)
        {
            MeetingRoom foundRoom = _meetingRooms.FirstOrDefault(room => room.RoomNumber == tempRoom.RoomNumber);

            if (foundRoom == null)
            {
                throw new ArgumentException("The requested room does not exist.");
            }
            if (participantCount > foundRoom.SeatingCapacity)
            {
                throw new ArgumentException("The requested room cannot accommodate the number of participants.");
            }

            ReservationRequest newRequest = new ReservationRequest(requestedBy, description, meetingDate, startTime, endTime, participantCount)
            {
                MeetingRoom = foundRoom
            };
            _reservationRequests.Add(newRequest);
        }

        public bool IsRoomAvailable(MeetingRoom meetingRoom, DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime)
            {
                Console.WriteLine($"Invalid time range: Start time {startTime} is not before end time {endTime}.");
                throw new ArgumentException("End time must be after start time.");
            }

            foreach (var reservation in _reservationRequests)
            {
                if (reservation.MeetingRoom.RoomNumber == meetingRoom.RoomNumber)
                {
                    Console.WriteLine($"Checking against reservation: {reservation.StartTime} to {reservation.EndTime}");

                    if (!(reservation.EndTime <= startTime || reservation.StartTime >= endTime))
                    {
                        Console.WriteLine($"Overlap found: {reservation.StartTime} to {reservation.EndTime} conflicts with {startTime} to {endTime}");
                        return false;
                    }
                }
            }
            return true;
        }

        public void ChangeReservationRequestStatus(int requestId, RequestStatus newStatus)
        {
            var request = _reservationRequests.FirstOrDefault(r => r.RequestID == requestId);
            if (request != null)
            {
                request.Status = newStatus;
            }
            else
            {
                throw new ArgumentException("Reservation request with the given ID not found.");
            }
        }

        public void AcceptReservationRequest(ReservationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");
            }

            if (request.StartTime >= request.EndTime)
            {
                throw new ArgumentException($"The start time must be before the end time. Start time: {request.StartTime}, End time: {request.EndTime}.");
            }

            if (!IsRoomAvailable(request.MeetingRoom, request.StartTime, request.EndTime))
            {
                throw new InvalidOperationException($"The room {request.MeetingRoom.RoomNumber} is not available from {request.StartTime:yyyy-MM-dd HH:mm} to {request.EndTime:yyyy-MM-dd HH:mm}.");
            }

            ChangeReservationRequestStatus(request.RequestID, RequestStatus.Accepted);
        }
    }
}
    

