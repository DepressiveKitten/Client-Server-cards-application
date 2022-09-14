namespace CardJsonDeserialization
{
    using System.Text.Json.Serialization;

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
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
