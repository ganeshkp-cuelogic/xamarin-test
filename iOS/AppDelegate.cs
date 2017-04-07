#define AZURE

using Facebook.CoreKit;
using Foundation;
using Google.SignIn;
using UIKit;

namespace TestDemo.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		string appId = "425601651122473";
		string appName = "Ganesh";

		public static AppDelegate applicationDelegate() {
			return UIApplication.SharedApplication.Delegate as AppDelegate;
		}

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			App.Initialize();

		//	var googleServiceDictionary = NSDictionary.FromFile("GoogleService-Info.plist");
			SignIn.SharedInstance.ClientID = "628615954631-1q16gnrg60dne98hhg7rnlpb8ii3e76s.apps.googleusercontent.com";

			// This is false by default,
			// If you set true, you can handle the user profile info once is logged into FB with the Profile.Notifications.ObserveDidChange notification,
			// If you set false, you need to get the user Profile info by hand with a GraphRequest
			Profile.EnableUpdatesOnAccessTokenChange(true);

			DBManager.sharedManager.createDataBase();

			// Select first UIViewController.
			if (AppRepository.sharedRepository.isUserLoggedIn())
				moveToRestruantViewController();
			else
				moveToLoginScreen();

			return true;
		}

		// For iOS 9 or newer
		public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
		{
			var openUrlOptions = new UIApplicationOpenUrlOptions(options);
			return SignIn.SharedInstance.HandleUrl(url, openUrlOptions.SourceApplication, openUrlOptions.Annotation);
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

		public void moveToRestruantViewController() {
			UINavigationController navigationController = new UINavigationController(UIStoryboard.FromName("Main", null).InstantiateViewController("GPRestruantsViewController"));
			Window.RootViewController = navigationController;
		}

		public void moveToLoginScreen()
		{
			Window.RootViewController = UIStoryboard.FromName("Main", null).InstantiateViewController("MyLoginScreenID");
		}

	}
}
