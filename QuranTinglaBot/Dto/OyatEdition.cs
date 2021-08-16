using Newtonsoft.Json;

namespace QuranTinglaBot.Dto
{
    public class OyatEdition
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("englishName")]
        public string EnglishName { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}