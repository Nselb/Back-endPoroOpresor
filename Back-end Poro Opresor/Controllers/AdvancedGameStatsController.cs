using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Poro_Opresor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvancedGameStatsController : ControllerBase
    {
        private readonly GameDBContext db;

        public AdvancedGameStatsController(GameDBContext context)
        {
            db = context;
        }
    }
}
