using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Cards.Services.JsonStorage
{
    /// <inheritdoc/>
    class CardsJsonStorage : ICardFileStorage
    {
        private readonly static string storageNameNode = "json-storage-name";

        public CardsJsonStorage(IJsonCardSerializer serializer, IConfiguration configuration)
        {
            this.Serializer = serializer;
            this.Configuration = configuration;
        }

        public IJsonCardSerializer Serializer { get; set; }
        public IConfiguration Configuration { get; set; }

        /// <inheritdoc/>
        public IEnumerable<Card> ReadAll()
        {
            if (!File.Exists(Configuration[storageNameNode]))
            {
                return Enumerable.Empty<Card>();
            }

            string document;

            using (StreamReader reader = new StreamReader(Configuration[storageNameNode]))
            {
                document = reader.ReadToEnd();
            }

            return Serializer.Deserialize(document);
        }

        /// <inheritdoc/>
        public bool WriteAll(IEnumerable<Card> cards)
        {
            using (StreamWriter writer = new StreamWriter(Configuration[storageNameNode],false))
            {
                writer.Write(Serializer.Serialize(cards));
            }

            return true;
        }
    }
}
