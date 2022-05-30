using Back_end_Poro_Opresor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Poro_Opresor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonersController : ControllerBase
    {
        private static GameDBContext db { get; set; }
        

        public SummonersController(GameDBContext context)
        {
            db = context;
        }

        [HttpGet]
        public List<Summoner> GetSummoners()
        {
            return db.Summoners.ToList();
        }
        [HttpGet("{summonerId}")]
        public Summoner GetSummoner(string summonerId)
        {
            return db.Summoners.Where(s => s.ID.Equals(summonerId)).FirstOrDefault();
        }
        [HttpPost]
        public bool PostSummoner( [FromBody] Summoner summoner)
        {
            try
            {
                db.Summoners.Add(summoner);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
