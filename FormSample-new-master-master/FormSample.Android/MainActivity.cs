using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FormSample.Helpers;
using Xamarin.Forms;

namespace FormSample.Droid
{
    using global::Android.Content.PM;

    using Xamarin.Forms.Platform.Android;

	[Activity(Label = "Form Sample", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : AndroidActivity,  ILoginManager
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

           // SetPage(App.GetMainPage());
			if (string.IsNullOrWhiteSpace (Settings.GeneralSettings)) {
				SetPage (App.GetLoginPage (this));
			} else {
				SetPage(App.GetMainPage(this));
			}
        }

		public override void OnBackPressed ()
		{
//			if (string.IsNullOrWhiteSpace (Settings.PageName)) {
//				return;
//			} else {
//				ShowLoginPage ();
//			}
//
////			if (string.IsNullOrWhiteSpace (Settings.GeneralSettings)) {
////				return;
////			}
			base.OnBackPressed ();
		}

		public void ShowMainPage ()
		{
			SetPage (App.GetMainPage (this)); 
		}

		public void ShowLoginPage() 
		{
			SetPage (App.GetLoginPage (this)); 
		}
    }
}

