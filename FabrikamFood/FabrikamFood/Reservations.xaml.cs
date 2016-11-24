using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabrikamFood
{
    public partial class Reservations : ContentPage
    {

        private List<ReservationsList> reservations;
        private string selectedTime = "";
        public Reservations()
        {
            InitializeComponent();
            selectedTime = picker.Time.ToString();
            picker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
                {
                    selectedTime = picker.Time.ToString();
                }
            };
        }

        private async void LoadReservations()
        {
            CreateRes.IsEnabled = false;
            UpdateRes.IsEnabled = false;
            DeleteRes.IsEnabled = false;
            reservations = await AzureManager.AzureManagerInstance.GetResItems();
            ReservationsList.ItemsSource = reservations;
            CreateRes.IsEnabled = true;
            UpdateRes.IsEnabled = true;
            DeleteRes.IsEnabled = true;
        }

        private async void show_reservations(Object sender, EventArgs e)
        {
            LoadReservations();
        }

        private async void create_reservations(Object sender, EventArgs e)
        {
            if (username.Text!=null && username.Text.Length>0)
            {
                ReservationsList res = new ReservationsList();
                res.Name = username.Text;
                // TIME HERE
                res.Time = selectedTime;
                await AzureManager.AzureManagerInstance.AddResItems(res);
                await DisplayAlert("Success", $"successfullyy created {res.Name}'s reservation", "OK");
                LoadReservations();
            };
        }

        private async void update_reservations(Object sender, EventArgs e)
        {
            if (update.Text != null && update.Text.Length > 0)
            {
                var user = reservations.Where(s => s.Name.ToLower() == update.Text.ToLower()).ToList();
                if (user.Count > 0)
                {
                    var res = user[0];
                    // TIME HERE
                    res.Time = selectedTime;
                    await AzureManager.AzureManagerInstance.UpdateReservation(res);
                    await DisplayAlert("Success", $"successfullyy updated {res.Name}'s reservation", "OK");
                    LoadReservations();
                }
                else
                {
                    await DisplayAlert("Fail", $"Cannot find {update.Text}", "OK");
                }
            }

        }

        private async void delete_reservations(Object sender, EventArgs e)
        {
            if (delete.Text != null && delete.Text.Length > 0)
            {
                var user = reservations.Where(s => s.Name.ToLower() == delete.Text.ToLower()).ToList();
                if (user.Count > 0)
                {
                    var res = user[0];
                    await AzureManager.AzureManagerInstance.CancelReservation(res); // Deletes user from DB
                    await DisplayAlert("Success", $"successfullyy removed your reservation MR: {res.Name}", "OK");
                    LoadReservations();
                }
                else
                {
                    await DisplayAlert("Fail", $"Cannot find {delete.Text}", "OK");
                }
            }

        }

    }
}
