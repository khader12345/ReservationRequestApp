using Assignment3.BusinessLogic;

namespace Assignment3.Pages;

public partial class AddRequestPage : ContentPage
{
    private MeetingRoom _selectedRoom;
    private ReservationRequestManager _reservationRequestManager;

    public AddRequestPage(MeetingRoom selectedRoom, ReservationRequestManager reservationRequest)
    {
        InitializeComponent();
        _selectedRoom = selectedRoom;
        _reservationRequestManager = reservationRequest;
        this.BindingContext = _selectedRoom;
    }

    private void OnAddRequestClicked(object sender, EventArgs e)
    {
        try
        {
            string requestedBy = UserNameEntry.Text;
            string description = DescriptionEntry.Text;
            DateTime meetingDate = MeetingDatePicker.Date;
            DateTime startTime = MeetingDatePicker.Date + StartTimePicker.Time;
            DateTime endTime = MeetingDatePicker.Date + EndTimePicker.Time;
            int participantCount = int.Parse(ParticipantCountEntry.Text);
            MeetingRoom tempRoom = _selectedRoom;

            if (participantCount > _selectedRoom.SeatingCapacity)
            {
                DisplayAlert("Error", "The selected room cannot accommodate the number of participants.", "OK");
                return;
            }

            _reservationRequestManager.AddReservationRequest(requestedBy, description, meetingDate, startTime, endTime, participantCount, tempRoom);
            DisplayAlert("Success", "Your reservation request has been added.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error while making request", ex.Message, "Ok");
        }
    }

    private void OnBackToRoomsClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}