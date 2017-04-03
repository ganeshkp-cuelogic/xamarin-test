using System;
namespace TestDemo
{
	public class RestruantModel
	{
		public RestruantModel()
		{
		}

		public RestruantInfo restaurant{ get; set; }
	}

	public class RestruantInfo {
		public string name { get; set; }
		public string thumb { get; set; }
		public string cuisines { get; set; }
		public string average_cost_for_two { get; set; }
	}


}
