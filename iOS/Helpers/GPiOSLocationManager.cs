using System;
using CoreLocation;
using Foundation;
using UIKit;

namespace TestDemo.iOS
{
	public class GPiOSLocationManager
	{
		public static GPiOSLocationManager sharedManager = new GPiOSLocationManager();
		CLLocationManager manager;
		Action<Tuple<double, double>> callback;

		public GPiOSLocationManager()
		{
			manager = new CLLocationManager();
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
				manager.RequestWhenInUseAuthorization();

			manager.LocationsUpdated += (sender, e) =>
			{
				foreach (var loc in e.Locations)
				{
					Console.WriteLine(loc);
					if(callback != null) {
						manager.StopUpdatingLocation();
						callback(Tuple.Create(loc.Coordinate.Latitude, loc.Coordinate.Longitude));
						this.callback = null;
						break;
					}
				}
			};
		}

		public void getCurrentLocation(Action<Tuple<double, double>> callback) {
			manager.StartUpdatingLocation();
			this.callback = callback;
		}

		public class MyLocationDelegate : CLLocationManagerDelegate
		{
			public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
			{
				foreach (var loc in locations)
				{
					Console.WriteLine(loc);
				}
			}
		}
	}
}
