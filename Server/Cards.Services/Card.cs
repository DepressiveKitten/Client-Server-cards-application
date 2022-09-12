namespace Cards.Services
{
    /// <summary>
    /// Represents an informational card.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Gets or sets a card id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a card name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a card image.
        /// </summary>
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] Image { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
