using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Micro_Instagram.Views;

namespace Micro_Instagram
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

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
    }
}
