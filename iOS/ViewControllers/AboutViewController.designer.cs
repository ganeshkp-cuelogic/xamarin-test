// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TestDemo.iOS
{
	[Register ("AboutViewController")]
	partial class AboutViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIImageView AboutImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UITextView AboutTextView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel AppNameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel VersionLabel { get; set; }

		[Action ("ReadMoreButton_TouchUpInside:")]
		partial void ReadMoreButton_TouchUpInside (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AboutImageView != null) {
				AboutImageView.Dispose ();
				AboutImageView = null;
			}

			if (AboutTextView != null) {
				AboutTextView.Dispose ();
				AboutTextView = null;
			}

			if (AppNameLabel != null) {
				AppNameLabel.Dispose ();
				AppNameLabel = null;
			}

			if (VersionLabel != null) {
				VersionLabel.Dispose ();
				VersionLabel = null;
			}
		}
	}
}
