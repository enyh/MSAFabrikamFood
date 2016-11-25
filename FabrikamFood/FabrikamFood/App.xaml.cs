using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace FabrikamFood
{
    public partial class App : Application
    {

        public App()
        {
            MainPage = new TabbedPage
            {
                Children =
                {
                    new MainPage(),
                    new Menu(),
                    new Reservations(),
                    new Facebook()
                }
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }
    }

    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
