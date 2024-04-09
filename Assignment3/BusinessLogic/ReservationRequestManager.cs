using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment3.BusinessLogic
{
    
    /// Manages meeting rooms and reservation requests.
    
    public class ReservationRequestManager
    {
        // List to store meeting rooms.
        private List<MeetingRoom> _meetingRooms = new List<MeetingRoom>();
        // List to store reservation requests.
        private List<ReservationRequest> _reservationRequests = new List<ReservationRequest>();

        
        /// Initializes the meeting rooms with predefined data.
        
        public void InitializeMeetingRooms()
        {
            // Hard-coded initialization of meeting rooms.
            AddMeetingRoom("A102", 20, RoomLayoutType.HollowSquare);
            AddMeetingRoom("B103", 20, RoomLayoutType.Ushape);
            AddMeetingRoom("C202", 40, RoomLayoutType.Classroom);
            AddMeetingRoom("C105", 200, RoomLayoutType.Auditorium);
        }

        // Provides public access to the list of meeting rooms.
        public List<MeetingRoom> MeetingRooms => _meetingRooms;

        // Provides public access to the list of reservation requests.
        public List<ReservationRequest> ReservationRequests => _reservationRequests;

        
        /// Adds a new meeting room to the list if it doesn't already exist.
        
        /// <param name="roomNumber">The room number.</param>
        /// <param name="seatingCapacity">The seating capacity of the room.</param>
        /// <param name="roomLayoutType">The layout of the room.</param>
        public void AddMeetingRoom(string roomNumber, int seatingCapacity, RoomLayoutType roomLayoutType)
        {
            // Checks if the room already exists by room number.
            if (_meetingRooms.Any(room => room.RoomNumber == roomNumber))
            {
                throw new ArgumentException("A room number duplicate has been detected. ");
            }

            // Creates a new room and adds it to the list.
            var newRoom = new MeetingRoom(roomNumber, seatingCapacity, roomLayoutType);
            _meetingRooms.Add(newRoom);
        }

        
        /// Adds a new reservation request after validating the room and capacity.
        
        /// <param name="requestedBy">The person who made the request.</param>
        /// <param name="description">A description of the meeting.</param>
        /// <param name="meetingDate">The date of the meeting.</param>
        /// <param name="startTime">The starting time of the meeting.</param>
        /// <param name="endTime">The ending time of the meeting.</param>
        /// <param name="participantCount">The number of participants.</param>
        /// <param name="tempRoom">The temporary room object for validation.</param>
        public void AddReservationRequest(string requestedBy, string description, DateTime meetingDate, DateTime startTime, DateTime endTime, int participantCount, MeetingRoom tempRoom)
        {
            // Checks if the room exists in the system.
            MeetingRoom foundRoom = _meetingRooms.FirstOrDefault(room => room.RoomNumber == tempRoom.RoomNumber);

            // Validation of room existence.
            if (foundRoom == null)
            {
                throw new ArgumentException("The specified room could not be found. ");
            }

            // Validation of room capacity against participant count.
            if (participantCount > foundRoom.SeatingCapacity)
            {
                throw new ArgumentException("The selected room lacks sufficient capacity for the expected attendees. ");
            }

            // Create and add the new reservation request.
            ReservationRequest newRequest = new ReservationRequest(requestedBy, description, meetingDate, startTime, endTime, participantCount)
            {
                MeetingRoom = foundRoom
            };
            _reservationRequests.Add(newRequest);
        }

        
        /// Checks if the room is available during the given time.
        
        /// <param name="meetingRoom">The meeting room to check.</param>
        /// <param name="startTime">The start time of the desired reservation.</param>
        /// <param name="endTime">The end time of the desired reservation.</param>
        /// <returns>True if the room is available; false otherwise.</returns>
        public bool IsRoomAvailable(MeetingRoom meetingRoom, DateTime startTime, DateTime endTime)
        {
            // Time range validation.
            if (endTime <= startTime)
            {
                throw new ArgumentException("The end time should come later than the start time. ");
            }

            // Check for time conflicts with existing reservations.
            foreach (var reservation in _reservationRequests)
            {
                if (reservation.MeetingRoom.RoomNumber == meetingRoom.RoomNumber &&
                    !(reservation.EndTime <= startTime || reservation.StartTime >= endTime))
                {
                    return false; // Found a time conflict.
                }
            }
            return true; // No conflicts found; the room is available.
        }

        
        /// Changes the status of a specific reservation request.
        
        /// <param name="requestId">The unique identifier of the reservation request.</param>
        /// <param name="newStatus">The new status to set for the reservation request.</param>
        public void ChangeReservationRequestStatus(int requestId, RequestStatus newStatus)
        {
            // Find the reservation request by ID.
            var request = _reservationRequests.FirstOrDefault(r => r.RequestID == requestId);
            if (request != null)
            {
                request.Status = newStatus; // Update the status.
            }
            else
            {
                throw new ArgumentException("No reservation request matches the provided ID. ");
            }
        }

        
        /// Accepts a reservation request if the room is available.
        
        /// <param name="request">The reservation request to accept.</param>
        public void AcceptReservationRequest(ReservationRequest request)
        {
            // Validation of the request object.
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Invalid input");
            }

            // Start time must be before the end time.
            if (request.StartTime >= request.EndTime)
            {
                throw new ArgumentException("The start time needs to precede the end time. ");
            }

            // Room must be available during the requested time.
            if (!IsRoomAvailable(request.MeetingRoom, request.StartTime, request.EndTime))
            {
                throw new InvalidOperationException($"The room {request.MeetingRoom.RoomNumber} is not available from {request.StartTime:yyyy-MM-dd HH:mm} to {request.EndTime:yyyy-MM-dd HH:mm}.");
            }

            // Change the status of the reservation request to accepted.
            ChangeReservationRequestStatus(request.RequestID, RequestStatus.Accepted);
        }
    }
}
