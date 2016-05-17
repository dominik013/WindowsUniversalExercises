using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
		// The number of decimal-places used
		public const int PRECISION = 5;

		private string calculationText;
		private string resultText;

		private decimal result = 0m;
		private decimal? memoryStorage = null;

		// See if the last pressed button was an operator
		private bool lastPressedOperator = true;

		// See what operation was last added, to make the right calculation.
		private string lastOperation = "";

		private StringBuilder currentNumberBuilder;

		public event PropertyChangedEventHandler PropertyChanged;

		public string ResultText
		{
			get { return this.resultText; }
			set
			{
				resultText = value;
				if (PropertyChanged != null)
					this.OnPropertyChanged();
			}
		}
		
		public string CalculationText
		{
			get { return this.calculationText; }
			set
			{
				calculationText = value;
				if (PropertyChanged != null)
					this.OnPropertyChanged();
			}
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public MainPage()
        {
            this.InitializeComponent();
			ApplicationView.PreferredLaunchViewSize = new Size(480, 800);
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

			this.DataContext = this;
			currentNumberBuilder = new StringBuilder();
			ResultText = result.ToString();
		}
		
		private void NumberClick(object sender, RoutedEventArgs e)
		{
			Button pressed;
			if (sender is Button)
			{
				pressed = sender as Button;

				if (pressed.Tag.Equals("Number"))
				{
					lastPressedOperator = false;

					if (currentNumberBuilder.Length == 0)
					{
						ResultText = "";
					}
					if (pressed.Content.Equals(".") && currentNumberBuilder.ToString().Contains("."))
					{
						return;
					}

					currentNumberBuilder.Append(pressed.Content);
					CalculationText += pressed.Content;
					ResultText += pressed.Content;
				}
			}
		}

		private void ClearClick(object sender, RoutedEventArgs e)
		{
			result = 0m;
			ResultText = "0";
			CalculationText = "";
			currentNumberBuilder.Clear();
			lastOperation = "";
			lastPressedOperator = true;
		}

		private void MemoryClick(object sender, RoutedEventArgs e)
		{
			Button pressed;
			if (sender is Button)
			{
				pressed = sender as Button;
				{
					if (pressed.Tag.ToString().Contains("Memory"))
					{
						var splittedTag = pressed.Tag.ToString().Split('_');

						switch (splittedTag[splittedTag.Length - 1])
						{
							case "Store":
								if (currentNumberBuilder.Length > 0)
								{
									decimal tempValue;
									memoryStorage = decimal.TryParse(currentNumberBuilder.ToString(), out tempValue)
										? tempValue : null as decimal?;

								}
								break;
							case "Restore":
								if (memoryStorage != null && !lastOperation.Equals("="))// && !CalculationText.ToString().Equals(ResultText.ToString()))
								{
									currentNumberBuilder.Clear().Append(memoryStorage.ToString());
									ResultText = currentNumberBuilder.ToString();
									lastPressedOperator = false;
									int i;
									for (i = CalculationText.Length - 1; i > 0; i--)
									{
										if (CalculationText[i].Equals('+') ||
											CalculationText[i].Equals('-') ||
											CalculationText[i].Equals('*') ||
											CalculationText[i].Equals('/'))
										{
											break;
										}
									}
									if (i == 0)
										CalculationText = CalculationText.Remove(i);
									else if (i == CalculationText.Length - 1) { }
									else
										CalculationText = CalculationText.Remove(i + 1);
									CalculationText += ResultText;
								}
								break;
							case "Clear":
								memoryStorage = null;
								break;
							default:
								break;
						}
					}
				}
			}
		}

		private async void OperatorClick(object sender, RoutedEventArgs e)
		{
			Button pressed;
			if (sender is Button)
			{
				pressed = sender as Button;
				if (pressed.Tag.ToString().Contains("Operator") && !lastPressedOperator)
				{
					decimal currentNumber;
					if (decimal.TryParse(currentNumberBuilder.ToString(), out currentNumber))
					{
						currentNumberBuilder.Clear();
					}
					else
					{
						// If we get an error parsing we are showing a message-box and reseting the calculator
						var dialog = new MessageDialog("Error parsing number!");
						await dialog.ShowAsync();
						result = 0m;
						ResultText = "0";
						CalculationText = "";
						return;
					}

					switch (lastOperation)
					{
						case "+":
							result += currentNumber;
							break;
						case "-":
							result -= currentNumber;
							break;
						case "/":
							if (currentNumber == 0)
							{
								var dialog = new MessageDialog("Dont't divide by zero!");
								await dialog.ShowAsync();
							}
							else
							{
								result /= currentNumber;
								result = Math.Round(result, PRECISION);
							}
							break;
						case "*":
							result *= currentNumber;
							result = Math.Round(result, PRECISION);
							break;
						case "":
							result = currentNumber;
							break;
						default:
							break;
					}

					ResultText = result.ToString();

					if (pressed.Content.Equals("="))
					{

						CalculationText = ResultText;
						lastPressedOperator = false;
						currentNumberBuilder.Append(ResultText);
					}
					else
					{
						CalculationText += pressed.Content;
						lastPressedOperator = true;
					}

					var splittedTag = pressed.Tag.ToString().Split('_');
					lastOperation = splittedTag[splittedTag.Length - 1];
				}
			}		
		}
	}
}
