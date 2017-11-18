using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IT.Models;
using Xamarin.Forms;
using IT.Views;

namespace IT.Views
{
    public partial class ListViewPage : ContentPage
    {
        public ObservableCollection<Occurrence> occurrences;
        public ObservableCollection<Occurrence> _occurrences;

        public ListViewPage()
        {
            InitializeComponent();
        }

		protected async override void OnAppearing()
		{
            base.OnAppearing();
            occurrences = await App.OccurrenceManager.GetTasksAsync();
            _occurrences = occurrences;
            listView.ItemsSource = _occurrences;
		}


        ObservableCollection<Occurrence> GetOccurrences(string SearchText = null)
		{
            if (string.IsNullOrWhiteSpace(SearchText))
                return _occurrences;
            
            var filteredOccurrences = new ObservableCollection<Occurrence>(_occurrences.Where(c => c.Subject.StartsWith(SearchText)));
            return filteredOccurrences;
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
            var occurrence = e.SelectedItem as Occurrence;
			var LogPage = new LogPage();
            LogPage.BindingContext = occurrence;
			Navigation.PushAsync(LogPage);
		}

		void OnAddItemClicked(object sender, EventArgs e)
		{
            var occurrence = new Occurrence()
            {
                ID = Guid.NewGuid().ToString(),
                dateTime = DateTime.Now.ToLocalTime()         
			};
            var LogPage = new LogPage(true);
            LogPage.BindingContext = occurrence;
            _occurrences.Add(occurrence);
            Navigation.PushAsync(LogPage);
		}

        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            var occurrence = (sender as MenuItem).CommandParameter as Occurrence;
            var client = new HttpClient();
            _occurrences.Remove(occurrence);
            await client.DeleteAsync(Constants.RestUrl + occurrence.pk);
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            occurrences = await App.OccurrenceManager.GetTasksAsync();
            listView.ItemsSource = GetOccurrences();
            listView.EndRefresh();
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            listView.ItemsSource = GetOccurrences(e.NewTextValue);
        }

    }
}
