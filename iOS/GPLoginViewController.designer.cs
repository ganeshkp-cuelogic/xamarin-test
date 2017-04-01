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

namespace MyFirstDemo.iOS
{
    [Register ("GPLoginViewController")]
    partial class GPLoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfEmailId { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tfEmailId != null) {
                tfEmailId.Dispose ();
                tfEmailId = null;
            }
        }
    }
}