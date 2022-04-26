using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string CodigoBase { get; set; } = string.Empty;
        public string DescripcionCorta { get; set; } = string.Empty;
        [BsonElement("Descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string CodigoBarras { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
