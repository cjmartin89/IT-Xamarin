using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using IT.Models;

namespace IT.Views
{
    public partial class GeraldCalculator : TabbedPage
    {
        
        double GeraldPercentageWrong = 0;

        public double GeraldRight;

		public async void GetPercentageWrong()
		{
            double timewrong = 0;

			ObservableCollection<Occurrence> occurrences;

			occurrences = await App.OccurrenceManager.GetTasksAsync();

			foreach (Occurrence occurrence in occurrences)
			{
				timewrong += occurrence.TimeWrong;
			}

			double minutesInYear = 525600;
			double minutesWrong = minutesInYear - timewrong;
            GeraldPercentageWrong = (minutesWrong / minutesInYear) * 100;
            var percentagerounded = Math.Round((double)GeraldPercentageWrong, 2);
            string percentwrongformatted = string.Format("{0:F2}", percentagerounded + " %");

            PercentageWrong.Text = percentwrongformatted;
            SetLabelColor();

		}

        public void SetLabelColor() {
            if(GeraldPercentageWrong > 99.98) {
                PercentageWrong.TextColor = Color.Green;
            }
            else 
            {
                PercentageWrong.TextColor = Color.Red;
            }
        }

        public void GetMinutesWrong(){

            double minutesInYear = 525600;
            double minutesRight;
            string percentWrongInput = PercentageRight.Text;

            double daysWrong;
            double hoursWrong;
            double minutesWrong;
            double secondsWrong;

            minutesRight = (Convert.ToDouble(percentWrongInput) / 100) * minutesInYear;
            minutesWrong = minutesInYear - minutesRight;
            secondsWrong = minutesWrong * 60;
            hoursWrong = minutesWrong / 60;
            daysWrong = minutesWrong / 1440;

            Days.Text = "Days: " + string.Format("{0:N}", daysWrong);
            Hours.Text = "Hours: " + string.Format("{0:N}", hoursWrong);
            Minutes.Text = "Minutes: " + string.Format("{0:N}", minutesWrong);
            Seconds.Text = "Seconds: " + string.Format("{0:N}", secondsWrong);
        }

        public GeraldCalculator()
        {

            InitializeComponent();

            GeraldRight = Convert.ToDouble(PercentageRight.Text);
            this.BarBackgroundColor = Color.FromHex("#215ace");

        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			GetPercentageWrong();
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			var occurrence = new Occurrence()
			{
				ID = Guid.NewGuid().ToString(),
				dateTime = DateTime.Now
			};
			var LogPage = new LogPage(true);
			LogPage.BindingContext = occurrence;
			Navigation.PushAsync(LogPage);
		}

        void Calculate_Clicked(object sender, System.EventArgs e)
        {
            GetMinutesWrong();
        }
    }
}
