using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using System.Threading.Tasks;
using FabrikamFood.DataModels;

namespace FabrikamFood
{
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Breakfast> foodMenu;
        private IMobileServiceTable<Lunch> lunchMenu;
        private IMobileServiceTable<Dinner> dinnerMenu;
        private IMobileServiceTable<ReservationsList> resMenu;


        private AzureManager()
        {
            this.client = new MobileServiceClient("https://msafabfood.azurewebsites.net");
            this.foodMenu = this.client.GetTable<Breakfast>();
            this.lunchMenu = this.client.GetTable<Lunch>();
            this.dinnerMenu = this.client.GetTable<Dinner>();
            this.resMenu = this.client.GetTable<ReservationsList>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task AddMenuItems(Breakfast menu)
        {
            await this.foodMenu.InsertAsync(menu);
        }

        public async Task<List<Breakfast>> GetMenuItems()
        {
            return await this.foodMenu.ToListAsync();
        }

        public async Task AddLunchItems(Lunch lunch)
        {
            await this.lunchMenu.InsertAsync(lunch);
        }

        public async Task<List<Lunch>> GetLunchItems()
        {
            return await this.lunchMenu.ToListAsync();
        }

        public async Task AddDinnerItems(Dinner dinner)
        {
            await this.dinnerMenu.InsertAsync(dinner);
        }

        public async Task<List<Dinner>> GetDinnerItems()
        {
            return await this.dinnerMenu.ToListAsync();
        }

        public async Task AddResItems(ReservationsList res)
        {
            await this.resMenu.InsertAsync(res);
        }

        public async Task<List<ReservationsList>> GetResItems()
        {
            return await this.resMenu.ToListAsync();
        }

        public async Task CancelReservation(ReservationsList res)
        {
            await this.resMenu.DeleteAsync(res);
        }

        public async Task UpdateReservation(ReservationsList res)
        {
            await this.resMenu.UpdateAsync(res);
        }

    }
}
