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
				"https://ru.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BD%D1%81%D1%82%D0%B8%D1%82%D1%83%D1%86%D0%B8%D1%8F_%D0%A3%D0%BA%D1%80%D0%B0%D0%B8%D0%BD%D1%8B";

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

