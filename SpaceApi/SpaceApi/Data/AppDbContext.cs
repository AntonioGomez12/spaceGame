using Microsoft.EntityFrameworkCore;
using SpaceApi.Models;

namespace SpaceApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<LeaderboardEntry> Leaderboard { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}