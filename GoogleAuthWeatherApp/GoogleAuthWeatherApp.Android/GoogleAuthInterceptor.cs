using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GoogleAuthWeatherApp.AuthLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleAuthWeatherApp.Droid
{
    [Activity(Label = "GoogleAuthInterceptor", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(actions: new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataSchemes = new[]
              {
                  // First part of the redirect url (Package name)
                  "com.companyname.googleauthweatherapp"
              },
              DataPaths = new[]
              {
                  // Second part of the redirect url (Path)
                  "/oauth2redirect"
              })]
    public class GoogleAuthInterceptor : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Create your application here
            Android.Net.Uri uri_android = Intent.Data;

            // Convert Android Url to C#/netxf/BCL System.Uri
            var uri_netfx = new Uri(uri_android.ToString());

            // Send the URI to the Authenticator for continuation
            GoogleAuthenticatorHelper.Auth?.OnPageLoading(uri_netfx);
            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            StartActivity(intent);

            Finish();
            return;
        }
    }
}