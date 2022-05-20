using Back_end_Poro_Opresor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Back_end_Poro_Opresor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GameDBContext db;

        public UsersController(GameDBContext context)
        {
            db = context;
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }
        [HttpGet("{id}")]
        public User? GetUser(int id)
        {
            return db.Users.Find(id);
        }
        [HttpPost]
        public async Task<bool> PostUser([FromBody] User user)
        {
            try
            {
                Summoner s = await GetSummonerData(user);
                if (s != null)
                {
                    user.SummonerId = s.ID;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<Summoner?> GetSummonerData(User user)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
            string url = $"https://{user.RegionId}{Urls.LeagueBaseUrl}/lol/summoner/v4/summoners/by-name/{user.SummonerName}";
            Summoner result = await client.GetFromJsonAsync<Summoner>(url);
            if (result != null)
            {
                result.SummonerName = user.SummonerName;
                try
                {
                    db.Summoners.Add(result);
                    db.SaveChanges();
                    await GetGameData(result, user.RegionId);
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        private async Task<bool> GetGameData(Summoner summoner, string server)
        {
            HttpClient client = new ();
            client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
            string url = $"https://{Urls.GetRoutingValue(server)}{Urls.LeagueBaseUrl}/lol/match/v5/matches/by-puuid/{summoner.PUUID}/ids?count=10";
            List<string> matchIds = await client.GetFromJsonAsync<List<string>>(url);
            if (matchIds != null)
            {
                foreach (string matchId in matchIds)
                {
                    client = new();
                    client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
                    url = $"https://{Urls.GetRoutingValue(server)}{Urls.LeagueBaseUrl}/lol/match/v5/matches/{matchId}";
                    var res = await client.GetStringAsync(url);
                    dynamic gameData = JObject.Parse(res);
                    Game game = new ()
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
                                GameStats stats = new()
                                {
                                    Assists = int.Parse(item.assists as string),
                                    ChampLevel = int.Parse(item.championLevel as string),
                                    ChampionId = int.Parse(item.championId as string),
                                    Deaths = int.Parse(item.deaths as string),
                                    Doubles = int.Parse(item.doubleKills as string),
                                    GameId = game.GameId,
                                    Items1 = int.Parse(item.item0 as string),
                                    Items2 = int.Parse(item.item1 as string),
                                    Items3 = int.Parse(item.item2 as string),
                                    Items4 = int.Parse(item.item3 as string),
                                    Items5 = int.Parse(item.item4 as string),
                                    Items6 = int.Parse(item.item5 as string),
                                    Kills = int.Parse(item.kills as string),
                                    Pentas = int.Parse(item.pentaKills as string),
                                    Quadras = int.Parse(item.quadraKills as string),
                                    TotalDamageDealtToChampions = int.Parse(item.totalDamageDealtToChampions as string),
                                    TotalMinions = int.Parse(item.totalMinionsKilled as string),
                                    Triples = int.Parse(item.tripleKills as string),
                                    Win = bool.Parse(item.wins as string),
                                };

                                db.GameStats.Add(stats);
                                db.SaveChanges();
                                AdvancedGameStats advancedGameStats = new()
                                {
                                    StatsId = stats.StatsId,
                                    ChampionName = item.championName,
                                    DamagDealtToObjectives = int.Parse(item.damagDealtToObjectives),
                                    DamageDealtToBuildings = int.Parse(item.damageDealtToBuildings),
                                    DamageDealtToTowers = int.Parse(item.damageDealtToTowers),
                                    DamageSelfMitigated = int.Parse(item.damageSelfMitigated),
                                    DetectorWardsPlaced = int.Parse(item.detectorWardsPlaced),
                                    DragonKills = int.Parse(item.dragonKills),
                                    FirstBloodAssit = item.firstBloodAssit,
                                    FirstBloodKill = item.firstBloodKill,
                                    FirstTowerAssits = item.firstTowerAssits,
                                    FirstTowerKill = item.firstTowerKill,
                                    GoldEarned = int.Parse(item.goldEarned),
                                    GoldSpent = int.Parse(item.goldSpent),
                                    InhibitorKills = int.Parse(item.inhibitorKills),
                                    InhibitorTakedowns = int.Parse(item.inhibitorTakedowns),
                                    InhibitorsLost = int.Parse(item.inhibitorsLost),
                                    LargetstCriticalStrike = int.Parse(item.largetstCriticalStrike),
                                    MagicDamageDealtToChampions = int.Parse(item.magicDamageDealtToChampions),
                                    MagicDamageTaken = int.Parse(item.magicDamageTaken),
                                    ObjectivesStolen = int.Parse(item.objectivesStolen),
                                    PhysicalDamagDealtToChampions = int.Parse(item.physicalDamageDealtToChampions),
                                    PhysicalDamageTaken = int.Parse(item.physicalDamageTaken),
                                    Spell1Casts = int.Parse(item.spell1Casts),
                                    Spell2Casts = int.Parse(item.spell2Casts),
                                    Spell3Casts = int.Parse(item.spell3Casts),
                                    Spell4Casts = int.Parse(item.spell4Casts),
                                    Summoner1Casts = int.Parse(item.summoner1Casts),
                                    Summoner1Id = int.Parse(item.summoner1Id),
                                    Summoner2Cast = int.Parse(item.summoner2Casts),
                                    Summoner2Id = int.Parse(item.summoner2Id),
                                    TeamPosition = item.teamPosition,
                                    TimeCCingOthers = int.Parse(item.timeCCingOthers),
                                    TotalDamageShieldedOnTeammates = int.Parse(item.totalDamageShieldedOnTeammates),
                                    TrueDamageDealtToChampions = int.Parse(item.trueDamageDealtToChampions),
                                    TrueDamageTaken = int.Parse(item.trueDamageTaken),
                                    TurretKills = int.Parse(item.turretKills),
                                    TurretTakedowns = int.Parse(item.turretTakedowns),
                                    TurretsLost = int.Parse(item.turretsLost),
                                    VisionScore = int.Parse(item.visionScore),
                                    WardsKilled = int.Parse(item.wardsKilled),
                                    WardsPlaced = int.Parse(item.wardsPlaced)
                                };
                                db.AdvancedGameStats.Add(advancedGameStats);
                                db.SaveChanges();

                                return true;
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

        [HttpDelete("{id}")]
        public bool DeleteUser(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                if (user != null)
                {

                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            } catch (Exception)
            {
                return false;
            }
        }
        [HttpPut("{id}")]
        public bool ModifyUser(int id, [FromBody] User user)
        {
            try
            {
                if (user.UserId == id)
                {
                    db.Users.Update(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
