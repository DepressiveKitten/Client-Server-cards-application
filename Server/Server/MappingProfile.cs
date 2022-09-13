namespace CardsApiApp
{
    using AutoMapper;
    using Cards.Services;
    using Cards.Services.JsonStorage.Serialization;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Card, JsonCard>();
            this.CreateMap<JsonCard, Card>();
        }
    }
}
