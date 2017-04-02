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
			List<RestruantModel> restruants = JsonConvert.DeserializeObject<List<RestruantModel>> (apiResult.ResponseJSON);
			RestruantsResponse restruantsResopnse = new RestruantsResponse();
			restruantsResopnse.restrunts = restruants.ToArray();
			callback(restruantsResopnse, apiResult.Error);
		}
	}
}
