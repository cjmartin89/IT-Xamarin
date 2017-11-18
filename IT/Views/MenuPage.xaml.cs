using System;
using System.Collections.Generic;
using IT.Models;
using Xamarin.Forms;

namespace IT.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();

            masterPage.ListView.ItemSelected += ListView_ItemSelected;

            NavigationPage.SetHasNavigationBar(this, false);
        }

        void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
			var item = e.SelectedItem as MasterPageItem;
			if (item != null)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}

        }
    }
}
