using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceApi.Data;
using SpaceApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Controllers
{
    [ApiController]
    [Route("api/leaderboard")]
    public class LeaderboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeaderboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardEntry>>> GetLeaderboard()
        {
            var leaderboard = await _context.Leaderboard.ToListAsync();
            return leaderboard;
        }

        [HttpPost]
        public async Task<ActionResult> CreateLeaderboardEntry(LeaderboardEntry entry)
        {
            // Make sure 'Id' is not set explicitly
            entry.Id = 0;

            _context.Entry(entry).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Ok();
        }


        // You can add more methods as needed for updating, deleting, etc.
    }
}
