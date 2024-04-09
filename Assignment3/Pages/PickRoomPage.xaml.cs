using System;
using System.Collections.Generic;
using System.Linq;
using Assignment3.BusinessLogic;

namespace Assignment3.Pages;

// The PickRoomPage class allows users to select a meeting room from a list.
public partial class PickRoomPage : ContentPage
{
    // Manager to handle reservation requests and meeting room details.
    private ReservationRequestManager _reservationRequestManager = new ReservationRequestManager();
    // Variable to keep track of the currently selected meeting room.
    private MeetingRoom selectedRoom;

    // Constructor for PickRoomPage.
    public PickRoomPage()
    {
        // Initialize the page's components.
        InitializeComponent();
        // Set up meeting rooms within the reservation manager.
        _reservationRequestManager.InitializeMeetingRooms();
        // Bind the list of meeting rooms to the ListView for display.
        MeetingRoomListView.ItemsSource = _reservationRequestManager.MeetingRooms;
        // Set a default image for the room display.
        RoomImage.Source = "hollowsquare_img.png";
    }

    // Event handler for when a room is selected from the list.
    private void RoomImageSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // Cast the selected item to a MeetingRoom and store it.
        selectedRoom = (MeetingRoom)MeetingRoomListView.SelectedItem;
        // Update the image to reflect the selected room's layout.
        RoomImage.Source = selectedRoom?.RoomImageFile;
    }

    // Event handler for the 'Add Request' button click.
    private async void AddRequestButton_Clicked(object sender, EventArgs e)
    {
        // Retrieve the selected room from the ListView.
        MeetingRoom selectedRoom = (MeetingRoom)MeetingRoomListView.SelectedItem;

        // If a room has been selected, navigate to the AddRequestPage.
        if (selectedRoom != null)
        {
            await Navigation.PushAsync(new AddRequestPage(selectedRoom, _reservationRequestManager));
        }
        else
        {
            // If no room is selected, display a warning to the user.
            await DisplayAlert("Warning!", "You must select a room first.", "OK");
        }
    }

    // Event handler for the 'View Requests' button click.
    private async void ViewRequestButton_Clicked(object sender, EventArgs e)
    {
        // If a room has been selected, navigate to the ViewRequestsPage.
        if (selectedRoom != null)
        {
            await Navigation.PushAsync(new ViewRequestsPage(selectedRoom, _reservationRequestManager));
        }
        else
        {
            // If no room is selected, display a warning to the user.
            await DisplayAlert("Warning!", "You must select a room first.", "OK");
        }
    }
}
