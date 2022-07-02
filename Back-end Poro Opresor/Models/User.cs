using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public string SummonerId { get; set; } = string.Empty;
		public string RegionId { get; set; } = string.Empty;
		public string SummonerName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string UserPassword { get; set; } = string.Empty;
		public bool IsAdmin { get; set; }
	}
}
