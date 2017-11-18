using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IT.Models;

namespace IT.Data
{
	public class OccurrenceItemManager
	{
        IRestService restService;

		public OccurrenceItemManager(IRestService service)
		{
			restService = service;
		}

		public Task<ObservableCollection<Occurrence>> GetTasksAsync()
		{
			return restService.RefreshDataAsync();
		}

        public Task SaveTaskAsync(Occurrence occurrence, bool isNewItem = false)
		{
            return restService.SaveOccurrenceItemAsync(occurrence, isNewItem);
		}

	}

}
