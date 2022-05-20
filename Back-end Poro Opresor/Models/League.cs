using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class League
	{
		[Key]
		public int LeagueId { get; set; }
		public string SummonerId { get; set; }
		public int QueueId { get; set; }
		public int LeaguePoints { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public bool Veteran { get; set; }
		public bool Inactive { get; set; }
		public bool FreshBlood { get; set; }
		public bool HotStreak { get; set; }
	}
}
