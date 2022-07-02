using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class Summoner
	{
		[Key]
		public string ID { get; set; } = string.Empty;
		public string AccountID { get; set; } = string.Empty;
		public string PUUID { get; set; } = string.Empty;
		public string SummonerName { get; set; } = string.Empty;
		public int ProfileIconID { get; set; }
		public int SummonerLevel { get; set; }
	}
}
