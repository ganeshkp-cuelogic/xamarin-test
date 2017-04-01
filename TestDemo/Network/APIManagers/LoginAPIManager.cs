using System;
using Newtonsoft.Json;

namespace TestDemo
{
	public class LoginAPIManager:BaseAPI, ILoginAPI
	{
		private String loginEndPoint = "/login";

		private static LoginAPIManager sharedManager = new LoginAPIManager();
		public static LoginAPIManager SharedManager
		{
			get
			{
				return sharedManager;
			}
		}

		public LoginAPIManager()
		{	
			
		}

		public async void doLogin(LoginRequestModel loginRequest, Action<LoginResponse, GPError> callback)
		{
			String completeURL = base.getCompleteURL(loginEndPoint);

			String strInputJson = JsonConvert.SerializeObject(loginRequest);

			APIResult result = await NetworkRequestManager.Sharedmanager.sendPostRequest(strInputJson, completeURL);
			LoginResponse loginResponse = new LoginResponse();
			callback(loginResponse, result.Error);
		}
	}
}
