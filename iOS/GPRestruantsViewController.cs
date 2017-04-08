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
		private bool lastGPSStatus = DBManager.sharedManager.getCurrentSetting().location;

        public GPRestruantsViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			configureUI();
			if(lastGPSStatus) {
				fetchUsingCurrentLocation();
			} else {
				fetchRestruants(currentLocation);	
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			if(lastGPSStatus != DBManager.sharedManager.getCurrentSetting().location) {
				lastGPSStatus = DBManager.sharedManager.getCurrentSetting().location;
				if (lastGPSStatus) {
					fetchUsingCurrentLocation();
				} else {
					currentLocation = null;
					fetchRestruants(currentLocation);
				}
			}
		}

		private void fetchUsingCurrentLocation() {
			GPiOSLocationManager.sharedManager.getCurrentLocation((Tuple<double, double> location) =>
					{
						Console.WriteLine(location);
						currentLocation = location;
						fetchRestruants(currentLocation);
					});
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