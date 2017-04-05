
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			initUI();
		}

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
				if(validateFields()) {					
					showLoadingIndicator("Signing in ...");
					LoginAPIManager.
							   SharedManager
					               .doLogin(LoginRequestModel.requestModel(mEditTextEmail.Text, mEditTextPassword.Text), (LoginResponse resposne, GPError error) =>
									 {
										hideProgressDialog();
										 if (error == null) {
											//mMessageDialog.SendMessage("Login successfull", "Message");
											moveToMainScreen();
										 } else {
											 mMessageDialog.SendMessage(error.Message, "Alert");
										 }
									 });	
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

		private void moveToMainScreen() {
			var intent = new Intent(this, typeof(GPRestaurantsActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			StartActivity(intent);
			Finish();
		}
		#endregion
	}
}
