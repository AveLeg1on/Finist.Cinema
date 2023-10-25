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
using System.Windows.Shapes;
using Finist.Cinema.Base;
using Finist.Cinema.Models;
using Finist.Cinema.View.Frame;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Finist.Cinema.View.Windows
{
	/// <summary>
	/// Логика взаимодействия для WindowFood.xaml
	/// </summary>
	public partial class WindowFood : MetroWindow
	{
		ForWorkBase _context = ForWorkBase.GetContext();
		List<Food> addFoods = new List<Food>();

		public WindowFood()
		{
			InitializeComponent();
			_context.Foods.Load();
			LV.ItemsSource = _context.Foods.Local.ToBindingList();


		}

		private void NewAddFod(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			addFoods.Add(button.DataContext as Food);

		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.ShowDialog();
			byte[] p = File.ReadAllBytes(ofd.FileName);
			Food food = _context.Foods.Where(x => x.Id == 0).First();
			food.Photo = p;

			try
			{

				_context.SaveChanges();
				MessageBox.Show("Успешно");

			}
			catch
			{

			}

		}

		private void WindowFood_OnClosed(object sender, System.ComponentModel.CancelEventArgs e)
		{
			string FinalyFood =
				addFoods.Aggregate("", (current, food) => current + (food.NameFood + Environment.NewLine));

			var t = MessageBox.Show($"Вы уверены, что выбрали все что хотите?\n Ваш заказ: {FinalyFood}",
				"Закрытие магазина", MessageBoxButton.YesNo,
				MessageBoxImage.Question);
			if (t == MessageBoxResult.Yes)
			{
				try
				{

					MainWindow.Data.FoodsesList.AddRange(addFoods);
				}
				catch
				{

				}

				

			}
			else
			{

				e.Cancel = true;
				return;
			}

		}
	}
}