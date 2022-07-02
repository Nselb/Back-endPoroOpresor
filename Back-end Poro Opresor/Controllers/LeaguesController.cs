using Back_end_Poro_Opresor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Back_end_Poro_Opresor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        public GameDBContext db { get; set; }
        public LeaguesController(GameDBContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<List<League>> GetLeague(string id, string regionId)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
            string url = $"https://{regionId}.api.riotgames.com/lol/league/v4/entries/by-summoner/{id}";
            try
            {
                var res = await client.GetStringAsync(url);
                dynamic leagues = JArray.Parse(res);
                foreach (var item in leagues)
                {
                    int queueId = 0;
                    string queueType = item.queueType;
                    switch (queueType)
                    {
                        case "RANKED_FLEX_SR":
                            queueId = 1;
                            break;
                        case "RANKED_SOLO_5x5":
                            queueId = 2;
                            break;
                        default:
                            break;
                    }
                    bool updated = false;
                    foreach (var liga in db.Leagues.Where(l => l.SummonerId.Equals(id)).ToList())
                    {
                        if (liga.QueueId == queueId)
                        {
                            try
                            {
                                int wins = item.wins;
                                int losses = item.losses;
                                if (liga.Wins != wins || liga.Losses != losses)
                                {
                                    liga.SummonerId = item.summonerId;
                                    liga.FreshBlood = item.freshBlood;
                                    liga.HotStreak = item.hotStreak;
                                    liga.Inactive = item.inactive;
                                    liga.LeagueId = item.leagueId;
                                    liga.LeaguePoints = item.leaguePoints;
                                    liga.Losses = item.losses;
                                    liga.QueueId = queueId;
                                    liga.Rank = item.rank;
                                    liga.Tier = item.tier;
                                    liga.Veteran = item.veteran;
                                    liga.Wins = item.wins;
                                    db.Leagues.Update(liga);
                                    db.SaveChanges();
                                }
                                updated = true;
                            }
                            catch (Exception)
                            {
                                updated = false;
                            }
                        }
                    }
                    if (updated)
                    {
                        continue;
                    }
                    db.Leagues.Add(new League
                    {
                        SummonerId = item.summonerId,
                        FreshBlood = item.freshBlood,
                        HotStreak = item.hotStreak,
                        Inactive = item.inactive,
                        LeagueId = item.leagueId,
                        LeaguePoints = item.leaguePoints,
                        Losses = item.losses,
                        QueueId = queueId,
                        Rank = item.rank,
                        Tier = item.tier,
                        Veteran = item.veteran,
                        Wins = item.wins
                    });
                    db.SaveChanges();
                }
                return db.Leagues.Where(l => l.SummonerId.Equals(id)).ToList();
            }
            catch (Exception)
            {
                return new List<League>();
            }
        }
    }
}
