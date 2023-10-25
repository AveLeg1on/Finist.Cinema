using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Finist.Cinema.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Finist.Cinema.Base
{
    public partial class ForWorkBase : FinistContext
    {
        private static ForWorkBase _instance;
        public static ForWorkBase GetContext()
        {
	        if (_instance == null)
	        {
		        _instance = new ForWorkBase();
                
	        }
            return _instance;

        }
		public DbSet<MovieActorView> MovieActorViews { get; set; }

		protected void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MovieActorView>().HasNoKey();
		}
		public IEnumerable<MovieSchedule> GetDates(int number)
        {
	        var query = $"EXEC GetDates {number}";
            
	        return _instance.MovieSchedules.FromSqlInterpolated(FormattableStringFactory.Create(query)).ToList();
        }

        public void AddViewer( Viewer viewer)
        {

	        var nameParam = new SqlParameter("@Name", viewer.Name);
	        var secondnameParam = new SqlParameter("@Secondname", viewer.Secondname);
	        var telephoneParam = new SqlParameter("@Telephone", viewer.Telephone);
	        var emailParam = new SqlParameter("@Email", viewer.Email);
	        var dateBirthdayParam = new SqlParameter("@DateBirthday", SqlDbType.DateTime);
	        dateBirthdayParam.Value = viewer.DateBirthday ?? (object)DBNull.Value;
	        var photoParam = new SqlParameter("@Photo", SqlDbType.VarBinary);
			photoParam.Value = viewer.Photo?? (object)DBNull.Value;
	        var passwordParam = new SqlParameter("@Password", viewer.Password);
	        var patronymicParam = new SqlParameter("@Patronymic", viewer.Patronymic ?? (object)DBNull.Value);

			Database.ExecuteSqlRaw("EXEC AddViewer @Name, @Secondname, @Telephone, @Email, @DateBirthday, @Photo, @Password, @Patronymic", nameParam, secondnameParam, telephoneParam, emailParam, dateBirthdayParam, photoParam, passwordParam, patronymicParam);
        }
      

	}

	public class MovieActorView
	{
		public int ID { get; set; }	
		public string FilmName { get; set; }
		public byte[] Poster { get; set; }
		public string Name { get; set; }
		public string Secondname { get; set; }
	}
}
