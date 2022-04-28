using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepo _repo;

        public BasketController(IBasketRepo repo)
        {
            this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet("{userName}",Name = "ListarCanasta")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult> ListarCanasta(string userName)
        {
            return Ok(await this._repo.ListarCanasta(userName) ?? new ShoppingCart(userName));
        }

        [HttpPost(Name = "Actualiza Canasta")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult> ActualizaCanasta([FromBody] ShoppingCart canasta)
        {
            return Ok(await this._repo.ActualizarCanasta(canasta));
        }

        [HttpDelete("{userName}", Name = "EliminarCanasta")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> EliminarCanasta(string userName)
        {
            await this._repo.EliminarCanasta(userName);
            return Ok();
        }
    }
}
