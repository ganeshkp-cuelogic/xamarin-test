using System;
namespace TestDemo
{
	public class LoginRequestModel
	{
		public LoginRequestModel()
		{
		}

		/// <summary>
		/// The name of the user.
		/// </summary>
		public string email = String.Empty;

		/// <summary>
		/// The password.
		/// </summary>
		public string password = String.Empty;

		public static LoginRequestModel requestModel(String email, String password) {
			LoginRequestModel requestModel = new LoginRequestModel();
		requestModel.email = email;
		requestModel.password = password;
			return requestModel;
		}

	}
}
