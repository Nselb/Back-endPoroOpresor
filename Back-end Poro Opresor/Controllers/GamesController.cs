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
        [HttpGet]
        public async Task<List<Game>> GetGameData(string ID,string PUUID, string server)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
            string url = $"https://{Urls.GetRoutingValue(server)}{Urls.LeagueBaseUrl}/lol/match/v5/matches/by-puuid/{PUUID}/ids?type=ranked&start=0&count=50";
            List<string> matchIds = await client.GetFromJsonAsync<List<string>>(url);
            if (matchIds != null)
            {
                List<Game> games = new();
                List<GameStats> stats = new();
                List<AdvancedGameStats> astats = new();
                foreach (string matchId in matchIds)
                {
                    if (db.Games.Where(g => g.MatchId.Equals(matchId) && g.SummonerId.Equals(ID)).FirstOrDefault() != null)
                    {
                        continue;
                    }
                    try
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
                            SummonerId = ID,
                            QueueId = gameData.info.queueId,

                        };
                        foreach (var item in gameData.info.participants)
                        {
                            string summonerGameId = item.summonerId;
                            if (ID.Equals(summonerGameId))
                            {
                                try
                                {
                                    GameStats stat = new()
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
                                        Items7 = item.item6,
                                        GameId = game.GameId,
                                        ChampionId = item.championId,
                                        ChampLevel = item.champLevel,
                                        Deaths = item.deaths,
                                        Doubles = item.doubleKills,
                                        Triples = item.tripleKills,
                                        Pentas = item.pentaKills,
                                        TotalDamageDealtToChampions = item.totalDamageDealtToChampions,
                                        TotalMinions = item.totalMinionsKilled,
                                        PerkDefense = item.perks.statPerks.defense,
                                        PerkFlex = item.perks.statPerks.flex,
                                        PerkOffense = item.perks.statPerks.offense,
                                        PerkPStyle = item.perks.styles[0].style,
                                        PerkP1 = item.perks.styles[0].selections[0].perk,
                                        PerkP2 = item.perks.styles[0].selections[1].perk,
                                        PerkP3 = item.perks.styles[0].selections[2].perk,
                                        PerkP4 = item.perks.styles[0].selections[3].perk,
                                        PerkSStyle = item.perks.styles[1].style,
                                        PerkS1 = item.perks.styles[1].selections[0].perk,
                                        PerkS2 = item.perks.styles[1].selections[1].perk
                                    };
                                    stats.Add(stat);
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
                                    astats.Add(advancedGameStats);
                                }
                                catch (Exception)
                                {
                                    return new();
                                }
                                game.Score = (item.kills - item.deaths) * item.totalDamageDealtToChampions + item.totalMinionsKilled;
                                games.Add(game);
                            }
                        } 
                    }
                    catch (Exception)
                    {
                        return new();
                    }
                }
                foreach (var game in games)
                {
                    db.Games.Add(game);
                }
                db.SaveChanges();
                foreach (var game in games)
                {
                    foreach (var stat in stats)
                    {
                        if (stat.GameId == 0)
                        {
                            stat.GameId = game.GameId;
                            db.GameStats.Add(stat);
                            break;
                        }
                    }
                }
                db.SaveChanges();
                foreach (var stat in stats)
                {
                    foreach (var astat in astats)
                    {
                        if (astat.StatsId == 0)
                        {
                            astat.StatsId = stat.StatsId;
                            db.AdvancedGameStats.Add(astat);
                            break;
                        }
                    }
                }
                db.SaveChanges();
                return db.Games.Where(g => g.SummonerId.Equals(ID)).ToList();
            }
            return new();
        }
        [HttpGet("{id}")]
        public List<Game> GetGames(string id)
        {
            return db.Games.Where(g => g.SummonerId.Equals(id)).ToList();
        }
        [HttpGet("{championId}/{summonerId}")]
        public List<Game> GetGamesByChampion(int championId, string summonerId)
        {
            List<Game> games = db.Games.Where(g => g.SummonerId.Equals(summonerId)).ToList();
            List<Game> result = new();
            foreach (var item in games)
            {
                GameStats gs = db.GameStats.Where(gs => gs.GameId == item.GameId && gs.ChampionId == championId).FirstOrDefault();
                if (gs != null)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        [HttpGet("1")]
        public List<Game> GetGamesById(int gameId)
        {
            return db.Games.Where(g => g.GameId == gameId).ToList();
        }
        [HttpGet("2/{date1}/{date2}")]
        public List<Game> GetGamesByDate(long date1, long date2)
        {
            List<Game> games = db.Games.Where(g => g.GameStartTimestamp >= date1 && g.GameStartTimestamp <= date2).ToList();
            games.OrderBy(g => g.Score);
            return games;
        }
    }
}
