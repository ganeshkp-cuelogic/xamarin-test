using System;
namespace TestDemo
{
	public class AppRepository:IAppRepository
	{
		public static AppRepository sharedRepository = new AppRepository();

		public AppRepository()
		{
			
		}

		public void saveUserInfo(UserInfoModel userInfoModel)
		{
			DBManager.sharedManager.saveUserInfo(userInfoModel);
		}

		private UserInfoModel getCurrentUserInfo() {
			return DBManager.sharedManager.getUserInfo();
		}

		public bool isUserLoggedIn() {
			return getCurrentUserInfo() != null;
		}
	}
}
