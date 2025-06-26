namespace Odin_Todo_Backend.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string JsonData { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;  
    }
}
