using GoogleAuthWeatherApp.Data;
using GoogleAuthWeatherApp.Views.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GoogleAuthWeatherApp.Views.Weather
{
    public class WeatherViewModel : ViewModelBase
    {
        private readonly IElTiempoRequester requester;
        private readonly IOpenWeatherMapRequester mapRequester;
        public Command LoadItemsCommand { get; }
        private ObservableCollection<Province> provinces = new ObservableCollection<Province>();
        public ObservableCollection<Province> Provinces { get => provinces; set { SetProperty(ref provinces, value); } }
        private string currenLocationWeather = "Fetching your location's weather data...";
        public string CurrentLocationWeather { get => currenLocationWeather; set { SetProperty(ref currenLocationWeather, value); } }

        private bool showCollection;

        public bool ShowCollection
        {
            get { return showCollection; }
            set { SetProperty(ref showCollection, value); DontShowCollection = !value; }
        }
        private bool dontshowCollection;

        public bool DontShowCollection
        {
            get { return dontshowCollection; }
            set { SetProperty(ref dontshowCollection, value); }
        }
        private string noResultsCause;

        public string NoResultsCause
        {
            get { return noResultsCause; }
            set { SetProperty(ref noResultsCause, value); }
        }
        private string currentLocationHeader = "Current location weather";

        public string CurrentLocationHeader
        {
            get { return currentLocationHeader; }
            set { SetProperty(ref currentLocationHeader, value); }
        }

        public WeatherViewModel()
        {
            this.requester = DependencyService.Resolve<IElTiempoRequester>();
            this.mapRequester = DependencyService.Resolve<IOpenWeatherMapRequester>();
            LoadItemsCommand = new Command(() =>
            {
                IsBusy = true;
                ShowCollection = false;
                NoResultsCause = "Fetching provinces data...";
                CurrentLocationWeather = "Fetching your location's weather data...";
                Provinces = new ObservableCollection<Province>();
                Task.Run(async () =>
                {
                    var geoLocation = await Geolocation.GetLastKnownLocationAsync();

                    var results = await requester.GetElTiempoResultsParsed();
                    var actualLocationWeatherResult = await mapRequester.GetWeatherFromCoordinates(geoLocation.Latitude.ToString(), geoLocation.Longitude.ToString());
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        CurrentLocationWeather = actualLocationWeatherResult != default && actualLocationWeatherResult != null ? actualLocationWeatherResult.Description : "No result to show, check your network or report to developer...";
                        CurrentLocationHeader = actualLocationWeatherResult != default && actualLocationWeatherResult != null && !string.IsNullOrEmpty(actualLocationWeatherResult.Name) ? actualLocationWeatherResult.Name+" nearby weather" : CurrentLocationWeather;

                        foreach (var pr in results?.Provinces)
                        {
                            Provinces.Add(pr);
                        }
                        if(Provinces.Count != 0)
                            OnPropertyChanged(nameof(Provinces));

                        NoResultsCause = Provinces.Count == 0 ? "No results to show, check your network or report to developer... " : string.Empty;
                        ShowCollection = Provinces.Count == 0;
                        IsBusy = false;
                    });
                });
            }
            );
            LoadItemsCommand.Execute(null);
        }

    }





}
