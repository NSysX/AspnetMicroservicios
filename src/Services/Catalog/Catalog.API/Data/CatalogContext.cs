using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        // es el contexto de MONGODB

        public CatalogContext(IConfiguration configuration)
        {
            string connString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            // Creamos el cliente
            var cliente = new MongoClient(connString);
            // obtenemos la base de datos
            string nombreBD = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var basedatos = cliente.GetDatabase(nombreBD);
            // crear registros en la coleccion de productos
            string nombreColeccion = configuration.GetValue<string>("DatabaseSettings:CollectionName");
            Products = basedatos.GetCollection<Product>(nombreColeccion);
            // Seeding 
           // CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
