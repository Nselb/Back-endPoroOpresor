using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public string SummonerId { get; set; }
		public string RegionId { get; set; }
		public string SummonerName { get; set; }
		public string Email { get; set; }
		public string UserPassword { get; set; }
		public bool IsAdmin { get; set; }
	}
}
