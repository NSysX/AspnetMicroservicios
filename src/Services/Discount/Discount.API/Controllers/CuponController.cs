using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly IDiscountRepo _discountRepo;

        public CuponController(IDiscountRepo discountRepo)
        {
            this._discountRepo = discountRepo ?? throw new ArgumentNullException(nameof(discountRepo));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Cupon), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetDiscount([BindRequired] string productName)
        {
            return Ok(await this._discountRepo.CuponXProductName(productName));
        }

        [HttpPost(Name = "InsertarCupon")]
        [ProducesResponseType(typeof(Cupon), StatusCodes.Status200OK)]
        public async Task<ActionResult> InsertarCupon([FromBody] [BindRequired] Cupon cupon)
        {
            await this._discountRepo.InsertarCupon(cupon);
            return CreatedAtRoute("GetDiscount", new { cupon.ProductName }, cupon);
        }

        [HttpPut(Name = "ActualizarCupon")]
        [ProducesResponseType(typeof(Cupon), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Cupon), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ActualizarCupon([FromBody] [BindRequired] Cupon cupon)
        {
            return Ok(await this._discountRepo.ActualizaCupon(cupon));
        }

        [HttpDelete("{productName}")]
        [ProducesResponseType(typeof(Cupon), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Cupon), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string productName)
        {
            return Ok(await this._discountRepo.EliminaCupon(productName));
        }
    }
}
