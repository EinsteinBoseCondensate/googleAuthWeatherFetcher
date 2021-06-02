using GoogleAuthWeatherApp.AuthLogic;
using GoogleAuthWeatherApp.Views.Weather;
using System;
using Xamarin.Forms;

namespace GoogleAuthWeatherApp
{
    public partial class MainPage : ContentPage
    {
        private void NavigateToWeatherPage(object sender, EventArgs args)
        {
            Navigation.PushAsync(new WeatherPage());
        }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = DependencyService.Resolve<MainPageViewModel>();
            ((MainPageViewModel)BindingContext).NavigateToWeatherPageBecauseThereIsAToken += NavigateToWeatherPage;
            
        }

        ~MainPage()
        {
            ((MainPageViewModel)BindingContext).NavigateToWeatherPageBecauseThereIsAToken -= NavigateToWeatherPage;
        }


        
    }
}
