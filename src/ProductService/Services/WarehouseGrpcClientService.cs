namespace ProductService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.ViewModels;
    using Grpc.Core;
    using Grpc.Net.Client;
    using WarehouseService;

    public class WarehouseGrpcClientService : IWarehouseGrpcClientService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public WarehouseGrpcClientService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper; 
        }

        public WarehouseViewModel? GetWarehouseById(string id)
        {
            var channel = GrpcChannel.ForAddress(_configuration["GrpcWarehouseService"]!);

            var client = new WarehouseServiceGrpc.WarehouseServiceGrpcClient(channel);

            var request = new GetWarehouseByIdRequest
            {
                Id = id
            };

            try
            {
                var response = client.GetWarehouseById(request);

                return _mapper.Map<WarehouseViewModel>(response.Warehouse);
            }
            catch (RpcException e)
            {
                return null;
            }
        }
    }
}
