using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly string _conn;
        private readonly IConfiguration _configuration;

        public DiscountRepo(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._conn = this._configuration.GetConnectionString("Discountdb");
        }

        public async Task<bool> ActualizaCupon(Cupon cupon)
        {
            using var cnn = new NpgsqlConnection(this._conn);
            int afecto = await cnn.ExecuteAsync("UPDATE CUPON Set ProductName = @ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id", new { cupon.ProductName, cupon.Description, cupon.Amount, cupon.Id });
            return afecto != 0;
        }

        public async Task<Cupon> CuponXProductName(string productName)
        {
            using (var cnn = new NpgsqlConnection(this._conn))
            {
                var cupon = await cnn.QueryFirstOrDefaultAsync<Cupon>("Select * From Cupon Where ProductName = @productName", new { productName });

                if (cupon != null) return cupon;
            }

            return new Cupon() { ProductName = "No Discount", Amount = 0, Description = "No Disc Descrip" };
        }

        public async Task<bool> EliminaCupon(string productName)
        {
            using var cnn = new NpgsqlConnection(this._conn);
            var afecto = await cnn.ExecuteAsync("Delete From Cupon Where productName = @productName", new { productName });
            return afecto != 0;
        }

        public async Task<bool> InsertarCupon(Cupon cupon)
        {
            using var cnn = new NpgsqlConnection(this._conn);
            var afecto = await cnn.ExecuteAsync("INSERT INTO CUPON (ProductName, Description, Amount) Values (@ProductName, @Description, @Amount)", new { cupon.ProductName, cupon.Description, cupon.Amount });

            return afecto != 0;
        }
    }
}
