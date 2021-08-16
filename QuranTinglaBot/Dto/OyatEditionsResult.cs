using System.Collections.Generic;

namespace QuranTinglaBot.Dto
{
    public class OyatEditionsResult
    {
        public bool IsSuccess { get; set; }
        
        public string ErrorMessage { get; set; }
        
        public int StatusCode { get; set; }
        
        public List<OyatEdition> Editions { get; set; }
        
    }
}