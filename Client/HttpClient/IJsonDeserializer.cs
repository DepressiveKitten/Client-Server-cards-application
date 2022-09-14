using System.Collections.Generic;

namespace CardHttpClient
{
    public interface IJsonDeserializer
    {
        /// <summary>
        /// Deserialize Json document into <see cref="HttpCard"/>.
        /// </summary>
        /// <param name="input">String with Json document.</param>
        /// <returns>Cards.</returns>
        public IEnumerable<HttpCard> Deserialize(string input);


        /// <summary>
        /// Serialize <see cref="HttpCard"/> into Json document.
        /// </summary>
        /// <param name="cards"><see cref="HttpCard"/> to serialize.</param>
        /// <returns>String with Json document.</returns>
        public string Serialize(HttpCard card);
    }
}
