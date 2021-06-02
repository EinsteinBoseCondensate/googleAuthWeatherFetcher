using GoogleAuthWeatherApp.AuthLogic;
using GoogleAuthWeatherApp.Data;
using GoogleAuthWeatherApp.Views.Weather;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoogleAuthWeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IGoogleAuthService, GoogleAuthService>();
            DependencyService.Register<IElTiempoRequester, ElTiempoRequester>();
            DependencyService.Register<IOpenWeatherMapRequester, OpenWeatherMapRequester>();
            DependencyService.Register<WeatherViewModel>();
            DependencyService.Register<MainPageViewModel>();
            MainPage = new MainPage().ToNavigationPage();
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

    public static class NavigationExtensions
    {
        public static NavigationPage ToNavigationPage<T>(this T page) where T : Page
        {
            return new NavigationPage(page);
        }
    }
}
