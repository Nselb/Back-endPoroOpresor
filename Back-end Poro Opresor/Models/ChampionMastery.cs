using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class ChampionMastery
	{
		[Key]
		public int ChampionMasteryId { get; set; }
		public string SummonerId { get; set; } = string.Empty;
		public string ChampionName { get; set; } = string.Empty;
		public int ChampionId { get; set; }
		public int ChampionLevel { get; set; }
		public int ChampionPoints { get; set; }
		public bool ChestGranted { get; set; }
		public int TokensEarned { get; set; }
	}
}
