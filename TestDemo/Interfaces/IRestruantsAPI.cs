using System;
namespace TestDemo
{
	public interface IRestruantsAPI
	{
		void getAllRestraunts(Action<RestruantsResponse, GPError> callback);
	}
}
