using Foundation;
using System;
using UIKit;

namespace TestDemo.iOS
{
	public partial class GPLoginScreenViewController : BaseViewController
    {

		private MessageDialog dialog = new MessageDialog();

        public GPLoginScreenViewController (IntPtr handle) : base (handle)
        {
        }

		#region View Life Cycle
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//FIXME - Remove this
			tfEmailID.Text = "ganesh.nist@yopmail.com";
			tfPassword.Text = "adminasdf";

		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			configureUI();
		}
		#endregion

		#region Private Methods
		private bool validateFields() {
			bool status = true;
			if (tfEmailID.Text.Length == 0) {
				status = false;
				dialog.SendMessage("Please enter email ID", "Alert");
			} else if(!GPValidator.isEmailOK(tfEmailID.Text)) {
				status = false;
				dialog.SendMessage("Please enter valid email ID", "Alert");
			} else if (tfPassword.Text.Length == 0) {
				status = false;
				dialog.SendMessage("Please enter password", "Alert");
			}
			return status;
		}

		private void addLeftViewToTextField(UITextField textField) {
			textField.LeftViewMode = UITextFieldViewMode.Always;
			UIView leftView = new UIView();
			leftView.Frame = new CoreGraphics.CGRect(0, 0, 15, 15);;
			textField.LeftView = leftView;
		}

		private void moveToRestruantsScreen() {
			//AppDelegate.applicationDelegate().moveToRestruantViewController();
		}

		#endregion

		#region Protocol Merthods
		public void configureUI()
		{
			btnLogin.Layer.BorderWidth = 1;
			btnLogin.Layer.BorderColor = UIColor.White.CGColor;
			btnLogin.Layer.CornerRadius = 5;
			btnLogin.ClipsToBounds = true;

			tfEmailID.BackgroundColor = UIColor.Clear;
			addLeftViewToTextField(tfEmailID);
			tfEmailID.AttributedPlaceholder = new NSAttributedString("Email id", null, UIColor.White);
			tfEmailID.Layer.BorderWidth = 1;
			tfEmailID.Layer.BorderColor = UIColor.White.CGColor;
			tfEmailID.Layer.CornerRadius = 5;
			tfEmailID.ClipsToBounds = true;

			tfPassword.BackgroundColor = UIColor.Clear;
			addLeftViewToTextField(tfPassword);
			tfPassword.AttributedPlaceholder = new NSAttributedString("Password", null, UIColor.White);
			tfPassword.Layer.BorderWidth = 1;
			tfPassword.Layer.BorderColor = UIColor.White.CGColor;
			tfPassword.Layer.CornerRadius = 5;
			tfPassword.ClipsToBounds = true;
		}
		#endregion

		#region Action Methods
		partial void onClickOfLogin(UIButton sender)
		{			
			if(validateFields()) {
				showLoading("Signing in ...");
				LoginAPIManager.
				               SharedManager
				               .doLogin(LoginRequestModel.requestModel(tfEmailID.Text, tfPassword.Text), (LoginResponse resposne, GPError error) =>
									 {
					hideLoading();
					if(error == null) {
						dialog.SendMessage("Login successfull", "Message");
						moveToRestruantsScreen();
					} else {						
						dialog.SendMessage(error.Message, "Alert");
					}				 							
				 });
			}
		}
		#endregion
	}
}