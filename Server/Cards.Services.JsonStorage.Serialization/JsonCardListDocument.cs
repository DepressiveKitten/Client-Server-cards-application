namespace Cards.Services.JsonStorage.Serialization
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Element for Json Deserialization.
    /// </summary>
    public class JsonCardListDocument
    {
        /// <summary>
        /// Gets or Sets page data.
        /// </summary>
        [JsonPropertyName("cards")]
        public List<JsonCard> JsonCards { get; set; }
    }
}
