using System;
using Plugin.Connectivity;

namespace TestDemo
{
	public class NetworkReachabilityManager
	{
		public NetworkReachabilityManager()
		{
		}
		public static bool isInternetAvailable() {
			return CrossConnectivity.Current.IsConnected;
		}
	}
}
