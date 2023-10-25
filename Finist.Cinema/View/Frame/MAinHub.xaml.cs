using Finist.Cinema.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using Finist.Cinema.Models;
using Microsoft.EntityFrameworkCore;
using static Finist.Cinema.View.Frame.Autharization;

namespace Finist.Cinema.View.Frame
{
	/// <summary>
	/// Логика взаимодействия для MAinHub.xaml
	/// </summary>
	public partial class MAinHub : Page
	{
	
		ForWorkBase db = ForWorkBase.GetContext();
		public MAinHub()
		{
			InitializeComponent();
			


		}
		
		private byte[] SelectImage()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.ShowDialog();
			byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName);
			return image_bytes;


		}

		private void Photo(object sender, RoutedEventArgs e)
		{
			var movie = db.Movies.Find(27);
			movie.Poster = SelectImage();


			try
			{
				db.SaveChanges();
				MessageBox.Show("Сохранение успешно");
				LV.ItemsSource = db.Movies.Local.ToBindingList();
			}
			catch { }
		}

		private void Menu_UserClick(object sender, RoutedEventArgs e)
		{
			if (NavigationService.CanGoForward)
			{
				NavigationService.GoForward();	
			}
			else
			{
				NavigationService.Navigate(new UserPesonalCabinet());

			}
			
		}

		private void BuyTicket(object sender, RoutedEventArgs e)
		{ Button button=sender as Button;	
			var film=button.Tag as Movie;
            MainWindow.Data.Movie = film;
           
			NavigationService.Navigate(new DateScreeningFilm());	
		}

		private void MAinHub_OnLoaded(object sender, RoutedEventArgs e)
		{
            db.Actors.Load();
           db.Movies.Include(x=>x.Idcountries).Load();	
            db.Movies.Include(x=>x.Idactors).Load();
			LV.ItemsSource = db.Movies.Local.ToBindingList();
			userHello.Text = MainWindow.Data.viewer.Name;
           



        }
	}
}
