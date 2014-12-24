﻿using System;
using FormSample.Helpers;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;


namespace FormSample.Views
{
	using OxyPlot;
	using OxyPlot.Axes;
	using OxyPlot.Series;
	using OxyPlot.XamarinForms;
	using Xamarin.Forms;

	public class CalculatorPage: ContentPage
	{
		SfChart chart1;
		SfChart chart2;
		Entry txtDailyRate;
		Entry txtWeeklyExpense;
		Grid chartGrid;
		Grid takeHomeGridBelowChart;
		Label takeHomePayLimitedLabel;
		Label percentageLimitedLabel;

		Label takeHomePayumbrellaLabel;
		Label percentageumbrellaLabel;

		Label labelAfterChart;
		DataModel limitedCompanyModel;
		DataModel umbrellaCompanyModel;

		public CalculatorPage()
		{

			chart1 = new SfChart();
			chart2 = new SfChart();
			takeHomePayLimitedLabel = new Label(){ XAlign = TextAlignment.Center};
			percentageLimitedLabel = new Label(){ BackgroundColor = Color.Gray, XAlign = TextAlignment.Center };

			takeHomePayumbrellaLabel = new Label{XAlign = TextAlignment.Center};
			percentageumbrellaLabel =  new Label{BackgroundColor = Color.Gray, XAlign = TextAlignment.Center };

			//this.BackgroundColor = Color.White;
			labelAfterChart = new Label(){ TextColor = Color.Black};

			var label = new Label
			{ 
				Text = "Take home pay calculator", BackgroundColor = Color.Black, Font = Font.SystemFontOfSize(NamedSize.Large),
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center, // Center the text in the blue box.
				YAlign = TextAlignment.Center,
				HeightRequest=30
			};

			var grid = SetDailyGrid();

			var lblText = new Label
			{
				Text = "Your contractor would be a 64.00 better off with a limited company set through us than through an" +
					"umbrella company.click refer a contractor button below.", 
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand
			};

			this.chartGrid = new Grid
			{ 
				RowSpacing = 0,
				ColumnSpacing = 0
			};

			this.takeHomeGridBelowChart = new Grid
			{
				RowSpacing=0,
				ColumnSpacing=0
			};

			var downloadButton = new Button { Text = "Download terms and condition", BackgroundColor = Color.FromHex("f7941d"), TextColor = Color.White};
			downloadButton.Clicked += delegate
			{
				//TODO: add downlink location
			};

			var contactUsButton = new Button { Text = "Contact us",BackgroundColor = Color.FromHex("0d9c00"), TextColor = Color.White };
			contactUsButton.Clicked += delegate
			{
				App.RootPage.NavigateTo("Contact us");
			};

			var layout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				// Padding = new Thickness(0, 0, 0, 0)
			};

			layout.Children.Add(label);
			layout.Children.Add(grid);
			layout.Children.Add(lblText);
			layout.Children.Add(new ScrollView
				{
					Content = chartGrid,
					VerticalOptions = LayoutOptions.FillAndExpand,
					HorizontalOptions = LayoutOptions.FillAndExpand
				});
			layout.Children.Add(this.takeHomeGridBelowChart);
			layout.Children.Add(labelAfterChart);
			layout.Children.Add(downloadButton);
			layout.Children.Add(contactUsButton);

			Content = new ScrollView { Content = layout };

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//this.GetDataFromService();
		}

		private Grid SetDailyGrid()
		{
			var grid = new Grid
			{
				RowSpacing = 5,
				ColumnSpacing = 50
			};

			var lblDailyRate = new Label
			{
				Text = "Daily Rate",
				TextColor = Color.Black,

				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.Center,
				// WidthRequest = 100
			};
			this.txtDailyRate = new Entry
			{
				Text = "100",
				TextColor = Color.White,
				BackgroundColor = Color.Green,
				HorizontalOptions = LayoutOptions.End,
				WidthRequest = 100,
				IsEnabled=false
			};
			var lblWeeklyExpense = new Label
			{
				Text = "Weekly Expenses",
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
			};
			this.txtWeeklyExpense = new Entry
			{
				Text = "25",
				TextColor = Color.White,
				BackgroundColor = Color.Green,
				WidthRequest = 100,
				IsEnabled=false
			};
			var upButton = new Button()
			{
				Text = "+",
				TextColor = Color.Red,
				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 30
			};
			var downButton = new Button()
			{
				Text = "-",
				TextColor = Color.Red,
				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 30
			};
			upButton.Clicked += async (object sender, EventArgs e) =>   
			{
				if (Convert.ToInt32(txtDailyRate.Text) >= 100 && Convert.ToInt32(txtDailyRate.Text) < 1200)
				{
					takeHomePayLimitedLabel.Text = string.Empty;
					txtDailyRate.Text = (Convert.ToInt32(txtDailyRate.Text) + 100).ToString();
					await CalculatePayTableData();
				}
			};
			downButton.Clicked += async (object sender, EventArgs e) =>  
			{
				if (Convert.ToInt32(txtDailyRate.Text) > 100)
				{
					takeHomePayLimitedLabel.Text = string.Empty;
					txtDailyRate.Text = (Convert.ToInt32(txtDailyRate.Text) - 100).ToString();
					await CalculatePayTableData();
				}
			};
			var upWeeklyButton = new Button()
			{
				Text = "+",
				TextColor = Color.White,
				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 20
			};
			var downWeeklyButton = new Button()
			{
				Text = "-",
				TextColor = Color.White,

				BackgroundColor = Color.Gray,
				HeightRequest = 20,
				WidthRequest = 20
			};
			upWeeklyButton.Clicked += async (object sender, EventArgs e) =>  
			{
				if (Convert.ToInt32(txtWeeklyExpense.Text) >= 0 && Convert.ToInt32(txtWeeklyExpense.Text) < 750)
				{
					takeHomePayLimitedLabel.Text = string.Empty;
					txtWeeklyExpense.Text = (Convert.ToInt32(txtWeeklyExpense.Text) + 25).ToString();
					await CalculatePayTableData();

				}
			};
			downWeeklyButton.Clicked += async (object sender, EventArgs e) => 
			{
				if (Convert.ToInt32(txtWeeklyExpense.Text) > 25)
				{
					takeHomePayLimitedLabel.Text = string.Empty;
					txtWeeklyExpense.Text = (Convert.ToInt32(txtWeeklyExpense.Text) - 25).ToString();
					await CalculatePayTableData();
				}
			};
			var layout1 = new StackLayout()
			{
				Children = {
					lblDailyRate,
					txtDailyRate,
					upButton,
					downButton
				},
				HorizontalOptions = LayoutOptions.End,
				Orientation = StackOrientation.Horizontal
			};
			grid.Children.Add (layout1, 0, 0);

			var weeklyLayout = new StackLayout () {
				Children =  {
					lblWeeklyExpense,
					txtWeeklyExpense,
					upWeeklyButton,
					downWeeklyButton
				},
				HorizontalOptions = LayoutOptions.End,
				Orientation = StackOrientation.Horizontal
			};
			grid.Children.Add (weeklyLayout, 0, 2);


			return grid;
		}


		private async Task CalculatePayTableData()
		{

			FormSample.PayTableDatabase d = new PayTableDatabase();
			var dailyRate = Convert.ToInt32(this.txtDailyRate.Text);
			var weeklyExpense = Convert.ToInt32(this.txtWeeklyExpense.Text);
			var grossPay = dailyRate * 5;
			var taxablePay = grossPay - weeklyExpense;
			var allData = d.GetPayTables().ToList();

			var payData = d.GetPayTableTaxablePay(taxablePay); //TODO: replace it with taxablePay variable.
			if (payData != null)
			{
				var netPay = payData.TakeHomeLimited;
				var takeHomePayLimited = netPay + weeklyExpense;
				var percentLimited = (takeHomePayLimited / grossPay) * 100;
				var expense = 100 - percentLimited;

				limitedCompanyModel = new DataModel();
				limitedCompanyModel.SetLimitedCompanyData("Take home", percentLimited);
				limitedCompanyModel.SetLimitedCompanyData("Expense", expense);
				GenerateSyncFusionchartLimited("Pay break down - Limited Company");


				Label limitedCompany = new Label()
				{ Text = "Limited company", BackgroundColor = Color.Gray };

				takeHomePayLimitedLabel.Text = takeHomePayLimited.ToString("C");
				percentageLimitedLabel.Text = percentLimited.ToString("F") + " %";

				var takeHomePayLabel = new Label{ Text = "Take Home Pay" };
				var percentPayLabel = new Label{ Text = "Percentage", BackgroundColor = Color.Gray };

				this.takeHomeGridBelowChart.Children.Add(limitedCompany, 0, 1);
				this.takeHomeGridBelowChart.Children.Add(takeHomePayLimitedLabel, 0, 2);
				this.takeHomeGridBelowChart.Children.Add(percentageLimitedLabel, 0, 3);
				Label dummyLabel = new Label()
				{ Text = "         ", BackgroundColor = Color.Gray, XAlign = TextAlignment.Start };

				this.takeHomeGridBelowChart.Children.Add(dummyLabel, 1, 1);
				this.takeHomeGridBelowChart.Children.Add(takeHomePayLabel, 1, 2);
				this.takeHomeGridBelowChart.Children.Add(percentPayLabel, 1, 3);
			}

			payData = d.GetPayTableTaxablePay(grossPay);
			if (payData != null)
			{
				var takeHomeUmbrella = payData.TakeHomeUmbrella;
				var percentUmbrella = (takeHomeUmbrella / grossPay) * 100;
				var expense = 100 - percentUmbrella;

				umbrellaCompanyModel = new DataModel();
				umbrellaCompanyModel.SetUmbrellaCompanyData("Take home", percentUmbrella);
				umbrellaCompanyModel.SetUmbrellaCompanyData("Expense", expense);
				GenerateSyncFusionchartUmbrella("Pay break down - Umbrella Company");

				Label umbrellaCompany = new Label()
				{ Text = "Umbrella company", BackgroundColor = Color.Gray, XAlign = TextAlignment.Start };
				takeHomePayumbrellaLabel.Text = takeHomeUmbrella.ToString("C");
				percentageumbrellaLabel.Text = percentUmbrella.ToString("F") + " %";


				this.takeHomeGridBelowChart.Children.Add(umbrellaCompany, 2, 1);
				this.takeHomeGridBelowChart.Children.Add(takeHomePayumbrellaLabel, 2, 2);
				this.takeHomeGridBelowChart.Children.Add(percentageumbrellaLabel, 2, 3);
				labelAfterChart.Text = "These figures are based on average contracting terms. They ALL include " + weeklyExpense.ToString("C") + " expenses as you specified.";
			}


		}

//		private async Task GetDataFromService()
//		{
//			var service = new PayTableDataService ();
//			var  result = await service.GetPayTableData (Settings.GeneralSettings);
//			var p = result.ToList ();
//			if (p != null) {
//				AddPayData (p);
//			}
//		}
//
//		private void AddPayData(List<PayTable> payDataList)
//		{
//			FormSample.PayTableDatabase d = new PayTableDatabase ();
//			foreach (var payData in payDataList) {
//				d.SaveItem (payData);
//			}
//		}


		private void GenerateSyncFusionchartLimited(string title)
		{
			// SfChart  chart=new SfChart();
			chart1.Title=new ChartTitle(){Text=title};
			chart1.Title.Font = Font.OfSize("Arial", 10);
			chart1.WidthRequest = 200;
			chart1.HeightRequest = 100;

			//Adding ColumnSeries to the chart for percipitation
			chart1.Series.Add(new Syncfusion.SfChart.XForms.PieSeries()
				{
					ItemsSource = limitedCompanyModel.limitedCompanyTax,
					DataMarker = new ChartDataMarker (){ShowLabel  =true},
					IsVisibleOnLegend =true  ,
					// ExplodeAll=true,

				});

			chart1.ColorModel.Palette = ChartColorPalette.Metro;

			//Adding Chart Legend for the Chart
			chart1.Legend = new ChartLegend() 
			{ 
				IsVisible = true, 
				DockPosition= Syncfusion.SfChart.XForms.LegendPlacement.Bottom ,
				LabelStyle = new ChartLegendLabelStyle(){Font = Font.OfSize("Arial", 10) }
			};
			this.chartGrid.Children.Add(chart1, 0, 0);
		}

		private void GenerateSyncFusionchartUmbrella(string title)
		{

			chart2.Title=new ChartTitle() { Text=title};
			chart2.Title.Font = Font.OfSize("Arial", 10);
			//            chart.WidthRequest = 200;
			chart2.HeightRequest = 200;

			//Adding Series to the chart 
			chart2.Series.Add(new Syncfusion.SfChart.XForms.PieSeries()
				{
					ItemsSource = umbrellaCompanyModel.umbrallaCompanyTax,
					DataMarker = new ChartDataMarker (){ShowLabel  =true },
					IsVisibleOnLegend =true  ,
					// ExplodeAll=true
				});


			//Adding Chart Legend for the Chart
			chart2.Legend = new ChartLegend() 
			{ 
				IsVisible = true, 
				DockPosition= Syncfusion.SfChart.XForms.LegendPlacement.Bottom ,
				LabelStyle = new ChartLegendLabelStyle(){Font = Font.OfSize("Arial", 10) }
			};
			this.chartGrid.Children.Add(chart2,1, 0);
		}

	}

	public class DataModel
	{
		public ObservableCollection<ChartDataPoint> limitedCompanyTax;

		public ObservableCollection<ChartDataPoint> umbrallaCompanyTax;

		public DataModel()
		{
			limitedCompanyTax = new ObservableCollection<ChartDataPoint>();
			umbrallaCompanyTax = new ObservableCollection<ChartDataPoint>();

		}

		public void SetLimitedCompanyData(string title, double value)
		{
			this.limitedCompanyTax.Add(new ChartDataPoint(title,value));
		}

		public void SetUmbrellaCompanyData(string title, double value)
		{
			this.umbrallaCompanyTax.Add(new ChartDataPoint(title, value));
		}
	}


}
