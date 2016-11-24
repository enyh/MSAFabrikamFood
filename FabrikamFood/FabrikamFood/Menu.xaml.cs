using FabrikamFood.DataModels;
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
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        private async void ViewMenu_Clicked(Object sender, EventArgs e)
        {
            List<Breakfast> menu = await AzureManager.AzureManagerInstance.GetMenuItems();

            Breakfast.ItemsSource = menu;
        }

        private async void lunch_Clicked(Object sender, EventArgs e)
        {
            List<Lunch> lunchMenu = await AzureManager.AzureManagerInstance.GetLunchItems();

            Lunch.ItemsSource = lunchMenu;
        }

        private async void dinner_Clicked(Object sender, EventArgs e)
        {
            List<Dinner> dinnerMenu = await AzureManager.AzureManagerInstance.GetDinnerItems();

            Dinner.ItemsSource = dinnerMenu;
        }
    }
}
