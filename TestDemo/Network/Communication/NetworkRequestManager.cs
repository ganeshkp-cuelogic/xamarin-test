using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestDemo
{
	public class NetworkRequestManager
	{
		private HttpClient client;

		private static NetworkRequestManager sharedmanager = new NetworkRequestManager();
		public static NetworkRequestManager Sharedmanager
		{
			get
			{
				return sharedmanager;
			}
		}

		public NetworkRequestManager() {
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 2560000;
		}

		#region Protocol Methods
		public async Task<APIResult> sendPostRequest(String inputJson, String url)
		{
			var uri = new Uri(url);
			var content = new StringContent(inputJson, Encoding.UTF8, "application/json");

			HttpResponseMessage response = null;
			response = await client.PostAsync(uri, content);
			var contentBody = await response.Content.ReadAsStringAsync();

			ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(contentBody);

			APIResult apiResult = new APIResult();
			if(response.IsSuccessStatusCode) {				
				if (errorResponse.error) {
					apiResult.Error = new GPError(errorResponse.message);
				}
				else {
					apiResult.ResponseJSON = response.Content.ToString();
				}
			} else {
				apiResult.Error = new GPError(Messages.API_FAILURE_MESSAGE);
			}

			return apiResult;
		}
		#endregion

	}


	public class ErrorResponse {
		public bool error;
		public string message;
	}

}
