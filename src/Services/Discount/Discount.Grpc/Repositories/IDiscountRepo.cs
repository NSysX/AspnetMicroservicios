using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepo
    {
        Task<Cupon> CuponXProductName(string productName);
        Task<bool> InsertarCupon(Cupon cupon);
        Task<bool> ActualizaCupon(Cupon cupon);
        Task<bool> EliminaCupon(string productName);
    }
}
