namespace CardsApiApp.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cards.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        public CardsController(ICardStorageService storageService, ILogger logger)
        {
            this.StorageService = storageService;
            this.Logger = logger;
        }

        private ICardStorageService StorageService { get; set; }

        private ILogger Logger { get; set; }

        // GET: Cards
        [HttpGet]
        public IAsyncEnumerable<Card> Get()
        {
            this.Logger.LogInformation("Get request");
            return this.StorageService.GetCardsAsync();
        }

        // GET: Cards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            this.Logger.LogInformation("Get request for" + (id is null?"null":id));

            if (id == null)
            {
                return this.NotFound();
            }

            (bool response, Card card) = await this.StorageService.TryGetCardAsync((int)id);
            if (response)
            {
                return this.Ok(card);
            }

            return this.NotFound();
        }

        // POST: Cards
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Card card)
        {
            Logger.LogInformation(string.Format("Create new card with{0} name and{1} image",
                (string.IsNullOrEmpty(card?.Name) ? " no" : ""),
                (card?.Image is null) ? " no" : ""));

            if (card is null)
            {
                return this.BadRequest();
            }

            int cardId = await this.StorageService.CreateCardAsync(card);

            return this.Ok(cardId);
        }

        // PUT Cards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Card card)
        {
            this.Logger.LogInformation("Put request for" + id);

            if (card is null)
            {
                return this.BadRequest();
            }

            if (await this.StorageService.UpdateCardAsync(id, card))
            {
                return this.Ok();
            }

            return this.NotFound();
        }

        // GET: Cards/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            this.Logger.LogInformation("Delete request for" + (id is null ? "null" : id));

            if (id == null)
            {
                return this.NotFound();
            }

            if (await this.StorageService.DeleteCardAsync((int)id))
            {
                return this.Ok();
            }

            return this.NotFound();
        }
    }
}
