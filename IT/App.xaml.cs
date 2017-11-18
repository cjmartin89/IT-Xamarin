using System;
using IT.Views;
using Xamarin.Forms;

namespace IT
{
    public partial class App : Application
    {
		public static Data.OccurrenceItemManager OccurrenceManager { get; private set; }
		public static Data.QuotesItemManager QuotesManager { get; private set; }

        public App()
        {
            InitializeComponent();

            OccurrenceManager = new Data.OccurrenceItemManager(new Data.RestService());
            QuotesManager = new Data.QuotesItemManager(new Data.RestService());
            MainPage = new NavigationPage(new MenuPage())
            {
                BarTextColor = Color.Black,
                BarBackgroundColor = Color.FromHex("#215ace"),
                BackgroundColor = Color.FromHex("#215ace")
            };
        }
    }
}
