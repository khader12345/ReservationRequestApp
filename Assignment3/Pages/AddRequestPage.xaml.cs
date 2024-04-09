using Assignment3.BusinessLogic;

namespace Assignment3.Pages;

// Defines the AddRequestPage which is responsible for creating a new reservation request
public partial class AddRequestPage : ContentPage
{
    // Holds the information about the selected meeting room
    private MeetingRoom _selectedRoom;

    // Manages the reservation requests
    private ReservationRequestManager _reservationRequestManager;

    // Constructor for AddRequestPage that initializes the room and reservation request manager
    public AddRequestPage(MeetingRoom selectedRoom, ReservationRequestManager reservationRequest)
    {
        InitializeComponent(); // Initializes the component from XAML
        _selectedRoom = selectedRoom; // Set the selected meeting room
        _reservationRequestManager = reservationRequest; // Set the reservation request manager
        this.BindingContext = _selectedRoom; // Sets the binding context to the selected room for data binding
    }

    // Event handler for when the "Add Request" button is clicked
    private void OnAddRequestClicked(object sender, EventArgs e)
    {
        try
        {
            // Retrieve input values from the form
            string requestedBy = UserNameEntry.Text;
            string description = DescriptionEntry.Text;
            DateTime meetingDate = MeetingDatePicker.Date;
            DateTime startTime = MeetingDatePicker.Date + StartTimePicker.Time;
            DateTime endTime = MeetingDatePicker.Date + EndTimePicker.Time;
            int participantCount = int.Parse(ParticipantCountEntry.Text);
            MeetingRoom tempRoom = _selectedRoom;

            // Validate if the participant count does not exceed the room's capacity
            if (participantCount > _selectedRoom.SeatingCapacity)
            {
                DisplayAlert("Error", "The chosen room does not have capacity for the expected number of attendees.", "OK");
                return; // Exit the event handler if the validation fails
            }

            // Add the reservation request using the provided details
            _reservationRequestManager.AddReservationRequest(requestedBy, description, meetingDate, startTime, endTime, participantCount, tempRoom);
            // Notify the user that the request has been added
            DisplayAlert("Completed", "Your reservation has been successfully registered. ", "OK");
        }
        catch (Exception ex)
        {
            // If an error occurs, show an error message
            DisplayAlert("Error transpired while making request", ex.Message, "OK");
        }
    }

    // Event handler for when the "Back To Rooms" button is clicked
    private void OnBackToRoomsClicked(object sender, EventArgs e)
    {
        // Navigates back to the previous page in the navigation stack
        Navigation.PopAsync();
    }
}
