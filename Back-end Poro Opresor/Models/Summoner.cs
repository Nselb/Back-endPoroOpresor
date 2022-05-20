using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
	public class Summoner
	{
		[Key]
		public string ID { get; set; }
		public string AccountID { get; set; }
		public string PUUID { get; set; }
		public string SummonerName { get; set; }
		public int ProfileIconID { get; set; }
		public int SummonerLevel { get; set; }
	}
}
