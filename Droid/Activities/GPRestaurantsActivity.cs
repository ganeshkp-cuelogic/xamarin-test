
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
using Android.Support.V7.Widget;
using UniversalImageLoader.Core;
using Android.Support.Design.Widget;
using Android;

namespace TestDemo.Droid
{
	[Activity(Label = "Restaurants",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class GPRestaurantsActivity : BaseActivity
	{
		RecyclerView mRecyclerView;
		GPRestaurantsRecyclerViewAdapter adapter;
		Tuple<double, double> location;

		GPAndroidLocationManager locationManager;
		private bool lastGPSStatus = DBManager.sharedManager.getCurrentSetting().location;

		Snackbar snackbar;

		readonly string[] PermissionsLocation =
		{
	 		 Manifest.Permission.AccessCoarseLocation,
	 		 Manifest.Permission.AccessFineLocation
		};

		const int RequestLocationId = 0;

		protected override int LayoutResource => Resource.Layout.activity_restaurants;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			initUI();
			SupportActionBar.SetDisplayHomeAsUpEnabled(false);

			if (lastGPSStatus) {
				fetchUsingCurrentLocation();
			} else {
				fetchTheRestaurants(location);
			}
		}

		protected override void OnResume()
		{
			base.OnResume();

			if (lastGPSStatus != DBManager.sharedManager.getCurrentSetting().location) {
				lastGPSStatus = DBManager.sharedManager.getCurrentSetting().location;
				if (lastGPSStatus) {
					fetchUsingCurrentLocation();
				} else {
					location = null;
					fetchTheRestaurants(location);
				}
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
			if (snackbar != null)
			{
				snackbar.Dismiss();
			}
		}

		private void fetchUsingCurrentLocation() {
			showLoadingIndicator("Fetching current location ...");
			locationManager = new GPAndroidLocationManager(this);
			locationManager.getCurrentLocation((Tuple<double, double> location) =>
			{
				this.location = location;
				hideProgressDialog();
				fetchTheRestaurants(location);
			},(bool needPermission) => {
				if(needPermission) {
					hideProgressDialog();

					//Explain to the user why we need to read the contacts
					snackbar = Snackbar.Make(mRecyclerView, "Location access is required to show restaurants nearby.", Snackbar.LengthIndefinite)
					        .SetAction("OK", v => {								
								RequestPermissions(PermissionsLocation, RequestLocationId);
					});		
					snackbar.Show();
				}
			});
		}

		public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			switch (requestCode)
			{
				case RequestLocationId:
					{
						if (grantResults[0] == Permission.Granted)
						{
							//Permission granted
							var snack = Snackbar.Make(mRecyclerView, "Location permission is available, getting lat/long.", Snackbar.LengthShort);
							snack.Show();
							showLoadingIndicator("Fetching current location ...");
							locationManager.permissionGivenByUser();
						}
						else
						{
							//Permission Denied :(
							//Disabling location functionality
							var snack = Snackbar.Make(mRecyclerView, "Location permission is denied.", Snackbar.LengthShort);
							snack.Show();
						}
					}
					break;
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.setting) {
				Intent newIntent;
				newIntent = new Intent(this, typeof(GPSettingsActivity));
				StartActivity(newIntent);
			}
			return true;
		}

		private void initUI() {
			mRecyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerViewRestr);

			// Use default options
			var config = ImageLoaderConfiguration.CreateDefault(ApplicationContext);
			// Initialize ImageLoader with configuration.
			ImageLoader.Instance.Init(config);
		}

		private void fetchTheRestaurants(Tuple<double, double> location) {			
				showLoadingIndicator("Fetching Restaurants...");
				GPRestaurantsProvider.sharedProvider.getRestaurants(location,(restaurants, error) =>
				{
					this.RunOnUiThread(() =>
					{						
						hideProgressDialog();
						if(error == null) {
							adapter = new GPRestaurantsRecyclerViewAdapter(restaurants, this);
							mRecyclerView.SetAdapter(adapter);
							mRecyclerView.SetLayoutManager(new LinearLayoutManager(this));
						} else {
							mMessageDialog.SendMessage(error.Message);
						}
					});
				}); 			
		}
	}
}
