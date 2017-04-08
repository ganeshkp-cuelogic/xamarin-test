
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

		protected override int LayoutResource => Resource.Layout.activity_restaurants;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			initUI();
			SupportActionBar.SetDisplayHomeAsUpEnabled(false);
		}

		protected override void OnResume()
		{
			base.OnResume();
			if (DBManager.sharedManager.getCurrentSetting().location)
			{
				showLoadingIndicator("Fetching current location ...");
				locationManager = new GPAndroidLocationManager(this);
				locationManager.getCurrentLocation((Tuple<double, double> location) =>
				{
					this.location = location;
					hideProgressDialog();
					fetchTheRestaurants(location);
				});
			}
			else
			{
				fetchTheRestaurants(null);
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
