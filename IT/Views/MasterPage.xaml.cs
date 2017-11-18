using System;
using System.Collections.Generic;
using IT.Models;
using Xamarin.Forms;

namespace IT.Views
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

		public MasterPage()
		{
			InitializeComponent();

			var masterPageItems = new List<MasterPageItem>();

			masterPageItems.Add(new MasterPageItem()
			{
				Title = "Home",
				TargetType = typeof(HomePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Quotes",
                TargetType = typeof(QuotesPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Gerald Calculator",
                TargetType = typeof(GeraldCalculator)
			});

			listView.ItemsSource = masterPageItems;
		}
    }
}
