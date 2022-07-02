using System.ComponentModel.DataAnnotations;

namespace Back_end_Poro_Opresor.Models
{
    public class AdvancedGameStats
    {
        [Key]
        public int AdvancedGameStatsId { get; set; }
        public int StatsId { get; set; }
        public string ChampionName { get; set; } = String.Empty;
        public int DamageDealtToBuildings { get; set; }
        public int DamagDealtToObjectives { get; set; }
        public int DamageDealtToTowers { get; set; }
        public int DamageSelfMitigated { get; set; }
        public int DetectorWardsPlaced { get; set; }
        public int DragonKills { get; set; }
        public bool FirstBloodAssit { get; set; }
        public bool FirstBloodKill { get; set; }
        public bool FirstTowerAssits { get; set; }
        public bool FirstTowerKill { get; set; }
        public int GoldEarned { get; set; }
        public int GoldSpent { get; set; }
        public string TeamPosition { get; set; } = String.Empty;
        public int InhibitorKills { get; set; }
        public int InhibitorTakedowns { get; set; }
        public int InhibitorsLost { get; set; }
        public int MagicDamageDealtToChampions { get; set; }
        public int MagicDamageTaken { get; set; }
        public int LargetstCriticalStrike { get; set; }
        public int ObjectivesStolen { get; set; }
        public int PhysicalDamagDealtToChampions { get; set; }
        public int PhysicalDamageTaken { get; set; }
        public int Spell1Casts { get; set; }
        public int Spell2Casts { get; set; }
        public int Spell3Casts { get; set; }
        public int Spell4Casts { get; set; }
        public int Summoner1Casts { get; set; }
        public int Summoner1Id { get; set; }
        public int Summoner2Cast { get; set; }
        public int Summoner2Id { get; set; }
        public int TimeCCingOthers { get; set; }
        public int TotalDamageShieldedOnTeammates { get; set; }
        public int TrueDamageDealtToChampions { get; set; }
        public int TrueDamageTaken { get; set; }
        public int TurretKills { get; set; }
        public int TurretTakedowns { get; set; }
        public int TurretsLost { get; set; }
        public int VisionScore { get; set; }
        public int WardsKilled { get; set; }
        public int WardsPlaced { get; set; }
    }
}
