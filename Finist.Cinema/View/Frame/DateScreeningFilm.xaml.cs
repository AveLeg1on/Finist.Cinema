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
using Finist.Cinema.Base;
using Finist.Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Finist.Cinema.View.Frame
{
    /// <summary>
    /// Логика взаимодействия для DateScreeningFilm.xaml
    /// </summary>
    public partial class DateScreeningFilm : Page
    {
        ForWorkBase _context=ForWorkBase.GetContext();
        public DateScreeningFilm()
        {
            InitializeComponent();
            LV.ItemsSource = _context.GetDates(MainWindow.Data.Movie.Id);
            var s = _context.GetDates(MainWindow.Data.Movie.Id);
            _context.MovieSchedules.Include(sv => sv.IdroomNavigation);

        }

        private void NextToTimeClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Data.movieSchedule = (MovieSchedule)(sender as Button).DataContext;
           var l= _context.MovieSchedules
                .Select(i => i)
                .Where(id => id.Id == MainWindow.Data.movieSchedule.Id);
                
            MainWindow.Data.movieSchedule.Idroom = (l.FirstOrDefault()).Idroom;
            NavigationService.Navigate(new UserTicketBuy());
        }
    }
}
