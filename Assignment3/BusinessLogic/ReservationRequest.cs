using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.BusinessLogic
{
    public class ReservationRequest
    {
        private static int _IDassigned = 1;
        private int _requestID;
        private string _requestedBy;
        private string _description;
        private DateTime _startTime;
        private DateTime _endTime;
        private int _participantCount;
        private MeetingRoom _meetingRoom;
        private DateTime _meetingDate;
        private RequestStatus _status = RequestStatus.Pending;

        public RequestStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int RequestID => _requestID;

        public DateTime MeetingDate
        {
            get => _meetingDate;
            set { _meetingDate = value; }
        }

        public string RequestedBy
        {
            get => _requestedBy;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("A name for the request is required. ");
                }
                _requestedBy = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("A description must be provided. ");
                }
                _description = value;
            }
        }
        
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if(value <= DateTime.Now) 
                {
                    throw new ArgumentException("The meeting time has to be in the future. ");
                
                }
                _startTime = value;
            }
        }
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if(value <= _startTime)
                {
                    throw new ArgumentException("The meeting end time must be greater than the start time. ");  
                }
                _endTime = value;
            }
        }

        public int ParticipantCount
        {
            get => _participantCount;
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentException("Number of participants must be more than 0. ");
                }
                _participantCount = value;
            }
        }

        public MeetingRoom MeetingRoom
        {
            get => _meetingRoom;
            set { _meetingRoom = value; }
        }

        public ReservationRequest(string requestedBy, string description, DateTime meetingDate, DateTime startTime, DateTime endTime, int participantCount)
        {
            _requestID = _IDassigned++;
            RequestedBy = requestedBy;
            Description = description;
            MeetingDate = meetingDate;
            StartTime = startTime;
            EndTime = endTime;
            ParticipantCount = participantCount;
            MeetingRoom = null;
        }
    }
}
