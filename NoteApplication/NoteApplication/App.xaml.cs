﻿using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NoteApplication.ViewModel;

namespace NoteApplication
{
	/// <summary>
	/// Stellt das anwendungsspezifische Verhalten bereit, um die Standardanwendungsklasse zu ergänzen.
	/// </summary>
	sealed partial class App : Application
	{
		/// <summary>
		/// Initialisiert das Singletonanwendungsobjekt. Dies ist die erste Zeile von erstelltem Code
		/// und daher das logische Äquivalent von main() bzw. WinMain().
		/// </summary>
		public App()
		{
			Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
				Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
				Microsoft.ApplicationInsights.WindowsCollectors.Session);
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		/// <summary>
		/// Wird aufgerufen, wenn die Anwendung durch den Endbenutzer normal gestartet wird. Weitere Einstiegspunkte
		/// werden z. B. verwendet, wenn die Anwendung gestartet wird, um eine bestimmte Datei zu öffnen.
		/// </summary>
		/// <param name="e">Details über Startanforderung und -prozess.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
#if DEBUG
			if (System.Diagnostics.Debugger.IsAttached)
			{
				this.DebugSettings.EnableFrameRateCounter = true;
			}
#endif
			Frame rootFrame = Window.Current.Content as Frame;

			// App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthält.
			// Nur sicherstellen, dass das Fenster aktiv ist.
			if (rootFrame == null)
			{
				// Frame erstellen, der als Navigationskontext fungiert und zum Parameter der ersten Seite navigieren
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					//TODO: Zustand von zuvor angehaltener Anwendung laden
				}

				// Den Frame im aktuellen Fenster platzieren
				Window.Current.Content = rootFrame;

				SystemNavigationManager.GetForCurrentView().BackRequested += AppBackRequested;

				rootFrame.Navigated += (sender, args) =>
				{
					SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
						= rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
				};
			}

			if (e.PrelaunchActivated == false)
			{
				if (rootFrame.Content == null)
				{
					// Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
					// und die neue Seite konfigurieren, indem die erforderlichen Informationen als Navigationsparameter
					// übergeben werden
					rootFrame.Navigate(typeof(MainPage), e.Arguments);
				}
				// Sicherstellen, dass das aktuelle Fenster aktiv ist
				Window.Current.Activate();
			}
		}

		public event EventHandler<BackRequestedEventArgs> OnBackRequested;

		private void AppBackRequested(object sender, BackRequestedEventArgs e)
		{
			OnBackRequested?.Invoke(this, e);

			if (!e.Handled)
			{
				// Navigate back within the Frame
				Frame frame = Window.Current.Content as Frame;
				if (frame.CanGoBack)
				{
					if (frame.CurrentSourcePageType == typeof(Pages.NewNotePage))
					{
						((App)App.Current).ViewModelLocator.NewNoteViewModel.NavigateBack();
					}
					else
					{
						frame.GoBack();					
					}
					// Signal handled so that system doesn't navigate back through app stack
					e.Handled = true;
				}
			}
		}

		/// <summary>
		/// Wird aufgerufen, wenn die Navigation auf eine bestimmte Seite fehlschlägt
		/// </summary>
		/// <param name="sender">Der Rahmen, bei dem die Navigation fehlgeschlagen ist</param>
		/// <param name="e">Details über den Navigationsfehler</param>
		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		/// <summary>
		/// Wird aufgerufen, wenn die Ausführung der Anwendung angehalten wird.  Der Anwendungszustand wird gespeichert,
		/// ohne zu wissen, ob die Anwendung beendet oder fortgesetzt wird und die Speicherinhalte dabei
		/// unbeschädigt bleiben.
		/// </summary>
		/// <param name="sender">Die Quelle der Anhalteanforderung.</param>
		/// <param name="e">Details zur Anhalteanforderung.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Anwendungszustand speichern und alle Hintergrundaktivitäten beenden
			deferral.Complete();
		}

		public ViewModelLocator ViewModelLocator => this.Resources["Locator"] as ViewModelLocator;
	}
}
