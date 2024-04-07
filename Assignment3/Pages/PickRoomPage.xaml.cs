using System;
using System.Collections.Generic;
using System.Linq;
using Assignment3.BusinessLogic;



namespace Assignment3.Pages;

public partial class PickRoomPage : ContentPage
{
    private ReservationRequestManager _reservationRequestManager = new ReservationRequestManager();
    private MeetingRoom selectedRoom;

    public PickRoomPage()
    {
        InitializeComponent();
        _reservationRequestManager.InitializeMeetingRooms();
        MeetingRoomListView.ItemsSource = _reservationRequestManager.MeetingRooms;
        RoomImage.Source = "hollowsquare_img.png";
    }

    private void RoomImageSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedRoom = (MeetingRoom)MeetingRoomListView.SelectedItem;
        RoomImage.Source = selectedRoom?.RoomImageFile;
    }

    private async void AddRequestButton_Clicked(object sender, EventArgs e)
    {
        MeetingRoom selectedRoom = (MeetingRoom)MeetingRoomListView.SelectedItem;

        if (selectedRoom != null)
        {
            await Navigation.PushAsync(new AddRequestPage(selectedRoom, _reservationRequestManager));
        }
        else
        {
            await DisplayAlert("Warning", "You must select a room first.", "OK");
        }
    }

    private async void ViewRequestButton_Clicked(object sender, EventArgs e)
    {
        if (selectedRoom != null)
        {
            await Navigation.PushAsync(new ViewRequestsPage(selectedRoom, _reservationRequestManager));
        }
        else
        {
            await DisplayAlert("Warning", "You must select a room first.", "OK");
        }
    }
}





