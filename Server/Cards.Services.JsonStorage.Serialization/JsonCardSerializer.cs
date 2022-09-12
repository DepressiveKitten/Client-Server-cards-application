using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;

namespace Cards.Services.JsonStorage.Serialization
{
    /// <inheritdoc/>
    public class JsonCardSerializer : IJsonCardSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonCardSerializer"/> class.
        /// </summary>
        /// <param name="mapper">Mapper from JsonCard to Card.</param>
        public JsonCardSerializer(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        private IMapper Mapper { get; }

        /// <inheritdoc/>
        public IEnumerable<Card> Deserialize(string input)
        {
            JsonCardListDocument document = JsonSerializer.Deserialize<JsonCardListDocument>(input);

            return from item in document.jsonCards
                   select this.Mapper.Map<JsonCard, Card>(item);
        }

        /// <inheritdoc/>
        public string Serialize(IEnumerable<Card> cards)
        {
            JsonCardListDocument document = new JsonCardListDocument();
            document.jsonCards = (from card in cards
                                 select this.Mapper.Map<Card, JsonCard>(card)).ToList();
            return JsonSerializer.Serialize<JsonCardListDocument>(document);
        }
    }
}
