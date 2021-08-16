using System.Collections.Generic;
using Newtonsoft.Json;

namespace QuranTinglaBot.Dto
{
    public class OyatEditionsResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<OyatEdition> Editions { get; set; }
    }
}