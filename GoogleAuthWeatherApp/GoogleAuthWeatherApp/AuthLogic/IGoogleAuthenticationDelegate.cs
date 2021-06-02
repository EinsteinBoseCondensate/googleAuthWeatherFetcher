using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAuthWeatherApp.AuthLogic
{
    public interface IGoogleAuthenticationDelegate
    {
        void OnAuthenticationCompleted(GoogleOAuthToken token);
        void OnAuthenticationFailed(string message, Exception exception);
        void OnAuthenticationCanceled(string reason = "");
    }
    public interface IGoogleAuthService
    {
        void Autheticate(IGoogleAuthenticationDelegate googleAuthenticationDelegate);

    }
}
