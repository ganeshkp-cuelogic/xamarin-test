using Foundation;
using System;
using UIKit;
using TestDemo.iOS;

namespace MyFirstDemo.iOS
{
	public partial class GPLoginViewController : BaseViewController
	{
		public GPLoginViewController(IntPtr handle) : base(handle)
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

#region Protocol Merthods
		public void configureUI() {
			btnLogin.Layer.BorderWidth = 1;
			btnLogin.Layer.BorderColor = UIColor.White.CGColor;
			btnLogin.Layer.CornerRadius = 5;
			btnLogin.ClipsToBounds = true;
		}
#endregion

#region Action Methods
		partial void onClickOfLogin(NSObject sender)
		{
			Console.WriteLine("Button Cliked!!");
			MessageDialog dialog = new MessageDialog();
			dialog.SendMessage("Login Clicked", "Alert");
		}
#endregion

	}
}