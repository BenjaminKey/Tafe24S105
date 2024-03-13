using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
		}

		private void math2CalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}

		//private void mortgageCalculator2Button_Click(object sender, RoutedEventArgs e)
		//{
		//	Frame.Navigate(typeof(MortgageCalculator));
		//}

		private void currencyConverter2Button_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(CurrencyConverter));
		}

		//private void exit2Button_Click(object sender, RoutedEventArgs e)
		//{
		//	Frame.Navigate(typeof(MainMenu));
		//}

		private async void calcButton_Click(object sender, RoutedEventArgs e)
		{
			double principal;
			int years;
			int months;
			double yearlyInterest;
			int totalMonths;
			double monthlyInterest;

			try
			{
				principal = double.Parse(principalTextBox.Text);
				years = int.Parse(yearsTextBox.Text);
				months = int.Parse(monthsTextBox.Text);
				yearlyInterest = double.Parse(yearlyInterestTextBox.Text);
			}
			catch
			{
				var dialog = new MessageDialog("Error! Please enter a valid data.");
				await dialog.ShowAsync();
				principalTextBox.Focus(FocusState.Programmatic);
				principalTextBox.SelectAll();
				return;
			}

			totalMonths = years * 12 + months;
			monthlyInterest = yearlyInterest / 12 / 100;

			double output = CalculateMonthlyRepayment(principal, totalMonths, monthlyInterest);

			repaymentTextBox.Text = output.ToString("N2");
			monthlyInterestTextBox.Text = (monthlyInterest * 100).ToString("N2");

		}

		private void exit4Button_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}

		public static double CalculateMonthlyRepayment(double principal, int totalMonths, double monthlyInterestRate)
		{
			double M = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths)) / (Math.Pow(1 + monthlyInterestRate, totalMonths) - 1);

			return M;
		}
	}
}
