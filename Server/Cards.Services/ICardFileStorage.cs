using System.Collections.Generic;

namespace Cards.Services
{
    /// <summary>
    /// Represent a service that saves and loads all cards from file.
    /// </summary>
    public interface ICardFileStorage
    {
        /// <summary>
        /// Reads all cards from file.
        /// </summary>
        /// <returns>All cards in file.</returns>
        public IEnumerable<Card> ReadAll();

        /// <summary>
        /// Rewrites all cards to file.
        /// </summary>
        /// <param name="cards">Cards to save to fire.</param>
        /// <returns>True if a cards are saved; otherwise false. </returns>
        public bool WriteAll(IEnumerable<Card> cards);
    }
}
