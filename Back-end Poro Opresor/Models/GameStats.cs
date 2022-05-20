using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class GameStats
	{
		[Key]
		public int StatsId { get; set; }
		public int GameId { get; set; }
		public int ChampLevel { get; set; }
		public int ChampionId { get; set; }
		public bool Win { get; set; }
		public int Kills { get; set; }
		public int Deaths { get; set; }
		public int Assists { get; set; }
		public int TotalMinions { get; set; }
		public int TotalDamageDealtToChampions { get; set; }
		public int Doubles { get; set; }
		public int Triples { get; set; }
		public int Quadras { get; set; }
		public int Pentas { get; set; }
		public int Items1 { get; set; }
		public int Items2 { get; set; }
		public int Items3 { get; set; }
		public int Items4 { get; set; }
		public int Items5 { get; set; }
		public int Items6 { get; set; }
	}
}
