using System;
using System.Collections.Generic;
using IT.Data;
using IT.Models;
using Xamarin.Forms;

namespace IT.Views
{
    public partial class LogPage : ContentPage
    {
        bool isNewItem;
        public int percentageWrong; 

        public LogPage(bool isNew = false )
        {
            InitializeComponent();
            isNewItem = isNew;
        }

        async void OnSaveActivated (object sender, EventArgs e)
        {
            var occurrence = (Occurrence)BindingContext;
            await App.OccurrenceManager.SaveTaskAsync(occurrence, isNewItem);
            await Navigation.PushAsync(new ListViewPage());
        }

    }
}
