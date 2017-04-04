using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Views;
using UniversalImageLoader.Core;

namespace TestDemo.Droid
{
	public class GPRestaurantsRecyclerViewAdapter: BaseRecycleViewAdapter
	{
		private List<RestruantModel> restraunts;
		private Activity activity;
		public GPRestaurantsRecyclerViewAdapter(List<RestruantModel> restraunts, Activity activity)
		{
			this.restraunts = restraunts;
			this.activity = activity;
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(Android.Views.ViewGroup parent, int viewType)
		{
			View itemView;
			var id = Resource.Layout.layout_row_restaurant;
			itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
			var viewHolder = new RestaurantViewHolder(itemView);
			return viewHolder;
		}

		// Replace the contents of a view (invoked by the layout manager)
		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			RestruantModel restaurant = restraunts[position];

			// Replace the contents of the view with that element
			var myHolder = holder as RestaurantViewHolder;
			myHolder.tvName.Text = restaurant.restaurant.name;
			myHolder.tvCost.Text =  "Average cost for two : " + restaurant.restaurant.average_cost_for_two;
			myHolder.tvCuisine.Text = (restaurant.restaurant.cuisines == "") ? "NA": restaurant.restaurant.cuisines;

			DisplayImageOptions options = new DisplayImageOptions.Builder().ShowImageForEmptyUri(Resource.Drawable.heroplaceholder).Build();

			// Get singleton instance
			UniversalImageLoader.Core.ImageLoader imageLoader = ImageLoader.Instance;
			// Load image
			imageLoader.DisplayImage(restaurant.restaurant.thumb, myHolder.imgViewRestaurant, options);
		}

		public override int ItemCount
		{
			get
			{
				return restraunts.Count;
			}
		}

	}

	public class RestaurantViewHolder : RecyclerView.ViewHolder
	{
		public TextView tvName;
		public TextView tvCost;
		public TextView tvCuisine;
		public ImageView imgViewRestaurant;

		public RestaurantViewHolder(View itemView) : base(itemView)
		{
			tvName = itemView.FindViewById<TextView>(Resource.Id.tvRestaurantName);
			tvCost = itemView.FindViewById<TextView>(Resource.Id.tvCost);
			tvCuisine = itemView.FindViewById<TextView>(Resource.Id.tvCuisine);
			imgViewRestaurant = itemView.FindViewById<ImageView>(Resource.Id.imgViewRes);
		}
	}
}
