using Foundation;
using System;
using UIKit;

namespace TestDemo.iOS
{
	public partial class GPSettingsViewController : BaseViewController
    {
		private MessageDialog dialog = new MessageDialog();

        public GPSettingsViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			configureUI();
		}

		private void configureUI() {
			addLogoutButton();
			NavigationController.NavigationBar.TintColor = UIColor.Black;
			popluateCurrentSettingsInfo();
		}

		private void popluateCurrentSettingsInfo() {
			switchOffline.On = DBManager.sharedManager.getCurrentSetting().offline;
			switchLocation.On = DBManager.sharedManager.getCurrentSetting().location;
			switchPushNotifications.On = DBManager.sharedManager.getCurrentSetting().push_notifications;
		}

		private void saveSettings() {
			SettingsModel settingModel = new SettingsModel();
			settingModel.location = switchLocation.On;
			settingModel.offline = switchOffline.On;
			settingModel.push_notifications = switchPushNotifications.On;
			settingModel.apikey = AppRepository.sharedRepository.getAccesstoken();
			DBManager.sharedManager.saveSetting(settingModel);
		}

		private void addLogoutButton()
		{
			UIBarButtonItem rightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle("power"), UIBarButtonItemStyle.Plain, (sender, e) =>
			{
				dialog.SendConfirmation("Do you want to logout ?", "Alert",(bool status) => {
					if(status) {						
						DBManager.sharedManager.deleteCurrentSetting();
						AppRepository.sharedRepository.deleteUserInfo();
						AppDelegate.applicationDelegate().moveToLoginScreen();
					}
				});	
			});
			NavigationItem.SetRightBarButtonItem(rightBarButtonItem, true);

			UIBarButtonItem leftBarButtonItem = new UIBarButtonItem(UIImage.FromBundle("back"), UIBarButtonItemStyle.Plain, (sender, e) =>
			{
				saveSettings();
				NavigationController.PopViewController(true);;
			});

			NavigationItem.SetLeftBarButtonItem(leftBarButtonItem, true);
		}
    }
}