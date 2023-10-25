using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finist.Cinema.Models
{
	public partial class Ticket
	{
		public string AllFoodChecker
		{
			get
			{
				var foodster=string.Join(", ", Idfoods.Select(food=>$"{food.NameFood}"));
				

				return foodster;
			}
		}
	}
}
