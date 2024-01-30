namespace WarehouseService.Services
{
    using AutoMapper;
    using Data.Repositories.Contracts;
    using Grpc.Core;

    public class GrpcWarehouseService : WarehouseServiceGrpc.WarehouseServiceGrpcBase
    {
        private readonly IWarehouseRepository _repository;
        private readonly IMapper _mapper;

        public GrpcWarehouseService(IWarehouseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<GetWarehouseByIdResponse> GetWarehouseById(GetWarehouseByIdRequest request, ServerCallContext context)
        {
            try
            {
                var warehouse = await _repository.GetByIdAsync(request.Id);

                var response = new GetWarehouseByIdResponse
                {
                    Warehouse = _mapper.Map<GrpcWarehouseModel>(warehouse)
                };

                return response;
            }
            catch (InvalidOperationException ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
