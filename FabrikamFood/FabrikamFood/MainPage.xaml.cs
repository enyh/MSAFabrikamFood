using FabrikamFood.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace FabrikamFood
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void search_clicked(Object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var origin = address.Text; 

            var urlString = "https://maps.googleapis.com/maps/api/directions/json?origin=" + 
                origin + "&destination=20+Symonds+St,+Auckland,+1010&key=AIzaSyC1C30FXX5ceD1bqUKgQbrwok5v386MiQA";

            string strResponse = await client.GetStringAsync(new Uri(urlString));
            JsonList.RootObject locationData = JsonConvert.DeserializeObject<JsonList.RootObject>(strResponse);
            string data = $"{locationData.routes[0].legs[0].distance.text} will take {locationData.routes[0].legs[0].duration.text}";
            await DisplayAlert("Distance and Duration:", data, "OK");
        }
    } 

}
