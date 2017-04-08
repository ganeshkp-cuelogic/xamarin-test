using Foundation;
using System;
using UIKit;

namespace TestDemo.iOS
{
	public partial class GPRestruantsViewController : BaseViewController
    {
		RestrunatsDatasource dataSource = new RestrunatsDatasource();
		private MessageDialog dialog = new MessageDialog();

		Tuple<double, double> currentLocation;

        public GPRestruantsViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			configureUI();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			if (DBManager.sharedManager.getCurrentSetting().location) {
				GPiOSLocationManager.sharedManager.getCurrentLocation((Tuple<double, double> location) => {		
					Console.WriteLine(location);
					currentLocation = location;
					fetchRestruants(currentLocation);
				});
			} else {
				fetchRestruants(null);
			}
		}

		private void addSettingsButton() {
			UIBarButtonItem rightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle("setting"), UIBarButtonItemStyle.Plain, (sender, e) =>
			{
				GPSettingsViewController settingVC = (GPSettingsViewController)Storyboard.InstantiateViewController("GPSettingsViewController");
				NavigationController.PushViewController(settingVC,true);
			});

			NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);
		}

		private void configureUI() {
			Title = "Restaurants";
			addSettingsButton();
			NavigationController.NavigationBar.TintColor = UIColor.Black;
			tblViewRestruants.Source = dataSource;
			tblViewRestruants.ReloadData();
		}    

		private void fetchRestruants(Tuple<double, double> location) {
			showLoading("Fetching Restruants ...");
			GPRestaurantsProvider.sharedProvider.getRestaurants(location, (restaurants, error) => {
				InvokeOnMainThread(() =>
					{
						hideLoading();
						if (error == null)
						{
							dataSource = new RestrunatsDatasource(restaurants);
							tblViewRestruants.Source = dataSource;
							tblViewRestruants.ReloadData();
						}
						else
						{
							dialog.SendMessage(error.Message);
						}
					});	
			});
		}
	}
}