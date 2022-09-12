using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cards.Services.InMemory
{
    class CardStorageService : ICardStorageService
    {
        private readonly CardsContext context;

        public CardStorageService(CardsContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateCardAsync(Card card)
        {
            this.context.Cards.Add(card);
            int rowsAffected = await this.context.SaveChangesAsync();
            return rowsAffected;
        }

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

        public IAsyncEnumerable<Card> GetCardsAsync()
        {
            return this.context.Cards.AsAsyncEnumerable();
        }

        public async Task<(bool, Card)> TryGetCardAsync(int cardId)
        {
            var card = await this.context.Cards.FindAsync(cardId);
            if (card is null)
            {
                return (false, null);
            }

            return (true, card);
        }

        public async Task<bool> UpdateCardAsync(int cardId, Card card)
        {
            var upd = await this.context.Cards.FindAsync(cardId);

            if (upd is null)
            {
                return false;
            }

            if (card.Image != Array.Empty<byte>())
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
