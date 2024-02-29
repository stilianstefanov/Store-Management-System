namespace WarehouseService.Services.GrpcServices
{
    using Grpc.Core;
    using AutoMapper;
    using Data.Repositories.Contracts;
    
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
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<WarehouseExistsByIdResponse> WarehouseExistsById(WarehouseExistsByIdRequest request, ServerCallContext context)
        {
            try
            {
                var exists = await _repository.ExistsByIdAsync(request.Id);

                var response = new WarehouseExistsByIdResponse
                {
                    Exists = exists
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
}
