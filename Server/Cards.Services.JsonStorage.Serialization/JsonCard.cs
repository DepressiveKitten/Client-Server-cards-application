using System.Text.Json.Serialization;

namespace Cards.Services.JsonStorage.Serialization
{
    /// <summary>
    /// Element for Json serialization.
    /// </summary>
    public class JsonCard
    {
        /// <summary>
        /// Gets or sets a card id.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a card name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a card image.
        /// </summary>
#pragma warning disable CA1819 // Properties should not return arrays
        [JsonPropertyName("image")]
        public byte[] Image { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
