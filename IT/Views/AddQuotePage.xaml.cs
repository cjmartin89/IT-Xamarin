using System;
using System.Collections.Generic;
using IT.Models;
using IT.Views;
using Xamarin.Forms;

namespace IT
{
    public partial class AddQuotePage : ContentPage
    {
        bool isNewItem;

        public AddQuotePage(bool isNew = false)
        {
			InitializeComponent();
            isNewItem = isNew;
        }

        async void OnSaveActivated_Clicked(object sender, System.EventArgs e)
        {
			var quote = (Quotes)BindingContext;
            await App.QuotesManager.SaveQuoteItemAsync(quote, isNewItem);
			await Navigation.PushAsync(new QuotesPage());
        }
    }
}
