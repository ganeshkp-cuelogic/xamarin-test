using Foundation;
using System;
using UIKit;

namespace TestDemo.iOS
{
    public partial class GPLoginScreenViewController : UIViewController
    {

		private MessageDialog dialog = new MessageDialog();

        public GPLoginScreenViewController (IntPtr handle) : base (handle)
        {
        }

		#region View Life Cycle
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			configureUI();
		}
		#endregion

		#region Private Methods
		private bool validateFields() {
			bool status = false;
			if (tfEmailID.Text.Length == 0) {
				status = false;
				dialog.SendMessage("Please enter email ID", "Alert");
			} else if(GPValidator.isEmailOK(tfEmailID.Text)) {
				status = false;
				dialog.SendMessage("Please enter valid email ID", "Alert");
			} else if (tfPassword.Text.Length == 0) {
				status = false;
				dialog.SendMessage("Please enter password", "Alert");
			}
			return status;
		}
		#endregion

		#region Protocol Merthods
		public void configureUI()
		{
			btnLogin.Layer.BorderWidth = 1;
			btnLogin.Layer.BorderColor = UIColor.White.CGColor;
			btnLogin.Layer.CornerRadius = 5;
			btnLogin.ClipsToBounds = true;
		}
		#endregion

		#region Action Methods
		partial void onClickOfLogin(UIButton sender)
		{
			Console.WriteLine("Button Cliked!!");
			if(validateFields()) {
				dialog.SendMessage("Proceed to hit the API", "Alert");
			}
		}
		#endregion
	}
}