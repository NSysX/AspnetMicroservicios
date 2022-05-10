using Discount.Grpc.Protos;

namespace Basket.API.GrpcServicios
{
    public class DescuentoGrpcServicios
    {
        private readonly DescuentoProtoServicios.DescuentoProtoServiciosClient _discountProto;

        public DescuentoGrpcServicios(DescuentoProtoServicios.DescuentoProtoServiciosClient discountProto)
        {
            this._discountProto = discountProto ?? throw new ArgumentNullException(nameof(discountProto));
        }

        public async Task<CuponModelo> CuponXProductName(string productName)
        {
            var descuentoPeticion = new ObtenerDescuentoPeticion() { ProductName = productName };
            return await this._discountProto.ObtenerDescuentoAsync(descuentoPeticion);
        }
    }
}
