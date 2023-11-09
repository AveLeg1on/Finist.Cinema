using Finist.Cinema.Base;
using Finist.Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Diagnostics;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace Finist.Cinema.View.Frame
{
	/// <summary>
	/// Логика взаимодействия для Autharization.xaml
	/// </summary>
	public partial class Autharization : Page
	{
		ForWorkBase db = ForWorkBase.GetContext();
	
		public Autharization()
		{
			InitializeComponent();

		}



		private async void AuthUserClick(object sender, RoutedEventArgs e)
		{
			var viewer = await db.Viewers.ToListAsync();
			var authtarizationViewer = viewer.Where(v => v.Email == LoginTB.Text && v.Password == PasswordTB.Password);

			if (authtarizationViewer.Count() != 0)
			{
				MainWindow.Data.viewer = authtarizationViewer.First();
				
					NavigationService.Navigate(new MAinHub());
			}
		}

		private void RegistrationClick(object sender, RoutedEventArgs e)
		{
			//AuthtarizationNext.Navigate(new RegistrationFrame());

			NavigationService navigationService = NavigationService.GetNavigationService(this);

			navigationService.Navigate(new RegistrationFrame());
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string uk =
				"https://www.google.com/";

			Process.Start(new ProcessStartInfo(uk) { UseShellExecute = true });

		}
		

		private void Autharization_OnLoaded(object sender, RoutedEventArgs e)
		{
			OnlineTextBlock.Text =$"Пользователей онлайн: {db.Viewers.ToList().Count}";
			

			// строка подключения к вашей базе данных

			var moviesActors = db.Set<MovieActorView>().FromSqlRaw("SELECT * FROM ActorForMovies")
				.ToList();
			LV.ItemsSource = moviesActors;
			


		}
	}
}

