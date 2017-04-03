using Foundation;
using System;
using UIKit;


namespace TestDemo.iOS
{
	public partial class GPRestruantsViewController : BaseViewController
    {
		RestrunatsDatasource dataSource = new RestrunatsDatasource();
		IRestruantsAPI restruantsAPI = new RestruantsAPIManager();

        public GPRestruantsViewController (IntPtr handle) : base (handle)
        {
			
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			configureUI();
			fetchRestruants();
		}

		private void configureUI() {
			tblViewRestruants.Source = dataSource;
			tblViewRestruants.ReloadData();
		}    

		private void fetchRestruants() {
			showLoading("Fetching Restruants ...");
			restruantsAPI.getAllRestraunts((RestruantsResponse restruants, GPError arg2) => {
				InvokeOnMainThread(()=>{
					hideLoading();
					dataSource = new RestrunatsDatasource(restruants.restaurants);
					tblViewRestruants.Source = dataSource;
					tblViewRestruants.ReloadData();	
				});
			});
		}
	}
}