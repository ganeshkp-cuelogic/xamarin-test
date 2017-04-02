using System;
using Foundation;
using UIKit;
namespace TestDemo.iOS
{
	public class RestrunatsDatasource: UITableViewSource
	{
		private NSMutableArray restruants = new NSMutableArray();
		public RestrunatsDatasource(NSMutableArray restruants)
		{
			this.restruants = restruants;
		}

		public RestrunatsDatasource()
		{
			
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell("cell", indexPath);

			UIImageView imgViewThumb = (UIImageView)cell.ContentView.ViewWithTag(100);
			UILabel lblName = (UILabel)cell.ContentView.ViewWithTag(200);
			UILabel lblCost = (UILabel)cell.ContentView.ViewWithTag(300);
			UILabel lblCuisine = (UILabel)cell.ContentView.ViewWithTag(400);

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return 30;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 126;
		}
	}
}
