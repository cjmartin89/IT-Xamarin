using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IT.Models;

namespace IT.Data
{
	public interface IRestService
	{
		Task<ObservableCollection<Occurrence>> RefreshDataAsync();

        Task SaveOccurrenceItemAsync(Occurrence occurrence, bool isNewItem);

		Task<ObservableCollection<Quotes>> RefreshQuotesAsync();

        Task SaveQuoteItemAsync(Quotes quote, bool isNewItem);
	}
}
