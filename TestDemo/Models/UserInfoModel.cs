using System;
using SQLite;

namespace TestDemo
{
	[Table("user")]
	public class UserInfoModel
	{	
		[PrimaryKey]
		public string apiKey { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public string createdAt { get; set; }
	}
}
