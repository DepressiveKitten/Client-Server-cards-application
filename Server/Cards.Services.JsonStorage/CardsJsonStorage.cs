namespace Cards.Services.JsonStorage
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    /// <inheritdoc/>
    public class CardsJsonStorage : ICardFileStorage
    {
        private static readonly string StorageNameNode = "json-storage-name";

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsJsonStorage"/> class.
        /// </summary>
        /// <param name="serializer">serializer.</param>
        /// <param name="configuration">app configuration.</param>
        public CardsJsonStorage(IJsonCardSerializer serializer, IConfiguration configuration)
        {
            this.Serializer = serializer;
            this.Configuration = configuration;
        }

        private IJsonCardSerializer Serializer { get; }

        private IConfiguration Configuration { get; }

        /// <inheritdoc/>
        public IEnumerable<Card> ReadAll()
        {
            if (!File.Exists(this.Configuration[StorageNameNode]))
            {
                return Enumerable.Empty<Card>();
            }

            string document;

            using (StreamReader reader = new StreamReader(this.Configuration[StorageNameNode]))
            {
                document = reader.ReadToEnd();
            }

            return this.Serializer.Deserialize(document);
        }

        /// <inheritdoc/>
        public bool WriteAll(IEnumerable<Card> cards)
        {
            using (StreamWriter writer = new StreamWriter(this.Configuration[StorageNameNode], false))
            {
                writer.Write(this.Serializer.Serialize(cards));
            }

            return true;
        }
    }
}
