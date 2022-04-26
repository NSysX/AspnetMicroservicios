using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Seeding
{
    public class ProductsSeed
    {
        public static void Seed(IMongoCollection<Product> productCollection)
        {
            bool extisteProducto = productCollection.Find(p => true).Any();
            if (!extisteProducto)
            {
                productCollection.InsertManyAsync(ProductosPreCapturados());
            }
        }

        private static IEnumerable<Product> ProductosPreCapturados()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    CodigoBase = "ZS-052",
                    DescripcionCorta = "BOBINA",
                    Descripcion = "BOBINA DE ENCENDIDO | NISSAN PLATINA 00-07 APRIO 4CIL 1.6",
                    Marca = "ROADSTAR",
                    CodigoBarras = "12315648",
                    Costo = 58.90m,
                    Precio = 100.00m,
                    ImagePath = "ZS-052.png"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    CodigoBase = "YS4Z-3A-130B",
                    DescripcionCorta = "TERMINAL DER",
                    Descripcion = "TERMINAL DERECHA | FORD FOCUS 99-04",
                    Marca = "TOTALSOLUCION",
                    CodigoBarras = "NA",
                    Costo = 149.9600m,
                    Precio = 225.0000m,
                    ImagePath = "YS4Z-3A-130B.png"
                }
            };
        }
    }
}
