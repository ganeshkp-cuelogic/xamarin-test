using System;
using System.Collections.Generic;


namespace TestDemo
{
	public class GPRestaurantsProvider
	{
		public static GPRestaurantsProvider sharedProvider = new GPRestaurantsProvider();
		IRestruantsAPI restruantsAPI = new RestruantsAPIManager();

		public GPRestaurantsProvider()
		{
			
		}

		public void getRestaurants(Tuple<double, double> location, Action<List<RestruantModel>, GPError> callback) {
			GPError errorLocal = null;
			if (NetworkReachabilityManager.isInternetAvailable()) {
				restruantsAPI.getAllRestraunts(location,(RestruantsResponse restruantResponse, GPError error) =>
				{
					if (error == null) {
						//Save in DB
						DBManager.sharedManager.saveRestaurantInfo(restruantResponse.restaurants);
					}
					callback(restruantResponse.restaurants, error);
				});
			} else {
				callback(DBManager.sharedManager.getAllRestaurants(), errorLocal);
			}
		} 
	}
}
