namespace Cards.Services.InMemory
{
    public static class ContextSeeder
    {
        public static void FillCards(CardsContext context, ICardFileStorage fileStorage)
        {
            context.Cards.AddRange(fileStorage.ReadAll());
            context.SaveChanges();
        }
    }
}
