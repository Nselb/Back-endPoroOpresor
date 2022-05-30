using Back_end_Poro_Opresor.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;

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
        public async Task<string> PostUser([FromBody] User user)
        {
            try
            {
                if (db.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault() != null)
                {
                    return "Correo ya ha sido usado!";
                }
                Summoner s = await GetSummonerData(user);
                if (s != null)
                {
                    user.SummonerId = s.ID;
                    user.UserPassword = Crypto.HashPassword(user.UserPassword);
                    db.Users.Add(user);
                    db.SaveChanges();
                    return "Usuario creado";
                }
                return "No ha sido posible crear el usuario";
            }
            catch (Exception)
            {

                return "No ha sido posible crear el usuario";
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
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
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

        [HttpGet("{email}/{password}")]
        public User? Login(string email, string password)
        {
            User user = db.Users.Where(u => u.Email.Equals(email)
                                            && Crypto.VerifyHashedPassword(u.UserPassword, password))
                                .FirstOrDefault();
            return user;
        }
    }
}
