
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Iid;

namespace TestDemo.Droid
{
	[Activity(Label = "@string/login",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class GPLoginActivity : BaseActivity
	{
		protected override int LayoutResource => Resource.Layout.activity_gplogin;

		EditText mEditTextEmail, mEditTextPassword;
		TextView mLoginButton;

		#region View Life Cycle Methods
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			initUI();

			//var options = new FirebaseOptions.Builder()
			//.SetApplicationId("1:479717029344:android:9b7c3f5efa21c5ef")
			//.SetApiKey("AIzaSyCyonXZHjL_6Uq5nFIWLKNSYwls1LVMNls")
			////.SetDatabaseUrl("Firebase-Database-Url")
			//.SetGcmSenderId("479717029344")
			//.Build();

			//var firebaseApp = FirebaseApp.InitializeApp(this, options);

			if (Intent.Extras != null)
			{
				foreach (var key in Intent.Extras.KeySet())
				{
					//var value = Intent.Extras.GetString(key);
					//Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
				}
			}

			IsPlayServicesAvailable();

			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Console.WriteLine("Login Activity" + "Refreshed token: " + refreshedToken);
		}
		#endregion 

		#region Private Methods
		private void initUI()
		{
			mEditTextEmail = (EditText)FindViewById(Resource.Id.editTextEmail);
			mEditTextEmail.Text = "ganesh.nist@gmail.com";
			mEditTextPassword = (EditText)FindViewById(Resource.Id.editTextPassword);
			mEditTextPassword.Text = "adminasdf";
			mLoginButton = (TextView)FindViewById(Resource.Id.tvLogin);
			mLoginButton.Click += (sender, e) =>
			{
				if (validateFields())
				{
					if (NetworkReachabilityManager.isInternetAvailable())
					{
						showLoadingIndicator("Signing in ...");
						LoginAPIManager.
								   SharedManager
									   .doLogin(LoginRequestModel.requestModel(mEditTextEmail.Text, mEditTextPassword.Text), (LoginResponse resposne, GPError error) =>
										 {
											 hideProgressDialog();
											 if (error == null)
											 {
												 //mMessageDialog.SendMessage("Login successfull", "Message");
												 moveToMainScreen();
											 }
											 else
											 {
												 mMessageDialog.SendMessage(error.Message, "Alert");
											 }
										 });
					}
					else
					{
						mMessageDialog.SendMessage("No internet connection available");
					}
				}
			};
		}

		private bool validateFields()
		{
			bool status = true;
			if (mEditTextEmail.Text.Length == 0)
			{
				status = false;
				mMessageDialog.SendMessage("Please enter email ID", "Alert");
			}
			else if (!GPValidator.isEmailOK(mEditTextEmail.Text))
			{
				status = false;
				mMessageDialog.SendMessage("Please enter valid email ID", "Alert");
			}
			else if (mEditTextPassword.Text.Length == 0)
			{
				status = false;
				mMessageDialog.SendMessage("Please enter password", "Alert");
			}
			return status;
		}

		private void moveToMainScreen()
		{
			var intent = new Intent(this, typeof(GPRestaurantsActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			StartActivity(intent);
			Finish();
		}
		#endregion


		#region FCM Setup
		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode)){
					
				}
					//msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
				else
				{
					//msgText.Text = "This device is not supported";
					Finish();
				}
				return false;
			}
			else
			{
				//msgText.Text = "Google Play Services is available.";
				return true;
			}
		}
  		#endregion

	}
}
