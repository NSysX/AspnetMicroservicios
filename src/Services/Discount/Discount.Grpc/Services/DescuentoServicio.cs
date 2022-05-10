using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DescuentoServicio : DescuentoProtoServicios.DescuentoProtoServiciosBase
    {
        private readonly IDiscountRepo _repo;
        private readonly ILogger<DescuentoServicio> _logger;
        private readonly IMapper _mapper;

        // exponer grpc metodos
        public DescuentoServicio(IDiscountRepo repo, ILogger<DescuentoServicio> logger, IMapper mapper)
        {
            this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper;
        }

        public override async Task<CuponModelo> ObtenerDescuento(ObtenerDescuentoPeticion request, ServerCallContext context)
        {
            var cupon = await this._repo.CuponXProductName(request.ProductName.Trim());
            if (cupon == null)
                  throw new RpcException(new Status(StatusCode.NotFound, $"No tiene Descuento el producto ${ request.ProductName.Trim() }"));

            this._logger.LogInformation($"Descuento existe para el producto {cupon.ProductName} con un total de {cupon.Amount} %");

            var cuponModelo = this._mapper.Map<CuponModelo>(cupon);
            return cuponModelo;
        }

        public override async Task<CuponModelo> InsertarDescuento(InsertarDescuentoPeticion request, ServerCallContext context)
        {
            // va a llegar como un tipo y hay que mapearlo al de tipo de la tabla
            var cupon = this._mapper.Map<Cupon>(request.Cupon);
            await this._repo.InsertarCupon(cupon);
            this._logger.LogInformation($"Cupon de Descuento creado exitosamente: { cupon.ProductName }");

            var cuponModel = this._mapper.Map<CuponModelo>(cupon);
            return cuponModel;
        }

        public override async Task<CuponModelo> ActualizarDescuento(ActualizarDescuentoPeticion request, ServerCallContext context)
        {
            var cupon = this._mapper.Map<Cupon>(request.Cupon);

            await this._repo.ActualizaCupon(cupon);
            this._logger.LogInformation($"Cupon de Descuento Actualizado : { cupon.ProductName }");

            var cuponModelo = this._mapper.Map<CuponModelo>(cupon);
            return cuponModelo;
        }

        public override async Task<EliminarDescuentoRespuesta> EliminarDescuento(EliminarDescuentoPeticion request, ServerCallContext context)
        {
            var exitoso = await this._repo.EliminaCupon(request.ProductName);

            var respuesta = new EliminarDescuentoRespuesta()
            {
                Exitoso = exitoso,
            };

            return respuesta;
        }
    }
}
