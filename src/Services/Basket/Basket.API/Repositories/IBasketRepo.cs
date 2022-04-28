using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepo
    {
        Task<ShoppingCart> ListarCanasta(string userName);
        Task<ShoppingCart> ActualizarCanasta(ShoppingCart canasta);
        Task EliminarCanasta(string userName);
    }
}
