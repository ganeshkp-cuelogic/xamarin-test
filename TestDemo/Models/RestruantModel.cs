using System;
using Android.Locations;
using SQLite;

namespace TestDemo
{
	public class RestruantModel
	{
		public RestruantInfo restaurant { get; set; }

		public RestruantModel(RestruantInfo info)
		{
			this.restaurant = info;
		}
	}

	[Table("restaurant")]
	public class RestruantInfo {
		[PrimaryKey]
		public string id { get; set; }
		public string name { get; set; }
		public string thumb { get; set; }
		public string cuisines { get; set; }
		public string average_cost_for_two { get; set; }
		public string address { get; set; }
	}
}
