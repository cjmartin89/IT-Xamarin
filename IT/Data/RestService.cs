using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using IT.Models;

namespace IT.Data
{
	public class RestService : IRestService
	{
		HttpClient client;

        public ObservableCollection<Occurrence> _occurrences { get; private set; }
        public ObservableCollection<Quotes> _quotes { get; private set; }

		public RestService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
		}

		public async Task<ObservableCollection<Occurrence>> RefreshDataAsync()
		{
            _occurrences = new ObservableCollection<Occurrence>();

			// RestUrl = http://18.220.140.97:8080/api/wrong/
			var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
                    _occurrences = JsonConvert.DeserializeObject<ObservableCollection<Occurrence>>(content);
                    Debug.WriteLine(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"             ERROR {0}", ex.Message);
			}

            return _occurrences;
		}

        public async Task SaveOccurrenceItemAsync(Occurrence occurrence, bool isNewItem = false)
		{
			// RestUrl = http://18.220.140.97:8080/api/wrong/create
			var uri = new Uri(string.Format(Constants.RestCreateUrl, string.Empty));

			try
			{
                var json = JsonConvert.SerializeObject(occurrence);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem)
				{
					response = await client.PostAsync(uri, content);
				}
				else
				{
                    var updateUri = Constants.RestUrl + occurrence.pk + "/";
                    response = await client.PutAsync(updateUri, content);
				}

				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine(@"TodoItem successfully saved.");
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
		}

        public async Task<ObservableCollection<Quotes>> RefreshQuotesAsync()
		{
            _quotes = new ObservableCollection<Quotes>();

			// RestUrl = http://18.220.140.97:8080/api/quotes/
            var uri = new Uri(string.Format(Constants.QuotesRestUrl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
                    _quotes = JsonConvert.DeserializeObject<ObservableCollection<Quotes>>(content);
					Debug.WriteLine(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"             ERROR {0}", ex.Message);
			}

			return _quotes;
		}

        public async Task SaveQuoteItemAsync(Quotes quote, bool isNewItem = false)
		{
			// RestUrl = http://18.220.140.97:8080/api/quotes/create
			var uri = new Uri(string.Format(Constants.QuotesRestCreateUrl, string.Empty));

			try
			{
                var json = JsonConvert.SerializeObject(quote);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem)
				{
					response = await client.PostAsync(uri, content);
				}
				else
				{
                    var updateUri = Constants.QuotesRestUrl + quote.pk + "/";
					response = await client.PutAsync(updateUri, content);
				}

				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine(@"TodoItem successfully saved.");
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
		}
	}
}
