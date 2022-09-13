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
        public string Image { get; set; }
    }
}
