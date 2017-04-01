using System;
namespace TestDemo
{
	public interface ILoginAPI: IBaseAPI
	{
		
		void doLogin(LoginRequestModel loginRequest, Action<LoginResponse, GPError> callback);
	}
}
