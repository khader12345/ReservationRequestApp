using Assignment3.BusinessLogic;
using System.Collections.ObjectModel;

namespace Assignment3.Pages;

public partial class ViewRequestsPage : ContentPage
{
    private MeetingRoom _selectedRoom;
    private ReservationRequestManager _reservationRequestManager;
    private ObservableCollection<ReservationRequest> _requestsForDisplay = new ObservableCollection<ReservationRequest>();

    public ViewRequestsPage(MeetingRoom selectedRoom, ReservationRequestManager reservationRequestManager)
    {
        InitializeComponent();
        _selectedRoom = selectedRoom;
        _reservationRequestManager = reservationRequestManager;
        PopulateRequestsForDisplay();
        RequestsListView.ItemsSource = _requestsForDisplay;
    }

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

    private void OnBackToRoomsClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void OnAcceptClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is ReservationRequest request)
        {
            try
            {
                _reservationRequestManager.AcceptReservationRequest(request);
                PopulateRequestsForDisplay();
                DisplayAlert("Success", "Request accepted successfully.", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

    private void OnRejectClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is ReservationRequest request)
        {
            _reservationRequestManager.ChangeReservationRequestStatus(request.RequestID, RequestStatus.Rejected);
            PopulateRequestsForDisplay();
            DisplayAlert("Success", "Request rejected successfully.", "OK");
        }
    }
}





