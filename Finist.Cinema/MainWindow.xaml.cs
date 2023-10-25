using System;
using Finist.Cinema.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using Finist.Cinema.Models;
using Finist.Cinema.View.Frame;
using System.Collections.Generic;
using MahApps.Metro.Controls;

namespace Finist.Cinema
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
        public static class Data
        {
            public static Viewer viewer;
            public static Movie Movie;
            public static List<Food> FoodsesList=new List<Food>();
            public static MovieSchedule movieSchedule;
        }

        public MainWindow()
		{
			InitializeComponent();
			Hub.Content=new Autharization();

		}
	}


}
