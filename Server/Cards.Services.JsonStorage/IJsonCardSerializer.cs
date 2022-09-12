using System.Collections.Generic;
using Cards.Services;

namespace Cards.Services.JsonStorage
{
    /// <summary>
    /// Serialize and Deserialize <see cref="Card"/> collection into Json document./>
    /// </summary>
    public interface IJsonCardSerializer
    {
        /// <summary>
        /// Deserialize Json document into <see cref="Card"/>
        /// </summary>
        /// <param name="input">String with Json document.</param>
        /// <returns>Cards.</returns>
        public IEnumerable<Card> Deserialize(string input);

        /// <summary>
        /// Serialize <see cref="Card"/> into Json document.
        /// </summary>
        /// <param name="cards">collection of <see cref="Card"/> to serialize.</param>
        /// <returns>String with Json document.</returns>
        public string Deserialize(IEnumerable<Card> cards);
    }
}
