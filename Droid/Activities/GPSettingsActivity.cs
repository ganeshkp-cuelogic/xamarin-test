
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
	[Activity(Label = "Settings",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class GPSettingsActivity : BaseActivity
	{
		protected override int LayoutResource => Resource.Layout.activity_settings;

		private Button btnLogout;
		private CheckedTextView ctvPushNotifications, ctvOfflineMode, ctvLocation;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			initUI();
			populateSettings();

			toolBar.SetNavigationIcon(Resource.Id.home);
			//toolBar.SetNavigationOnClickListener(this);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.home) {
				saveSettings();
				Finish();
			}
			return true;
		}

		public override void OnBackPressed()
		{			
			base.OnBackPressed();
			saveSettings();
			Finish();
		}

		protected override void OnPause()
		{			
			base.OnPause();
		}

		private void saveSettings() {
			SettingsModel settingsModel = new SettingsModel();
			settingsModel.apikey = AppRepository.sharedRepository.getAccesstoken();
			settingsModel.location = ctvLocation.Checked;
			settingsModel.offline = ctvOfflineMode.Checked;
			settingsModel.push_notifications = ctvPushNotifications.Checked;
			DBManager.sharedManager.saveSetting(settingsModel);	
		}
		 
		private void initUI() {
			btnLogout = (Button)FindViewById(Resource.Id.btnLogout);
			btnLogout.Click += (sender, e) => {
				showConfirmation();
			};

			ctvLocation = (CheckedTextView)FindViewById(Resource.Id.checkedTvLocations);
			ctvLocation.Click += (sender, e) => {
				ctvLocation.Checked = !ctvLocation.Checked;
			};

			ctvPushNotifications = (CheckedTextView)FindViewById(Resource.Id.checkedTvPushNotifications);
			ctvPushNotifications.Click += (sender, e) =>
			{
				ctvPushNotifications.Checked = !ctvPushNotifications.Checked;
			};

			ctvOfflineMode = (CheckedTextView)FindViewById(Resource.Id.checkedTvOffline);
			ctvOfflineMode.Click += (sender, e) =>
			{
				ctvOfflineMode.Checked = !ctvOfflineMode.Checked;
			};
		}

		private void populateSettings() {
			SettingsModel currentSetting = DBManager.sharedManager.getCurrentSetting();
			ctvLocation.Checked = currentSetting.location;
			ctvOfflineMode.Checked = currentSetting.offline;
			ctvPushNotifications.Checked = currentSetting.push_notifications;
		}

		private void showConfirmation() {
			var builder = new AlertDialog.Builder(this);
			builder.SetMessage("Do you want to logout ?");
			builder.SetPositiveButton("Yes", (s, e) => {
				moveToLoginScreen();
			});
			builder.SetNegativeButton("No", (s, e) => {
				builder.SetCancelable(true);
			});
			builder.Create().Show();
		}

		private void moveToLoginScreen() {
			DBManager.sharedManager.deleteCurrentSetting();
			AppRepository.sharedRepository.deleteUserInfo();
			Intent newIntent;
			newIntent = new Intent(this, typeof(GPLoginActivity));
			newIntent.AddFlags(ActivityFlags.ClearTop);
			newIntent.AddFlags(ActivityFlags.SingleTop);
			StartActivity(newIntent);
			Finish();
		}

	}
}
