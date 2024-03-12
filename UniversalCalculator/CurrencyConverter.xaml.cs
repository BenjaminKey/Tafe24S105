using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyConverter : Page
	{
		public CurrencyConverter()
		{
			this.InitializeComponent();
		}

		const double USD_EUR_RATE = 0.85189982, USD_GBP_RATE = 0.72872436, USD_INR_RATE = 74.257327,
					 GBP_USD_RATE = 1.371907, GBP_EUR_RATE = 1.1686692, GBP_INR_RATE = 101.68635,
					 EUR_USD_RATE = 1.1739732, EUR_GBP_RATE = 0.8556672, EUR_INR_RATE = 87.00755,
					 INR_USD_RATE = 0.011492628, INR_EUR_RATE = 0.013492774, INR_GBP_RATE = 0.0098339397;

		private void math3CalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}

		private void mortgageCalculator3Button_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MortgageCalculator));
		}

		//private void currencyConverter3Button_Click(object sender, RoutedEventArgs e)
		//{
		//	Frame.Navigate(typeof(CurrencyConverter));
		//}

		//private void exit3Button_Click(object sender, RoutedEventArgs e)
		//{
		//	Frame.Navigate(typeof(MainMenu));
		//}

		private async void conversionButton_Click(object sender, RoutedEventArgs e)
		{
			double inputAmount;
			double outputAmount = 0;
			string fromCurrency = "";
			string exchangeRate = "";
			string reverseExchangeRate = "";
			string toCurrency = "";
			string currencySign = "";

			if (string.IsNullOrWhiteSpace(amountTextBox.Text))
			{
				var msg = new MessageDialog("Error. Please enter a number.");
				await msg.ShowAsync();
				amountTextBox.Focus(FocusState.Programmatic);
				amountTextBox.SelectAll();
				return;
			}
			try { inputAmount = double.Parse(amountTextBox.Text); }
			catch
			{
				var msg = new MessageDialog("Error. Please enter a number.");
				await msg.ShowAsync();
				amountTextBox.Focus(FocusState.Programmatic);
				amountTextBox.SelectAll();
				return;
			}
			if (int.Parse(amountTextBox.Text) < 0)
			{
				var msg = new MessageDialog("Error. Please enter a positive number.");
				await msg.ShowAsync();
				amountTextBox.Focus(FocusState.Programmatic);
				amountTextBox.SelectAll();
				return;
			}
			if (fromCombbBox.SelectedItem.ToString() == toCombbBox.SelectedItem.ToString())
			{
				beforeConversionTextBlock.Text = "";
				afterConversionTextBlock.Text = "Please choose different currencies.";
				rateTextBlock.Text = "";
				reverseTextBlock.Text = "";
			}
			else
			{
				if (fromCombbBox.SelectedIndex == 0)
				{
					fromCurrency = "USD";
					amountTextBlock.Text = "Amount $";
					if (toCombbBox.SelectedIndex == 0)
					{
						toCurrency = "British Pound";
						currencySign = "£";
						outputAmount = inputAmount * USD_GBP_RATE;
						exchangeRate = USD_GBP_RATE.ToString();
						reverseExchangeRate = GBP_USD_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 2)
					{
						toCurrency = "Euro";
						currencySign = "€";
						outputAmount = inputAmount * USD_EUR_RATE;
						exchangeRate = USD_EUR_RATE.ToString();
						reverseExchangeRate = EUR_USD_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 3)
					{
						toCurrency = "Indian Rupee";
						currencySign = "₹";
						outputAmount = inputAmount * USD_INR_RATE;
						exchangeRate = USD_INR_RATE.ToString();
						reverseExchangeRate = INR_USD_RATE.ToString();
					}
					beforeConversionTextBlock.Text = inputAmount.ToString() + " US Dollar = ";
					afterConversionTextBlock.Text = currencySign + outputAmount.ToString() + " " + toCurrency;
					rateTextBlock.Text = "1 " + fromCurrency + " = " + exchangeRate + " " + toCurrency;
					reverseTextBlock.Text = "1 " + toCurrency + " = " + reverseExchangeRate + " " + fromCurrency;
				}
				if (fromCombbBox.SelectedIndex == 1)
				{
					fromCurrency = "GBP";
					amountTextBlock.Text = "Amount £";
					if (toCombbBox.SelectedIndex == 0)
					{
						toCurrency = fromCurrency;
						currencySign = "£";
						outputAmount = inputAmount;
						exchangeRate = "1";
						reverseExchangeRate = "1";
					}
					if (toCombbBox.SelectedIndex == 1)
					{
						toCurrency = "USD";
						currencySign = "$";
						outputAmount = inputAmount * GBP_USD_RATE;
						exchangeRate = GBP_USD_RATE.ToString();
						reverseExchangeRate = USD_GBP_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 2)
					{
						toCurrency = "Euro";
						currencySign = "€";
						outputAmount = inputAmount * GBP_EUR_RATE;
						exchangeRate = GBP_EUR_RATE.ToString();
						reverseExchangeRate = EUR_GBP_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 3)
					{
						toCurrency = "Indian Rupee";
						currencySign = "₹";
						outputAmount = inputAmount * GBP_INR_RATE;
						exchangeRate = GBP_INR_RATE.ToString();
						reverseExchangeRate = INR_GBP_RATE.ToString();
					}
					beforeConversionTextBlock.Text = inputAmount.ToString() + " British Pound = ";
					afterConversionTextBlock.Text = currencySign + outputAmount.ToString() + " " + toCurrency;
					rateTextBlock.Text = "1 " + fromCurrency + " = " + exchangeRate + " " + toCurrency;
					reverseTextBlock.Text = "1 " + toCurrency + " = " + reverseExchangeRate + " " + fromCurrency;
				}

				if (fromCombbBox.SelectedIndex == 2)
				{
					fromCurrency = "INR";
					amountTextBlock.Text = "Amount ₹";
					if (toCombbBox.SelectedIndex == 0)
					{
						toCurrency = "British Pound";
						currencySign = "£";
						outputAmount = inputAmount * INR_GBP_RATE;
						exchangeRate = INR_GBP_RATE.ToString();
						reverseExchangeRate = GBP_INR_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 1)
					{
						toCurrency = "USD";
						currencySign = "$";
						outputAmount = inputAmount * INR_USD_RATE;
						exchangeRate = INR_USD_RATE.ToString();
						reverseExchangeRate = USD_INR_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 2)
					{
						toCurrency = "Euro";
						currencySign = "€";
						outputAmount = inputAmount * INR_EUR_RATE;
						exchangeRate = INR_EUR_RATE.ToString();
						reverseExchangeRate = EUR_INR_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 3)
					{
						toCurrency = fromCurrency;
						currencySign = "₹";
						outputAmount = inputAmount;
						exchangeRate = "1";
						reverseExchangeRate = "1";
					}
					beforeConversionTextBlock.Text = inputAmount.ToString() + " Indian Rupee = ";
					afterConversionTextBlock.Text = currencySign + outputAmount.ToString() + " " + toCurrency;
					rateTextBlock.Text = "1 " + fromCurrency + " = " + exchangeRate + " " + toCurrency;
					reverseTextBlock.Text = "1 " + toCurrency + " = " + reverseExchangeRate + " " + fromCurrency;
				}

				if (fromCombbBox.SelectedIndex == 3)
				{
					fromCurrency = "Euro";
					amountTextBlock.Text = "Amount €";
					if (toCombbBox.SelectedIndex == 0)
					{
						toCurrency = "British Pound";
						currencySign = "£";
						outputAmount = inputAmount * EUR_GBP_RATE;
						exchangeRate = EUR_GBP_RATE.ToString();
						reverseExchangeRate = GBP_EUR_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 1)
					{
						toCurrency = "USD";
						currencySign = "$";
						outputAmount = inputAmount * EUR_USD_RATE;
						exchangeRate = EUR_USD_RATE.ToString();
						reverseExchangeRate = USD_EUR_RATE.ToString();
					}
					if (toCombbBox.SelectedIndex == 2)
					{
						toCurrency = fromCurrency;
						currencySign = "€";
						outputAmount = inputAmount;
						exchangeRate = "1";
						reverseExchangeRate = "1";
					}
					if (toCombbBox.SelectedIndex == 3)
					{
						toCurrency = "Indian Rupee";
						currencySign = "₹";
						outputAmount = inputAmount * EUR_INR_RATE;
						exchangeRate = EUR_INR_RATE.ToString();
						reverseExchangeRate = INR_EUR_RATE.ToString();
					}
					beforeConversionTextBlock.Text = inputAmount.ToString() + " Euros = ";
					afterConversionTextBlock.Text = currencySign + outputAmount.ToString() + " " + toCurrency;
					rateTextBlock.Text = "1 " + fromCurrency + " = " + exchangeRate + " " + toCurrency;
					reverseTextBlock.Text = "1 " + toCurrency + " = " + reverseExchangeRate + " " + fromCurrency;
				}
			}
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}
	}
}
