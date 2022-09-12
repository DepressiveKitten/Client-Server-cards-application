using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cards.Services.JsonStorage.Serialization
{
    /// <summary>
    /// Element for Json Deserialization.
    /// </summary>
    public class JsonCardListDocument
    {
        /// <summary>
        /// Gets or Sets page data.
        /// </summary>
        [JsonPropertyName("cards")]
        public List<JsonCard> jsonCards { get; set; }
    }
}
