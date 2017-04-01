using System;
using CoreGraphics;
using UIKit;
namespace TestDemo.iOS
{
	public class BaseViewController : UIViewController
	{
		LoadingOverlay loadPop;

		public BaseViewController()
		{

		}

		public BaseViewController(IntPtr handle) : base(handle)
		{

		}

		#region Loading Indicaor
		protected void showLoading()
		{
			var bounds = UIScreen.MainScreen.Bounds;

			// show the loading overlay on the UI thread using the correct orientation sizing
			loadPop = new LoadingOverlay((CGRect)bounds, "Signing in");
			View.Add(loadPop);
		}

		protected void hideLoading()
		{
			loadPop.Hide();
		}
		#endregion
	}
}
