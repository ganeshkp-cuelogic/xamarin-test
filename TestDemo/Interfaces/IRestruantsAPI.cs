using System;
namespace TestDemo
{
	public interface IRestruantsAPI
	{
		void getAllRestraunts(Tuple<double, double> location, Action<RestruantsResponse, GPError> callback);
	}
}
