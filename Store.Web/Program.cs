

using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.Data.Contexts;
using Store.Web.Extensions;
using Store.Web.Helper;
using Store.Web.Middleware;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
             builder.Services.AddDbContext<StoreDbContext>(options =>
             {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
             }); 
             builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
             {
                 options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
             });
 
             builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
             {
                var configurations = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(configurations);
             });

            builder.Services.AddApplicationServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();

            await ApplySeeding.ApplySeedingAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

           
            app.MapControllers();
   
            app.Run();
        }
    }
}
