using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestDemo
{
	public class RestruantsAPIManager: IRestruantsAPI
	{
		public RestruantsAPIManager()
		{
		}

		public async void getAllRestraunts(Action<RestruantsResponse, GPError> callback) {
			APIResult apiResult = await NetworkRequestManager.Sharedmanager.sendGetRequest("https://developers.zomato.com/api/v2.1/search");
			JsonSerializerSettings settings = new JsonSerializerSettings() { 
			
				TypeNameHandling = TypeNameHandling.Objects
			};

			RestruantsResponse restaurantResponse = null;
			if(apiResult.Error == null) {
				restaurantResponse = JsonConvert.DeserializeObject<RestruantsResponse>(apiResult.ResponseJSON, settings);
			}
			callback(restaurantResponse, apiResult.Error);
		}
	}
}
