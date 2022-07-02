using Back_end_Poro_Opresor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Poro_Opresor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStatsController : ControllerBase
    {
        private readonly GameDBContext db;

        public GameStatsController(GameDBContext context)
        {
            db = context;
        }

        [HttpGet]
        public GameStats GetGameStats(int gameId)
        {
            return db.GameStats.Where(gs => gs.GameId == gameId).FirstOrDefault();
        }
    }
}
