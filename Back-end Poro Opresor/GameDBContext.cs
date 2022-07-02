using Back_end_Poro_Opresor.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_end_Poro_Opresor
{
    public class GameDBContext : DbContext
    {
        public GameDBContext(DbContextOptions<GameDBContext> options) : base (options) { }    
        public DbSet<User> Users { get; set; }
        public DbSet<Summoner> Summoners { get; set; }
        public DbSet<GameStats> GameStats { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<AdvancedGameStats> AdvancedGameStats { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<ChampionMastery> ChampionMastery { get; set; }
    }
}
