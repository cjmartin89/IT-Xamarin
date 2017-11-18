using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using IT.Models;
using Xamarin.Forms;

namespace IT.Views
{
    public partial class QuotesPage : ContentPage
    {
        public ObservableCollection<Quotes> quotes;
		public ObservableCollection<Quotes> _quotes;

        public QuotesPage()
        {
            InitializeComponent();
        }

		protected async override void OnAppearing()
		{
			base.OnAppearing();
            quotes = await App.QuotesManager.GetTasksAsync();
			_quotes = quotes;
			listView.ItemsSource = _quotes;
		}

		ObservableCollection<Quotes> GetQuotes(string SearchText = null)
		{
			if (string.IsNullOrWhiteSpace(SearchText))
				return _quotes;

            var filteredQuotes = new ObservableCollection<Quotes>(_quotes.Where(c => c.Person.StartsWith(SearchText)));
			return filteredQuotes;
		}

        void OnAddQuote_Clicked(object sender, System.EventArgs e)
        {
            var quote = new Quotes()
			{
				ID = Guid.NewGuid().ToString(),
                dateTime = DateTime.Today
			};
            var AddQuote = new AddQuotePage(true);
            AddQuote.BindingContext = quote;
            _quotes.Add(quote);
            Navigation.PushAsync(AddQuote);
        }

        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            var quote = (sender as MenuItem).CommandParameter as Quotes;
			var client = new HttpClient();
            _quotes.Remove(quote);
            await client.DeleteAsync(Constants.QuotesRestUrl + quote.pk);
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            listView.ItemsSource = GetQuotes(e.NewTextValue);
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var quote = e.SelectedItem as Quotes;
            var AddQuotePage = new AddQuotePage();
            AddQuotePage.BindingContext = quote;
            Navigation.PushAsync(AddQuotePage);
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            quotes = await App.QuotesManager.GetTasksAsync();
			listView.ItemsSource = GetQuotes();
			listView.EndRefresh();
        }
    }
}
