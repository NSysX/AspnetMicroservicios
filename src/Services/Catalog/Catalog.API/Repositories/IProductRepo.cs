using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> ListaProductos();
        Task<Product> ProductoXId(string id);
        Task<IEnumerable<Product>> ProductoXDescripcion(string descripcion);
        Task<IEnumerable<Product>> ProductoXMarca(string marca);
        Task InsertaProducto(Product product);
        Task<bool> ActualizarProducto(Product product);
        Task<bool> EliminarProducto(string id);
    }
}
