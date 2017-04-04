using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace TestDemo
{
	public class DBManager: IDBManager
	{
		public static DBManager sharedManager = new DBManager();

		private string dbPath;
		private SQLiteConnection dataBaseConnection;

		public DBManager()
		{
			var sqliteFilename = "MyDatabase.db3";
			#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
			#else
				// we need to put in /Library/ on iOS5.1+ to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
			#endif
			dbPath = Path.Combine(libraryPath, sqliteFilename);
			dataBaseConnection = new SQLiteConnection(dbPath);
		}

		public void createDataBase() {			
			dataBaseConnection.CreateTable<UserInfoModel>();
			dataBaseConnection.CreateTable<RestruantInfo>();
		}

		public void saveUserInfo(UserInfoModel userInfoModel)
		{
			dataBaseConnection.Insert(userInfoModel);	
		}

		public UserInfoModel getUserInfo()
		{
			UserInfoModel userInfoModel = null;
			foreach (var userInfo in dataBaseConnection.Table<UserInfoModel>())
			{
				userInfoModel = userInfo;
				break;
			}	
			return userInfoModel;
		}

		public void saveRestaurantInfo(List<RestruantModel> restaurentModels) {
			for (int i = 0; i < restaurentModels.Count;i++) {
				var model = restaurentModels[i];

				var arrRes = dataBaseConnection.Query<RestruantInfo>("SELECT * FROM restaurant WHERE id = ?", model.restaurant.id);

				//var item = dataBaseConnection.Get<RestruantInfo>(model.restaurant.id);
				if (arrRes.Count == 0)
				{
					dataBaseConnection.Insert(model.restaurant);
				}
				else
				{
					dataBaseConnection.Update(model.restaurant);
				}	
			}
		}

		public List<RestruantModel> getAllRestaurants() {						
			List<RestruantModel> arrRestaurants = new List<RestruantModel>();
			foreach(RestruantInfo restr in dataBaseConnection.Table<RestruantInfo>()) {				
				arrRestaurants.Add(new RestruantModel(restr));
			}
			return arrRestaurants;
		}
	}
}
