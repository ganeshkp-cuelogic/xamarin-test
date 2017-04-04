using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace TestDemo.Droid
{
	[Activity(Label = "@string/app_name", Theme = "@style/SplashTheme", MainLauncher = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			DBManager.sharedManager.createDataBase();

			Intent newIntent;
			if (AppRepository.sharedRepository.isUserLoggedIn())
				newIntent = new Intent(this, typeof(GPRestaurantsActivity));
			else
				newIntent = new Intent(this, typeof(GPLoginActivity));

			newIntent.AddFlags(ActivityFlags.ClearTop);
			newIntent.AddFlags(ActivityFlags.SingleTop);
			StartActivity(newIntent);
			Finish();
		}
	}
}
