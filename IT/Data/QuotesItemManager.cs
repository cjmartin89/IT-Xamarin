using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IT.Models;

namespace IT.Data
{
		public class QuotesItemManager
		{
			IRestService restService;

			public QuotesItemManager(IRestService service)
			{
				restService = service;
			}

			public Task<ObservableCollection<Quotes>> GetTasksAsync()
			{
				return restService.RefreshQuotesAsync();
			}

			public Task SaveQuoteItemAsync(Quotes quote, bool isNewItem = false)
			{
				return restService.SaveQuoteItemAsync(quote, isNewItem);
			}

		}
}
