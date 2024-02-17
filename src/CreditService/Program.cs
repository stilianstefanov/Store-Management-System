namespace CreditService
{
    using Data;
    using Data.Repositories;
    using Data.Repositories.Contracts;
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
                .AddDbContext<CreditDbContext>(opt =>
                    opt.UseSqlServer(connectionString));

            builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
            builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            builder.Services.AddScoped<IPurchasedProductRepository, PurchasedProductRepository>();
            builder.Services.AddScoped<IBorrowerService, BorrowerService>();
            builder.Services.AddScoped<IPurchaseService, PurchaseService>();
            builder.Services.AddScoped<IPurchasedProductService, PurchasedProductService>();
            builder.Services.AddScoped<IProductGrpcClientService, ProductGrpcClientService>();

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
