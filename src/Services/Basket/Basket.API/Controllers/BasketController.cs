using Basket.API.Entities;
using Basket.API.GrpcServicios;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepo _repo;
        private readonly DescuentoGrpcServicios _descuentoGrpcServicios;

        public BasketController(IBasketRepo repo, DescuentoGrpcServicios descuentoGrpcServicios)
        {
            this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
            this._descuentoGrpcServicios = descuentoGrpcServicios ?? throw new ArgumentNullException(nameof(descuentoGrpcServicios));
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
            // TODO : Cominicarse con Discount Grpc y reacalcula precios  despues de los descuentos
            // consumit grpc descuento

            foreach (var item in canasta.Items)
            {
                var cupon = await this._descuentoGrpcServicios.CuponXProductName(item.ProductName);
                decimal porcentajeAValor = Math.Round((cupon.Amount / 100m) * item.Price,2);
                item.Price -= porcentajeAValor; 
            }

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
