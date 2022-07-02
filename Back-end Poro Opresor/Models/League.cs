using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class League
	{
		[Key]
		public int Id { get; set; }
		public string LeagueId { get; set; } = string.Empty;
		public string SummonerId { get; set; } = string.Empty;
		public int QueueId { get; set; }
		public int LeaguePoints { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public bool Veteran { get; set; }
		public bool Inactive { get; set; }
		public bool FreshBlood { get; set; }
		public bool HotStreak { get; set; }
		public string Tier { get; set; } = string.Empty;
		public string Rank { get; set; } = string.Empty;
    }
}
