
namespace WarehouseService
{
    using Data;
    using Data.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Services.Contracts;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddDbContext<WarehousesDbContext>(opt => opt.UseInMemoryDatabase("TestDb"));

            builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            builder.Services.AddScoped<IWarehouseService, WarehouseService>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
         
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
