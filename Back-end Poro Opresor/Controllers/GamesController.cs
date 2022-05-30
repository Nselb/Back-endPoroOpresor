using Back_end_Poro_Opresor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Back_end_Poro_Opresor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameDBContext db;

        public GamesController(GameDBContext context)
        {
            db = context;
        }

        private async Task<bool> GetGameData(Summoner summoner, string server)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
            string url = $"https://{Urls.GetRoutingValue(server)}{Urls.LeagueBaseUrl}/lol/match/v5/matches/by-puuid/{summoner.PUUID}/ids?count=10";
            List<string> matchIds = await client.GetFromJsonAsync<List<string>>(url);
            if (matchIds != null)
            {
                foreach (string matchId in matchIds)
                {
                    if (db.Games.Where(g => g.MatchId.Equals(matchId) && g.SummonerId.Equals(summoner.ID)).FirstOrDefault() != null)
                    {
                        continue;
                    }
                    client = new();
                    client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
                    url = $"https://{Urls.GetRoutingValue(server)}{Urls.LeagueBaseUrl}/lol/match/v5/matches/{matchId}";
                    var res = await client.GetStringAsync(url);
                    dynamic gameData = JObject.Parse(res);
                    Game game = new()
                    {
                        GameStartTimestamp = gameData.info.gameStartTimestamp,
                        GameDuration = gameData.info.gameDuration,
                        GameMode = gameData.info.gameMode,
                        MatchId = matchId,
                        SummonerId = summoner.ID
                    };
                    db.Games.Add(game);
                    db.SaveChanges();
                    foreach (var item in gameData.info.participants)
                    {
                        string summonerGameId = item.summonerId;
                        if (summoner.ID.Equals(summonerGameId))
                        {
                            try
                            {
                                int coso = item.assists;
                                GameStats stats = new()
                                {
                                    Assists = item.assists,
                                    Kills = item.kills,
                                    Quadras = item.quadraKills,
                                    Win = item.win,
                                    Items1 = item.item0,
                                    Items2 = item.item1,
                                    Items3 = item.item2,
                                    Items4 = item.item3,
                                    Items5 = item.item4,
                                    Items6 = item.item5,
                                    GameId = game.GameId,
                                    ChampionId = item.championId,
                                    ChampLevel = item.champLevel,
                                    Deaths = item.deaths,
                                    Doubles = item.doubleKills,
                                    Triples = item.tripleKills,
                                    Pentas = item.pentaKills,
                                    TotalDamageDealtToChampions = item.totalDamageDealtToChampions,
                                    TotalMinions = item.totalMinionsKilled
                                };
                                db.GameStats.Add(stats);
                                db.SaveChanges();
                                AdvancedGameStats advancedGameStats = new()
                                {
                                    ChampionName = item.championName,
                                    DamagDealtToObjectives = item.damageDealtToObjectives,
                                    DamageDealtToBuildings = item.damageDealtToBuildings,
                                    DamageDealtToTowers = item.damageDealtToTurrets,
                                    DamageSelfMitigated = item.damageSelfMitigated,
                                    DetectorWardsPlaced = item.detectorWardsPlaced,
                                    DragonKills = item.dragonKills,
                                    FirstBloodAssit = item.firstBloodAssist,
                                    FirstBloodKill = item.firstBloodKill,
                                    FirstTowerAssits = item.firstTowerAssist,
                                    FirstTowerKill = item.firstTowerKill,
                                    GoldEarned = item.goldEarned,
                                    GoldSpent = item.goldSpent,
                                    InhibitorKills = item.inhibitorKills,
                                    InhibitorsLost = item.inhibitorsLost,
                                    InhibitorTakedowns = item.inhibitorTakedowns,
                                    LargetstCriticalStrike = item.largestCriticalStrike,
                                    MagicDamageDealtToChampions = item.magicDamageDealtToChampions,
                                    MagicDamageTaken = item.magicDamageTaken,
                                    ObjectivesStolen = item.objectivesStolen,
                                    PhysicalDamagDealtToChampions = item.physicalDamageDealtToChampions,
                                    PhysicalDamageTaken = item.physicalDamageTaken,
                                    Spell1Casts = item.spell1Casts,
                                    Spell2Casts = item.spell2Casts,
                                    Spell3Casts = item.spell3Casts,
                                    Spell4Casts = item.spell4Casts,
                                    StatsId = stats.StatsId,
                                    Summoner1Casts = item.summoner1Casts,
                                    Summoner1Id = item.summoner1Id,
                                    Summoner2Cast = item.summoner2Casts,
                                    Summoner2Id = item.summoner2Id,
                                    TeamPosition = item.teamPosition,
                                    TimeCCingOthers = item.timeCCingOthers,
                                    TotalDamageShieldedOnTeammates = item.totalDamageShieldedOnTeammates,
                                    TrueDamageDealtToChampions = item.trueDamageDealtToChampions,
                                    TrueDamageTaken = item.trueDamageTaken,
                                    TurretKills = item.turretKills,
                                    TurretsLost = item.turretsLost,
                                    TurretTakedowns = item.turretTakedowns,
                                    VisionScore = item.visionScore,
                                    WardsKilled = item.wardsKilled,
                                    WardsPlaced = item.wardsPlaced
                                };
                                db.AdvancedGameStats.Add(advancedGameStats);
                                db.SaveChanges();
                            }
                            catch (Exception)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
