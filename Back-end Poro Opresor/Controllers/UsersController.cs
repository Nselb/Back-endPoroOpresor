﻿using Back_end_Poro_Opresor.Models;
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
            HttpClient client = new();
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

        [HttpDelete("{id}")]
        public bool DeleteUser(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                if (user == null) return false;
                Summoner s = db.Summoners.Find(user.SummonerId);
                if (s == null) return false;
                foreach (var game in db.Games.Where(u => u.SummonerId.Equals(s.ID)))
                {
                    GameStats stat = db.GameStats.Where(stat => stat.GameId == game.GameId).First();
                    AdvancedGameStats advstat = db.AdvancedGameStats.Where(adv => adv.StatsId == stat.StatsId).First();
                    db.AdvancedGameStats.Remove(advstat);
                    db.GameStats.Remove(stat);
                    db.Games.Remove(game);
                }
                db.Users.Remove(user);
                db.SaveChanges();
                db.Summoners.Remove(s);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
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
