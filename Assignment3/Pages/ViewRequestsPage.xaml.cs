using Assignment3.BusinessLogic;
using System.Collections.ObjectModel;

namespace Assignment3.Pages;

// Defines the page where users can view and manage reservation requests
public partial class ViewRequestsPage : ContentPage
{
    // The currently selected meeting room for which requests are being viewed
    private MeetingRoom _selectedRoom;
    // The manager handling all reservation requests and meeting room data
    private ReservationRequestManager _reservationRequestManager;
    // Collection of reservation requests to be displayed in the UI
    private ObservableCollection<ReservationRequest> _requestsForDisplay = new ObservableCollection<ReservationRequest>();

    // Constructor initializing the page with a selected meeting room and the reservation request manager
    public ViewRequestsPage(MeetingRoom selectedRoom, ReservationRequestManager reservationRequestManager)
    {
        InitializeComponent();
        _selectedRoom = selectedRoom;
        _reservationRequestManager = reservationRequestManager;
        PopulateRequestsForDisplay();
        RequestsListView.ItemsSource = _requestsForDisplay;
    }

    // Populates the ObservableCollection with reservation requests for the selected meeting room
    private void PopulateRequestsForDisplay()
    {
        _requestsForDisplay.Clear();
        foreach (var res in _reservationRequestManager.ReservationRequests)
        {
            if (res.MeetingRoom.RoomNumber == _selectedRoom.RoomNumber)
            {
                _requestsForDisplay.Add(res);
            }
        }
    }

    // Navigates back to the room selection page when the back button is clicked
    private void OnBackToRoomsClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    // Accepts the selected reservation request when the accept button is clicked
    private void OnAcceptClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is ReservationRequest request)
        {
            try
            {
                _reservationRequestManager.AcceptReservationRequest(request);
                PopulateRequestsForDisplay();
                DisplayAlert("Success", "Request has been accepted.", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    // Rejects the selected reservation request when the reject button is clicked
    private void OnRejectClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is ReservationRequest request)
        {
            try
            {
                _reservationRequestManager.ChangeReservationRequestStatus(request.RequestID, RequestStatus.Rejected);
                PopulateRequestsForDisplay();
                DisplayAlert("Success", "Request has been rejected.", "OK");

            }
            catch (Exception ex)
            {
                DisplayAlert("Success", "Request has rejected. ", "OK");
            }
        }
    }
}





