using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestDemo
{
	public class RestruantsAPIManager: IRestruantsAPI
	{
		public RestruantsAPIManager()
		{
		}

		public async void getAllRestraunts(Tuple<double, double> location, Action<RestruantsResponse, GPError> callback) {

			string url = "https://developers.zomato.com/api/v2.1/search";
			if(location != null) {
				url = url + "?lat=" + location.Item1 + "&lon="+location.Item2;
			}
			APIResult apiResult = await NetworkRequestManager.
			                                                 Sharedmanager.
			                                                 sendGetRequest(url);

			RestruantsResponse restaurantResponse = null;
			if(apiResult.Error == null) {
				restaurantResponse = JsonConvert.DeserializeObject<RestruantsResponse>(apiResult.ResponseJSON);
				List<RestruantModel> restModels = new List<RestruantModel>();
				JObject obj = JObject.Parse(apiResult.ResponseJSON);
				IList<JToken> results = obj["restaurants"].Children().ToList();
				foreach (JToken result in results) {
					RestruantModel restModel = result.ToObject<RestruantModel>();
					restModel.restaurant.address = result["restaurant"]["location"]["address"].ToString();
					restModels.Add(restModel);
				}
				restaurantResponse.restaurants = restModels;
			}
			callback(restaurantResponse, apiResult.Error);
		}
	}
}
