using Foundation;
using System;
using UIKit;
using Google.SignIn;
using Facebook.LoginKit;
using Facebook.CoreKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestDemo.iOS
{
	public partial class GPLoginScreenViewController : BaseViewController, ISignInDelegate, ISignInUIDelegate
	{
		private MessageDialog dialog = new MessageDialog();
		// This permission is set by default, even if you don't add it, but FB recommends to add it anyway
		string[] readPermissions = { "public_profile" };

		public GPLoginScreenViewController(IntPtr handle) : base(handle)
		{
			
		}

		#region View Life Cycle
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//FIXME - Remove this
			tfEmailID.Text = "ganesh.nist@gmail.com";
			tfPassword.Text = "adminasdf";
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			configureUI();
		}
		#endregion

		#region Private Methods
		private bool validateFields()
		{
			bool status = true;
			if (tfEmailID.Text.Length == 0)
			{
				status = false;
				dialog.SendMessage("Please enter email ID", "Alert");
			}
			else if (!GPValidator.isEmailOK(tfEmailID.Text))
			{
				status = false;
				dialog.SendMessage("Please enter valid email ID", "Alert");
			}
			else if (tfPassword.Text.Length == 0)
			{
				status = false;
				dialog.SendMessage("Please enter password", "Alert");
			}
			return status;
		}

		private void addLeftViewToTextField(UITextField textField)
		{
			textField.LeftViewMode = UITextFieldViewMode.Always;
			UIView leftView = new UIView();
			leftView.Frame = new CoreGraphics.CGRect(0, 0, 15, 15); ;
			textField.LeftView = leftView;
		}

		private void moveToRestruantsScreen()
		{
			var ad = (AppDelegate)UIApplication.SharedApplication.Delegate;
			ad.moveToRestruantViewController();
		}

		private async void configureFacebook()
		{
			var loginManager = new LoginManager();
			FacebookProfileGraphResponse profile = null;
			var loginResult = await loginManager.LogInWithReadPermissionsAsync(readPermissions);

			if( loginResult.Token != null) {
                    var graphRequest = new GraphRequest("/me", null, "GET");
                    var requestConnection = new GraphRequestConnection();
                    requestConnection.AddRequest(graphRequest, (connection, result, error) =>
                    {
                        //profile = result.ToJson<FacebookProfileGraphResponse>();
                        //tcs.SetResult(profile);
                    });
                    requestConnection.Start();
            } else {                    
				
            }
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

		partial void loginAction(UIKit.UIButton sender)
		{
			View.EndEditing(true);
			if (validateFields())
			{
				if (NetworkReachabilityManager.isInternetAvailable())
				{
					showLoading("Signing in ...");
					LoginAPIManager.
								   SharedManager
								   .doLogin(LoginRequestModel.requestModel(tfEmailID.Text, tfPassword.Text), (LoginResponse resposne, GPError error) =>
										 {
											 hideLoading();
											 if (error == null)
											 {
												 dialog.SendMessage("Login successfull", "Message");
												 moveToRestruantsScreen();
											 }
											 else
											 {
												 dialog.SendMessage(error.Message, "Alert");
											 }
										 });

				}
				else
				{
					dialog.SendMessage("No internet connection available");
				}
			}
		}

		partial void onClickOfGoogle(UIButton sender) {
			// Assign the SignIn Delegates to receive callbacks
			SignIn.SharedInstance.UIDelegate = this;
			SignIn.SharedInstance.Delegate = this;
			SignIn.SharedInstance.SignInUser();

		}

		partial void onClickOfFacebook(UIButton sender)
		{
			configureFacebook();
		}
		#endregion

		#region Google Sign in delegate methods
		public void DidSignIn(SignIn signIn, GoogleUser user, NSError error)
		{
			if (user != null && error == null) {
				dialog.SendMessage("Your google accesstoken is = " + user.Profile.Email + "Access Token - " + user.Authentication.AccessToken);
			}		
		}
		#endregion

		[Export("signInWillDispatch:error:")]
		public void WillDispatch(SignIn signIn, NSError error)
		{
			
		}
		[Export("signIn:presentViewController:")]
		public void PresentViewController(SignIn signIn, UIViewController viewController)
		{
			PresentViewController(viewController, true, null);
		}

		[Export("signIn:dismissViewController:")]
		public void DismissViewController(SignIn signIn, UIViewController viewController)
		{
			DismissViewController(true, null);
		}



	}
}