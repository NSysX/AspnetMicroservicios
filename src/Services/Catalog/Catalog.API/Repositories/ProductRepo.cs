using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly ICatalogContext _context;

        public ProductRepo(ICatalogContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> ActualizarProducto(Product product)
        {
            var resultado = await this._context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
            return (resultado.IsAcknowledged && resultado.ModifiedCount > 0);
        }

        public async Task<bool> EliminarProducto(string id)
        {
            var filtro = Builders<Product>.Filter.Eq(x => x.Id, id);
            var resultado = await this._context.Products.DeleteOneAsync(filtro);
            return resultado.IsAcknowledged && resultado.DeletedCount > 0;
        }

        public async Task InsertaProducto(Product product)
        {
            await this._context.Products.InsertOneAsync(product);
        }

        public async Task<IEnumerable<Product>> ListaProductos()
        {
            var resultado = await this._context.Products.Find(r => true).ToListAsync();
            if (resultado is null) throw new ArgumentNullException(nameof(ListaProductos));
            return resultado;
        }

        public async Task<Product> ProductoXId(string id)
        {
            var resultado = await this._context.Products.Find(r => r.Id == id).FirstOrDefaultAsync();
            if (resultado is null) throw new ArgumentNullException(nameof(ProductoXId));
            return resultado;
        }

        public async Task<IEnumerable<Product>> ProductoXMarca(string marca)
        {
            var filtro = Builders<Product>.Filter.Eq(x => x.Marca, marca);
            var resultado = await this._context.Products.Find(filtro).ToListAsync();
            if (resultado is null) throw new ArgumentNullException(nameof(ProductoXMarca));
            return resultado;
        }

        public async Task<IEnumerable<Product>> ProductoXDescripcion(string descripcion)
        {
            var filtro = Builders<Product>.Filter.Eq(x => x.Descripcion, descripcion);

            var resultado = await this._context.Products.Find(filtro).ToListAsync();
            if (resultado is null) throw new ArgumentNullException(nameof(ProductoXDescripcion));
            return resultado;
        }
    }
}
