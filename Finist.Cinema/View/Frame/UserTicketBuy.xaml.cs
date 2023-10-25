using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Finist.Cinema.Base;
using Finist.Cinema.Models;
using Finist.Cinema.View.Windows;
using Microsoft.EntityFrameworkCore;
using static Finist.Cinema.MainWindow;

namespace Finist.Cinema.View.Frame
{
	/// <summary>
	/// Логика взаимодействия для UserTicketBuy.xaml
	/// </summary>
	public partial class UserTicketBuy : Page
	{
		Random random = new Random();
		ForWorkBase _context = ForWorkBase.GetContext();

		public class CheckUser : INotifyPropertyChanged
		{

			public int Check { get; set; } = 0;

			public event PropertyChangedEventHandler? PropertyChanged;

			public void OnPropertyChanged([CallerMemberName] string propertyName = null)
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public CheckUser ActualCheck { get; set; }

		public UserTicketBuy()
		{
			InitializeComponent();
			ActualCheck = new CheckUser();
			Zal.DataContext = ActualCheck;

		}

		private void Check_1(object sender, RoutedEventArgs e)
		{
			ActualCheck.Check += 160;
			ActualCheck.OnPropertyChanged(nameof(ActualCheck.Check));

		}

		private void Unchek_1(object sender, RoutedEventArgs e)
		{
			ActualCheck.Check -= 160;
			ActualCheck.OnPropertyChanged(nameof(ActualCheck.Check));
		}



		private void AddFood(object sender, RoutedEventArgs e)
		{

			var windowFod = new WindowFood();
			windowFod.ShowDialog();
			if (MainWindow.Data.FoodsesList != null)
			{foreach (var food in MainWindow.Data.FoodsesList)
			{
				ActualCheck.Check=+food.Cost;
				ActualCheck.OnPropertyChanged(nameof(ActualCheck.Check));

			}
			

			}
			else
			{
				return;
			}
			



		}

		private void ConformationTicket(object sender, RoutedEventArgs e)
		{
			_context.ViewingRooms.Load();
			Ticket ticket = new Ticket();
			ticket.Idviewer = MainWindow.Data.viewer.Id;
			ticket.Idfilm = MainWindow.Data.Movie.Id;
			ticket.TotalSum = ActualCheck.Check;
			if (MainWindow.Data.FoodsesList != null)
			{


				foreach (var food in Data.FoodsesList)
				{
					ticket.Idfoods.Add(food);
					

				}
			}
		
			ticket.IdmovieSchedule=Data.movieSchedule.Id;
			ticket.Idrooms = MainWindow.Data.movieSchedule.Idroom;
			
			

			try
            {
				_context.Tickets.Add(ticket);
				_context.SaveChanges();

			}
			catch
			{
				MessageBox.Show("Возникли ошибки");
				Application.Current.Shutdown();	
			}

			MessageBox.Show("Успешно офрмленн электронный билет. Он доступен в любое время в истории заказов");

		}

		/// <summary>
		/// Рандомизация checkbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WinLoadBuy(object sender, RoutedEventArgs e)
		{
			List<CheckBox> checkBoxes = new List<CheckBox>();
			FindVisualChildren(checkBoxes, this);

			// генерируем 5 случайных чисел
			Random random = new Random();
			var checkBoxesToUncheck = checkBoxes.OrderBy(x => random.Next()).Take(15);

			// выключаем выбранные CheckBox-ы
			foreach (var checkBox in checkBoxesToUncheck)
			{
				checkBox.IsEnabled = false;
				checkBox.BorderThickness=new Thickness(4); 
					checkBox.BorderBrush= Brushes.Black;
			}
		}

		// метод для поиска всех CheckBox-ов на форме
		private void FindVisualChildren<T>(List<T> results, DependencyObject dependencyObject)
			where T : DependencyObject
		{
			int childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);
			for (int i = 0; i < childrenCount; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
				if (child != null && child is T)
				{
					results.Add((T)child);
				}
				else
				{
					FindVisualChildren<T>(results, child);
				}
			}
		}
	}
}
	
