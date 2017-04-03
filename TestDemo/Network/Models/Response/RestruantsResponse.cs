using System;
using System.Collections.Generic;

namespace TestDemo
{
	public class RestruantsResponse
	{
		public RestruantsResponse()
		{
		}

		public IEnumerable<RestruantModel> restaurants{ get; set; }
	}
}
