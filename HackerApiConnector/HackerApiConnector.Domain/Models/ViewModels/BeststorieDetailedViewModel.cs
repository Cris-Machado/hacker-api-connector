using System.Text.Json.Serialization;

namespace HackerApiConnector.Domain.Models.ViewModels
{
    public class BeststorieDetailedViewModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("postedBy")]
        public string PostedBy { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("commentCount")]
        public int CommentCount { get; set; }
    }

}
