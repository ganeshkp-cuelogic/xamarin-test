using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;

namespace TestDemo.Droid
{
	public class BaseActivity : AppCompatActivity
	{

		private ProgressDialog mProgressDialog;
		protected IMessageDialog mMessageDialog = new MessageDialog();

		protected Android.Support.V7.Widget.Toolbar toolBar;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(LayoutResource);
			toolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
			if (toolBar != null)
			{
				SetSupportActionBar(toolBar);
				SupportActionBar.SetDisplayHomeAsUpEnabled(true);
				SupportActionBar.SetHomeButtonEnabled(true);
			}
		}

		public Android.Support.V7.Widget.Toolbar Toolbar
		{
			get;
			set;
		}

		protected virtual int LayoutResource
		{
			get;
		}

		protected int ActionBarIcon
		{
			set { Toolbar?.SetNavigationIcon(value); }
		}


		#region Loading Indicator
		protected void showLoadingIndicator(string loadingText) {
			mProgressDialog = ProgressDialog.Show(this, "", loadingText, false);
		}

		protected void hideProgressDialog() {
			mProgressDialog.Hide();
		}
		#endregion
	}


}

