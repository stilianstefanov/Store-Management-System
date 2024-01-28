
namespace WarehouseService
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Services.Contracts;
    using Data.Repositories.Contracts;
    using Data.Repositories;
    using Messaging;
    using Messaging.Contracts;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<WarehousesDbContext>(opt => 
                    opt.UseSqlServer(connectionString));

            builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddScoped<IWarehouseService, WarehouseService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            builder.Services.AddHostedService<MessageBusSubscriber>();
         
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
