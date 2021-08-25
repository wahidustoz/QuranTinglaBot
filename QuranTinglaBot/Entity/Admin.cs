using System.ComponentModel.DataAnnotations;

namespace QuranTinglaBot.Entity
{
    public class Admin
    {
        [Key]
        [Required]
        public string UserId { get; set; }
        
        public string Username { get; set; }
        
        public string FullName { get; set; }
        
        
    }
}