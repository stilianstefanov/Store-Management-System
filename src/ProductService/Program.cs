namespace ProductService
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Data;
    using Data.Contracts;
    using Messaging;
    using Messaging.Contracts;
    using Services;
    using Services.Contracts;
    using Services.GrpcServices;
    using Services.GrpcServices.Contracts;
    using Utilities.Middleware;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<ProductDbContext>(opt =>
                    opt.UseSqlServer(connectionString));


            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowClient",
                    b => b
                        .WithOrigins(builder.Configuration["ClientUrl"]!)
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

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
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.MapGrpcService<GrpcProductService>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowClient");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
