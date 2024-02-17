namespace ProductService
{
    using Data;
    using Data.Contracts;
    using Messaging;
    using Messaging.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Services.Contracts;
    using Services.GrpcServices;
    using Services.GrpcServices.Contracts;
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<ProductDbContext>(opt =>
                    opt.UseSqlServer(connectionString));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IWarehouseGrpcClientService, WarehouseGrpcClientService>();
            builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
            builder.Services.AddSingleton<IMessageSenderService, MessageSenderService>();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddGrpc();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.MapGrpcService<GrpcProductService>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
