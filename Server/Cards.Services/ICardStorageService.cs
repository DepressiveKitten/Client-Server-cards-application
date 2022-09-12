using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cards.Services
{
    /// <summary>
    /// Represent a storage for cards.
    /// </summary>
    public interface ICardStorageService
    {
        /// <summary>
        /// Shows all cards.
        /// </summary>
        /// <returns>A <see cref="IAsyncEnumerable{T}"> od <see cref="Card"></returns>
        IAsyncEnumerable<Card> GetCardsAsync();

        /// <summary>
        /// Try to show a card with specified identifier.
        /// </summary>
        /// <param name="cardId">A card identifier</param>
        /// <returns>Returns true if a card is returned; otherwise false.</returns>
        Task<(bool, Card)> TryGetCardAsync(int cardId);

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="card">A <see cref="Card"> to create.</param>
        /// <returns>An identifier of a created card.</returns>
        Task<int> CreateCardAsync(Card card);

        /// <summary>
        /// Destroys an existed card.
        /// </summary>
        /// <param name="cardId">A card identifier.</param>
        /// <returns>True if a card is destroyed; otherwise false.</returns>
        Task<bool> DeleteCardAsync(int cardId);

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <param name="cardId">A card identifier.</param>
        /// <param name="card">A <see cref="Card"/>.</param>
        /// <returns>True if a card is updated; otherwise false.</returns>
        Task<bool> UpdateCardAsync(int cardId, Card card);
    }
}
