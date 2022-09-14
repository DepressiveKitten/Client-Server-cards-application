using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardHttpClient
{
    public interface ICardHttpClient
    {
        /// <summary>
        /// Shows all cards.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"> of <see cref="HttpCard"> elements.</returns>
        Task<IEnumerable<HttpCard>> GetCardsAsync();

        /// <summary>
        /// Creates a new card.
        /// </summary>
        /// <param name="card">A <see cref="HttpCard"> to create.</param>.
        /// <returns>1 if card was created.</returns>
        Task<bool> CreateCardAsync(HttpCard card);

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
        /// <param name="card">A <see cref="HttpCard"/>.</param>
        /// <returns>True if a card is updated; otherwise false.</returns>
        Task<bool> UpdateCardAsync(int cardId, HttpCard card);
    }
}
