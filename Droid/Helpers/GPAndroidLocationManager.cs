using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Geolocator.Plugin;

namespace TestDemo.Droid
{
	public class GPAndroidLocationManager:Activity, ILocationListener
	{				
		Action<Tuple<double, double>> callback;
		Location _currentLocation;
		LocationManager _locationManager;
		string _locationProvider;
		string LocationService = "location";
		GPRestaurantsActivity activity;

		public GPAndroidLocationManager(GPRestaurantsActivity activity)
		{
			this.activity = activity;
			_locationManager = (LocationManager) activity.GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = string.Empty;
			}
			Log.Debug("Location", "Using " + _locationProvider + ".");			
		}



		public async void getCurrentLocation(Action<Tuple<double, double>> callback)
		{
			this.callback = callback;
			//_locationManager
			//	.RequestLocationUpdates(_locationProvider, 0, 0, this);

			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 100;
			var position = await locator.GetPositionAsync(20000);
			callback(Tuple.Create(position.Latitude, position.Longitude));
		}

		public void stopLocationUpdates() {
			_locationManager.RemoveUpdates(this);
		}

		public void OnLocationChanged(Android.Locations.Location location)
		{
			_currentLocation = location;
			if (_currentLocation != null && callback != null) {
				callback(Tuple.Create(location.Latitude, location.Longitude));
				this.callback = null;
			}
		}

		public void OnProviderDisabled(string provider)
		{
		}

		public void OnProviderEnabled(string provider)
		{
		}

		public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
		{
		}
	}
}
