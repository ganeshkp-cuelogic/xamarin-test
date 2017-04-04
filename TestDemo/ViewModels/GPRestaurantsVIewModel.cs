using System;
using System.Threading.Tasks;

namespace TestDemo
{
	public class GPRestaurantsVIewModel: BaseViewModel
	{
		public ObservableRangeCollection<RestruantModel> restaurants;
		public Command LoadRestaurantCommand { get; set; }
		private IRestruantsAPI restruantsAPI;

		public GPRestaurantsVIewModel()
		{
			Title = "Restaurants";
			restaurants = new ObservableRangeCollection<RestruantModel>();
			LoadRestaurantCommand = new Command(async () => {
				await fetchRestaurantsData();
			});
		}

		async Task fetchRestaurantsData() {
			if (IsBusy)
				return;
			IsBusy = true;

			restaurants.Clear();
			restruantsAPI.getAllRestraunts((RestruantsResponse restruants, GPError arg2) =>
			{
				restaurants.ReplaceRange(restruants.restaurants);
				IsBusy = false;
			});
		}
	}
}
