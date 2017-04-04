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

		public void getRestaurants(Action<List<RestruantModel>, GPError> callback) {
			if (NetworkReachabilityManager.isInternetAvailable()) {
				restruantsAPI.getAllRestraunts((RestruantsResponse restruantResponse, GPError arg2) =>
				{
					//Save in DB
					DBManager.sharedManager.saveRestaurantInfo(restruantResponse.restaurants);
					callback(restruantResponse.restaurants, null);
				});
			} else {
				callback(DBManager.sharedManager.getAllRestaurants(), null);
			}
		} 
	}
}
