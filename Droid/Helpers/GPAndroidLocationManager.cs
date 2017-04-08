using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content.PM;
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
		Action<bool> permissionCallback;
		Location _currentLocation;
		LocationManager _locationManager;
		string _locationProvider;

		public GPAndroidLocationManager(GPRestaurantsActivity activity)
		{			
			initLocationProvider();
		}

		private void initLocationProvider() {
			_locationManager = (Android.Locations.LocationManager)Application.Context.GetSystemService("location");
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

		public void permissionGivenByUser() {
			initLocationProvider();
			requestLocationUpdates();
		}

		public async void getCurrentLocation(Action<Tuple<double, double>> callback, Action<bool> permissionCallback)
		{
			this.callback = callback;
			this.permissionCallback = permissionCallback;
			await GetLocationPermissionAsync();
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
				stopLocationUpdates();
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

		async Task TryGetLocationAsync()
		{
			if ((int)Build.VERSION.SdkInt < 23)
			{
				_locationManager.RequestLocationUpdates(_locationProvider, 0, 10, this);
				return;
			}

			await GetLocationPermissionAsync();
		}

		async Task GetLocationPermissionAsync()
		{
			Contract.Ensures(Contract.Result<Task>() != null);
			//Check to see if any permission in our group is available, if one, then all are
			const string permission = Manifest.Permission.AccessFineLocation;
			if (Application.Context.CheckSelfPermission(permission) == (int)Permission.Granted)
			{
				requestLocationUpdates();
				return;
			}

			this.permissionCallback(true);

			////need to request permission
			//if (ShouldShowRequestPermissionRationale(permission)) {
			//	this.permissionCallback(true);
			//	return;
			//}

			////Finally request permissions with the list of permissions and Id
			//RequestPermissions(PermissionsLocation, RequestLocationId);
		}

		private void requestLocationUpdates() {
			if (_locationProvider.Length > 0)
			{
				_locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 10, this);
			}
			else
			{
				this.permissionCallback(true);
			}			
		}
	}
}
