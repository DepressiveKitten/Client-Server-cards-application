namespace Cards.Services.InMemory
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <inheritdoc/>
    public class CardStorageService : ICardStorageService
    {
        private readonly CardsContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardStorageService"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public CardStorageService(CardsContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<int> CreateCardAsync(Card card)
        {
            this.context.Cards.Add(card);
            int rowsAffected = await this.context.SaveChangesAsync();
            return rowsAffected;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteCardAsync(int cardId)
        {
            var card = await this.context.Cards.FindAsync(cardId);
            if (card is null)
            {
                return false;
            }

            this.context.Cards.Remove(card);
            await this.context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Card> GetCardsAsync()
        {
            return this.context.Cards.AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public async Task<(bool, Card)> TryGetCardAsync(int cardId)
        {
            var card = await this.context.Cards.FindAsync(cardId);
            if (card is null)
            {
                return (false, null);
            }

            return (true, card);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateCardAsync(int cardId, Card card)
        {
            var upd = await this.context.Cards.FindAsync(cardId);

            if (upd is null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(card.Image))
            {
                upd.Image = card.Image;
            }

            if (!string.IsNullOrEmpty(card.Name))
            {
                upd.Name = card.Name;
            }

            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
