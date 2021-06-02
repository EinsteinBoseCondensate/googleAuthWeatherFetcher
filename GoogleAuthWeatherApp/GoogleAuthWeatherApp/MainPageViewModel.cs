using GoogleAuthWeatherApp.AuthLogic;
using GoogleAuthWeatherApp.Views.Base;
using GoogleAuthWeatherApp.Views.Weather;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GoogleAuthWeatherApp
{
    public class MainPageViewModel : ViewModelBase, IGoogleAuthenticationDelegate
    {
        private IGoogleAuthService _googleAuthService;
        public string AuthState { get => authState; set => SetProperty(ref authState, value); }
        private string authState;
        public string ButtonText { get => buttonText; set => SetProperty(ref buttonText, value); }
        private string buttonText;
        public Command LoginCommand { get; }
        public event EventHandler NavigateToWeatherPageBecauseThereIsAToken;
        public MainPageViewModel()
        {
            _googleAuthService = DependencyService.Resolve<IGoogleAuthService>();
            ButtonText = GoogleAuthenticatorHelper.Token != default && GoogleAuthenticatorHelper.Token != null ? "GO WEATHER" : "GO LOGIN";
            AuthState = GoogleAuthenticatorHelper.Token != default && GoogleAuthenticatorHelper.Token != null ? "Authenticated success" : "Need to authenticate";
            LoginCommand = new Command(() =>
            {
                if (GoogleAuthenticatorHelper.Token != default && GoogleAuthenticatorHelper.Token != null)
                    NavigateToWeatherPageBecauseThereIsAToken?.Invoke(null, EventArgs.Empty);
                else
                    _googleAuthService.Autheticate(this);
            });
        }
        public void OnAuthenticationCanceled(string reason = "")
        {
            AuthState = string.IsNullOrEmpty(reason) ? "Authentication canceled" : reason;
        }

        public void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            AuthState = "Authentication success";
            GoogleAuthenticatorHelper.Token = token;
            NavigateToWeatherPageBecauseThereIsAToken?.Invoke(null, EventArgs.Empty);
            ButtonText = "GO WEATHER";
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            AuthState = "Authentication failed";
        }
    }
}

