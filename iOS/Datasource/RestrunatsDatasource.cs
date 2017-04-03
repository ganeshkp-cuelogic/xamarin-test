using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using SDWebImage;

namespace TestDemo.iOS
{
	public class RestrunatsDatasource: UITableViewSource
	{
		private List<RestruantModel> restruants = new List<RestruantModel>();
		public RestrunatsDatasource(List<RestruantModel> restruants)
		{
			this.restruants = restruants;
		}

		public RestrunatsDatasource()
		{
			
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell("cell", indexPath);

			RestruantModel restruantModel = restruants[indexPath.Row];

			UIImageView imgViewThumb = (UIImageView)cell.ContentView.ViewWithTag(100);
			imgViewThumb.ClipsToBounds = true;
			imgViewThumb.Layer.MasksToBounds = true;
			imgViewThumb.ContentMode = UIViewContentMode.ScaleAspectFill;
			imgViewThumb.SetImage(
				url: new NSUrl(restruantModel.restaurant.thumb),
				placeholder: UIImage.FromBundle("restr")
			);

			UILabel lblName = (UILabel)cell.ContentView.ViewWithTag(200);
			lblName.Text = restruantModel.restaurant.name;

			UILabel lblCost = (UILabel)cell.ContentView.ViewWithTag(300);
			lblCost.Text = "Minimum cost for two : " + restruantModel.restaurant.average_cost_for_two;

			UILabel lblCuisine = (UILabel)cell.ContentView.ViewWithTag(400);
			lblCuisine.Text = (restruantModel.restaurant.cuisines == "") ? "NA" : restruantModel.restaurant.cuisines;

			cell.LayoutIfNeeded();
			cell.SetNeedsLayout();

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return restruants.Count;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 126;
		}
	}
}
