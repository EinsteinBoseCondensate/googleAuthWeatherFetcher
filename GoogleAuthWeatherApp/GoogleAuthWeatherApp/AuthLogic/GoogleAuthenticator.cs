
using System;
using Xamarin.Auth;

namespace GoogleAuthWeatherApp.AuthLogic
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public static Action MainActivityAction { get; set; }
        private readonly string clientId = "940637245969-vlssj56d6uf3ks1gk72b6ocjbo2hk1j7.apps.googleusercontent.com";
        public GoogleAuthService()
        {

        }

        public void Autheticate(IGoogleAuthenticationDelegate googleAuthenticationDelegate)
        {
            GoogleAuthenticatorHelper.Auth = new GoogleAuthenticator(
               clientId,
               "email",
               "com.companyname.googleauthweatherapp:/oauth2redirect",
               googleAuthenticationDelegate);
            MainActivityAction?.Invoke();
        }
    }
    public class GoogleAuthenticator
    {
        private const string AuthorizeUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        private const bool IsUsingNativeUI = true;

        private OAuth2Authenticator _auth;
        private IGoogleAuthenticationDelegate _authenticationDelegate;

        public delegate void AuthenticationDoneHandler();
        public event AuthenticationDoneHandler AuthenticationDone;

        public GoogleAuthenticator(string clientId, string scope, string redirectUrl, IGoogleAuthenticationDelegate authenticationDelegate)
        {
            
            try
            {
                _authenticationDelegate = authenticationDelegate;
                _auth = new OAuth2Authenticator(clientId, string.Empty, scope,
                                                new Uri(AuthorizeUrl),
                                                new Uri(redirectUrl),
                                                new Uri(AccessTokenUrl),
                                                null, IsUsingNativeUI);

            }
            catch (Exception ex)
            {
                //_authenticationDelegate.OnAuthenticationCanceled(ex.Message ?? ex.InnerException.Message);
            }
            _auth.Completed += OnAuthenticationCompleted;
            _auth.Error += OnAuthenticationFailed;
        }

        public OAuth2Authenticator GetAuthenticator()
        {
            return _auth;
        }

        public void OnPageLoading(Uri uri)
        {
            _auth.OnPageLoading(uri);
        }

        private void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var token = new GoogleOAuthToken
                {
                    TokenType = e.Account.Properties["token_type"],
                    AccessToken = e.Account.Properties["access_token"]
                };
                _authenticationDelegate.OnAuthenticationCompleted(token);
            }
            else
            {
                _authenticationDelegate.OnAuthenticationCanceled();
            }

            if (AuthenticationDone != null)
                AuthenticationDone();
        }

        private void OnAuthenticationFailed(object sender, AuthenticatorErrorEventArgs e)
        {
            _authenticationDelegate.OnAuthenticationFailed(e.Message, e.Exception);
            if (AuthenticationDone != null)
                AuthenticationDone();
        }
    }

    public static class GoogleAuthenticatorHelper
    {
        public static GoogleAuthenticator Auth;
        public static GoogleOAuthToken Token;
    }
}