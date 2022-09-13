namespace Cards.Services.InMemory
{
    /// <summary>
    /// Fills in database.
    /// </summary>
    public static class ContextSeeder
    {
        /// <summary>
        /// Fills in database.
        /// </summary>
        public static void FillCards(CardsContext context, ICardFileStorage fileStorage)
        {
            context.Cards.AddRange(fileStorage.ReadAll());
            context.SaveChanges();
        }
    }
}
