namespace CardJsonDeserialization
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using AutoMapper;
    using CardHttpClient;

    /// <inheritdoc/>
    public class JsonCardSerializer : IJsonDeserializer
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
        public IEnumerable<HttpCard> Deserialize(string input)
        {
            List<JsonCard> document = JsonSerializer.Deserialize<List<JsonCard>>(input);

            return from item in document
                   select this.Mapper.Map<JsonCard, HttpCard>(item);
        }

        /// <inheritdoc/>
        public string Serialize(HttpCard card)
        {
            JsonCard document = this.Mapper.Map<HttpCard, JsonCard>(card);
            return JsonSerializer.Serialize<JsonCard>(document);
        }
    }
}
