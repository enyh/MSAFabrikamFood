using Microsoft.WindowsAzure.MobileServices;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FabrikamFood
{
    public partial class Facebook : ContentPage
    {
        public Facebook()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string userId = CrossSettings.Current.GetValueOrDefault("user", "");
            string token = CrossSettings.Current.GetValueOrDefault("token", "");

            if (!token.Equals("") && !userId.Equals(""))
            {
                MobileServiceUser user = new MobileServiceUser(userId);
                user.MobileServiceAuthenticationToken = token;

                AzureManager.AzureManagerInstance.AzureClient.CurrentUser = user;

                authenticated = true;
            }

            if (authenticated == true)
                this.loginButton.IsVisible = false;
        }

        async void loginButton_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();

            if (authenticated == true)
            {
                this.loginButton.IsVisible = false;
                CrossSettings.Current.AddOrUpdateValue("user", AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId);
                CrossSettings.Current.AddOrUpdateValue("token", AzureManager.AzureManagerInstance.AzureClient.CurrentUser.MobileServiceAuthenticationToken);
            }
        }

        bool authenticated = false;
    }
}
