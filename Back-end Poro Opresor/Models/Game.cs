using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class Game
	{
		[Key]
		public int GameId { get; set; }
		public string SummonerId { get; set; }
		public string MatchId { get; set; }
		public long GameStartTimestamp { get; set; }
		public int GameDuration { get; set; }
		public string GameMode { get; set; }
	}
}
