// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace TestDemo.iOS
{
    [Register ("GPSettingsViewController")]
    partial class GPSettingsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch switchLocation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch switchOffline { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch switchPushNotifications { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (switchLocation != null) {
                switchLocation.Dispose ();
                switchLocation = null;
            }

            if (switchOffline != null) {
                switchOffline.Dispose ();
                switchOffline = null;
            }

            if (switchPushNotifications != null) {
                switchPushNotifications.Dispose ();
                switchPushNotifications = null;
            }
        }
    }
}