using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace CardHttpClient
{
    /// <inheritdoc/>
    public class CardClient : ICardHttpClient
    {
        private readonly string ServerUriNode = "server-uri";
        private readonly string UriPathDelimeter = "/";
        private readonly string FailedToConnectMessage = "Failed to connect to server";
        private readonly string CardNotExistMessage = "No card with selected id on server";

        public CardClient(IConfiguration configuration, HttpClient httpClient, IJsonDeserializer deserializer)
        {
            this.Configuration = configuration;
            this.HttpClient = httpClient;
            JsonDeserializer = deserializer;
        }

        private IConfiguration Configuration { get; }

        private HttpClient HttpClient { get; }

        private IJsonDeserializer JsonDeserializer { get; }

        /// <inheritdoc/>
        public async Task<bool> CreateCardAsync(HttpCard card)
        {
            HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Post, Configuration[ServerUriNode] + UriPathDelimeter);
            httpRequest.Content = new StringContent(JsonDeserializer.Serialize(card));
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new HttpRequestException(message: FailedToConnectMessage, inner: null, statusCode: response.StatusCode);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteCardAsync(int cardId)
        {
            var response = await HttpClient.SendAsync(CreateRequest(HttpMethod.Delete, Configuration[ServerUriNode] + UriPathDelimeter + cardId.ToString()));

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode is HttpStatusCode.NotFound)
            {
                return false;
            }

            throw new HttpRequestException(message: FailedToConnectMessage, inner: null, statusCode: response.StatusCode);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<HttpCard>> GetCardsAsync()
        {
            var response = await HttpClient.SendAsync(CreateRequest(HttpMethod.Get, Configuration[ServerUriNode]));
            if (response.IsSuccessStatusCode)
            {
                return JsonDeserializer.Deserialize(await response.Content.ReadAsStringAsync());
            }

            throw new HttpRequestException(message: FailedToConnectMessage, inner: null, statusCode: response.StatusCode);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateCardAsync(int cardId, HttpCard card)
        {
            HttpRequestMessage httpRequest = CreateRequest(HttpMethod.Put, Configuration[ServerUriNode] + UriPathDelimeter + cardId.ToString());
            httpRequest.Content = new StringContent(JsonDeserializer.Serialize(card));
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if(response.StatusCode is HttpStatusCode.NotFound)
            {
                return false;
            }

            throw new HttpRequestException(message: FailedToConnectMessage, inner: null, statusCode: response.StatusCode);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri)
        {
            Uri uriObject = new Uri(uri);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uriObject
            };

            return request;
        }
    }
}
