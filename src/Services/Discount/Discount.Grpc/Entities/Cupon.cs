namespace Discount.Grpc.Entities
{
    public class Cupon
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}
