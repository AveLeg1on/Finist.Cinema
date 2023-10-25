using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
using Microsoft.Win32;

namespace Finist.Cinema.View.Frame
{
    /// <summary>
    /// Логика взаимодействия для RegistrationFrame.xaml
    /// </summary>
    public partial class RegistrationFrame : Page
    {private ForWorkBase _context =  ForWorkBase.GetContext();
	    private byte[] photo;
        public RegistrationFrame()
        {
            InitializeComponent();

        }

        

        private void UploadPhoto(object sender, RoutedEventArgs e)
        {

	        var openFile = new OpenFileDialog();
	        if (openFile.ShowDialog() == true)
				{BitmapImage bitmap = new BitmapImage();
					try
					{
						photo = File.ReadAllBytes(openFile.FileName);
						
						bitmap.BeginInit();
						bitmap.UriSource = new Uri(openFile.FileName);
						bitmap.EndInit();



					}
					catch
					{
						MessageBox.Show("Пожалуйста выберите фотографию");
					}

					Im.Source = bitmap;
				}

        }

        private  void Registration_Click(object sender, RoutedEventArgs e)
        {
	        bool flag = false;
			StringBuilder builder=new StringBuilder();	
	       
	        if (!new EmailAddressAttribute().IsValid(EmailTB.Text))
	        { builder.AppendLine("Невернно введен Email");
				flag=true;
				

			}
	       
		
			if (!(DateBirthday.SelectedDate != null))
			{
				builder.AppendLine("Невернна дата");
				flag=true;
				

			}

			if (!(int.TryParse(TelephoneTB.Text, out int number)))
			{
				builder.AppendLine("Номер не соответствует не одному формату");
				flag = true;
				
			}
			if (flag)
			{
				MessageBox.Show(builder.ToString(), "Возникли следующие ошибки");
				return;
			}

			try
			{
				Viewer viewer = new Viewer()
				{
					DateBirthday = DateBirthday.SelectedDate,
					Email = EmailTB.Text,
					Name = nameTB.Text,
					Password = Password.Text,
					Photo = photo,
					Secondname = SecondnameTB.Text,
					Telephone = Convert.ToInt64(TelephoneTB.Text),
					Patronymic = PatronymicTB.Text,
					

				};


				_context.AddViewer(viewer);
			}
			catch (Exception exception)
			{
				builder.AppendLine("Введены неприемлимые данные");
				flag = true;
			}

			if (flag)
			{
				MessageBox.Show(builder.ToString(), "Возникли следующие ошибки");
			}
			else
			{

				_context.SaveChanges();
				MessageBox.Show("Success", "Успешная регистрация");
				NavigationService navigationService = NavigationService.GetNavigationService(this);

				//Проверка, можно ли перейти назад
				if (navigationService.CanGoBack)
				{
					//Переход назад
					navigationService.GoBack();
				}


			}





		}
    }
}
