﻿using System;
namespace TestDemo
{
	public class BaseAPI
	{
		public BaseAPI()
		{
		}

		private String BASE_URL = "http://192.168.0.101:8888/task_manager/v1";

		protected String getCompleteURL(String endPoint) {
			return BASE_URL + endPoint;
		}
	}
}
