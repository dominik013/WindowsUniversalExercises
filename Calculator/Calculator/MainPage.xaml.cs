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
		private string calculationText;
		private string resultText;
		private decimal result = 0m;
		private bool lastPressedOperator = true;
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
		}

		private async void ButtonClick(object sender, RoutedEventArgs e)
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
				else if (pressed.Tag.Equals("Operator") && !lastPressedOperator)
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
							}
							break;
						case "*":
							result *= currentNumber;
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

					lastOperation = pressed.Content as string;
				}
			}
		}
	}
}
