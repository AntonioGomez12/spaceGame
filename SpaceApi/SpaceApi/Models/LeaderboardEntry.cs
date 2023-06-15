using System.ComponentModel.DataAnnotations;

namespace SpaceApi.Models
{
    public class LeaderboardEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public int Score { get; set; }
    }
}