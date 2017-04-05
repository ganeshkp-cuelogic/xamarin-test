using Foundation;
using System;
using UIKit;

namespace TestDemo.iOS
{
	public partial class GPRestruantsViewController : BaseViewController
    {
		RestrunatsDatasource dataSource = new RestrunatsDatasource();
		private MessageDialog dialog = new MessageDialog();

        public GPRestruantsViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			configureUI();
			fetchRestruants();

			Title = "Restaurants";

			addSettingsButton();

			NavigationController.NavigationBar.TintColor = UIColor.Black;
		}

		private void addSettingsButton() {
			UIButton btnSettings = new UIButton(UIButtonType.Custom);
			btnSettings.SetBackgroundImage(UIImage.FromBundle("setting"), UIControlState.Normal);
			UIBarButtonItem rightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle("setting"), UIBarButtonItemStyle.Plain, (sender, e) =>
			{

			});

			NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);
		}

		private void configureUI() {
			tblViewRestruants.Source = dataSource;
			tblViewRestruants.ReloadData();
		}    

		private void fetchRestruants() {
				showLoading("Fetching Restruants ...");
				GPRestaurantsProvider.sharedProvider.getRestaurants((restaurants, error) =>
				{
					InvokeOnMainThread(() =>
					{
						hideLoading();
						if (error == null) {
							dataSource = new RestrunatsDatasource(restaurants);
							tblViewRestruants.Source = dataSource;
							tblViewRestruants.ReloadData();
						} else
						{
							dialog.SendMessage(error.Message);
						}
					});
				});

		}
	}
}