using Finist.Cinema.Base;
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
using Finist.Cinema.Models;

namespace Finist.Cinema.View.Frame
{
    /// <summary>
    /// Логика взаимодействия для UserPesonalCabinet.xaml
    /// </summary>
    public partial class UserPesonalCabinet : Page
    {
        ForWorkBase _context=ForWorkBase.GetContext();  
        public UserPesonalCabinet()
        {
            InitializeComponent();
        }

        private void PersCabLoad(object sender, RoutedEventArgs e)
        {
            _context.Tickets.Include(x=>x.IdfilmNavigation).Load();
	        _context.Tickets.Include(x=>x.IdmovieScheduleNavigation).Load();
            LV.ItemsSource = _context.Tickets.Include(x=>x.Idfoods).ToList().Where(x=>x.Idviewer==MainWindow.Data.viewer.Id);
            
        }

        private void ReturnHub(object sender, RoutedEventArgs e)
        {
	        NavigationService navigationService = NavigationService.GetNavigationService(this);

            // Проверка, можно ли перейти назад
            if (navigationService.CanGoBack)
            {
                // Переход назад
                navigationService.GoBack();
            }
        }

        private async void TicketDelete(object sender, RoutedEventArgs e)
        {
	        if (LV.SelectedIndex == -1)
	        {
		        return;
	        }
	        else
	        {
		        var ticket = LV.SelectedItem as Ticket;
		        _context.Tickets.Remove(ticket);
		        await _context.SaveChangesAsync();

                var s = await _context.Tickets.ToListAsync();
                var t = s.Where(x => x.Idviewer == MainWindow.Data.viewer.Id);
                LV.ItemsSource = t;
                //LV.ItemsSource = _context.Tickets.ToList().Where(x => x.Idviewer == MainWindow.Data.viewer.Id);
            }
        }

	}
}
