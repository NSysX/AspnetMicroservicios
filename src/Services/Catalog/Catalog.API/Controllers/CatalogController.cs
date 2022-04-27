using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepo productRepo, ILogger<CatalogController> logger)
        {
            this._productRepo = productRepo;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetListar()
        {
            return Ok(await this._productRepo.ListaProductos());
        }

        [HttpGet("{id:length(24)}", Name = "ProductoXId")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ProductoXId(string id)
        {
            if (id is null) return BadRequest();

            var respuesta = await this._productRepo.ProductoXId(id);
            if(respuesta is null)
            {
                this._logger.LogError($"El producto con id = {id} , no se encontro");
                return NotFound();
            }

            return Ok(await this._productRepo.ProductoXId(id));
        }

        [HttpGet("[action]/{marca}", Name = "ProductosXMarca")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ProductosXMarca(string marca)
        {
            var products = await this._productRepo.ProductoXMarca(marca);
            return Ok(products);
        }

        [HttpPost(Name = "InsertarProducto")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult> InsertarProducto([FromBody] Product product)
        {
            await this._productRepo.InsertaProducto(product);
            return CreatedAtRoute("ProductoXId", new { id = product.Id }, product);
        }

        [HttpPut(Name = "ActualizarProducto")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult> ActualizarProducto([FromBody] Product product)
        {
            return Ok(await this._productRepo.ActualizarProducto(product));
        }

        [HttpDelete("{id:length(24)}",Name = "EliminarProducto")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult> EliminarProducto(string id)
        {
            return Ok(await this._productRepo.EliminarProducto(id));
        }
    }
}
