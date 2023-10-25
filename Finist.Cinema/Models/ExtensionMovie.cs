using Finist.Cinema.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finist.Cinema.Models
{

    public partial class Movie
    {
        public string AllActorsFilm
        {
            get
            {
                string allActors = string.Empty;
				
				allActors = string.Join(", ", Idactors.Select(act => $"{act.Name} {act.Secondname}"));

				return allActors;
            }
        }
        
        public string AllCountriesFilm
        {
            get
            {
                string allCountries =string.Join(", ", Idcountries.Select(country=>$"{country.NameCountry}"));
               
                return allCountries;
            }
        }


    }
    


}
    
    

