using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace QuranTinglaBot.Entity
{
    public class Surah
    {
        [Key]
        [Required]
        public string FileId { get; set; }

        [Required]
        public string FileUniqueId { get; set; }
        
        [Required]
        [Range(1, 114, ErrorMessage = "Value for Surah {0} must be between {1} and {2}.")]
        public int Number { get; set; }
        
        public string ArabicName { get; set; }
        
        public string EnglishName { get; set; }
        
        public string UzbekName { get; set; }
        
    }
}