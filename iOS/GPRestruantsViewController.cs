using Foundation;
using System;
using UIKit;


namespace TestDemo.iOS
{
	public partial class GPRestruantsViewController : BaseViewController
    {
		RestrunatsDatasource dataSource = new RestrunatsDatasource();
		IRestruantsAPI restruantsAPI = new RestruantsAPIManager();

        public GPRestruantsViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			configureUI();
			fetchRestruants();

			Title = "Restaurants";
		}

		private void configureUI() {
			tblViewRestruants.Source = dataSource;
			tblViewRestruants.ReloadData();
		}    

		private void fetchRestruants() {
			showLoading("Fetching Restruants ...");

			GPRestaurantsProvider.sharedProvider.getRestaurants((restaurants, error) => {
				InvokeOnMainThread(() =>
				{
					hideLoading();
					dataSource = new RestrunatsDatasource(restaurants);
					tblViewRestruants.Source = dataSource;
					tblViewRestruants.ReloadData();
				});	 	
			});
		}
	}
}