using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Repository;
using WebApi.Mapper;
using WebApi.Services;

namespace WebApi.Configs;

public static class DependencyInjection
{
    public static void AddServices(WebApplicationBuilder builder)
    {
        AddDbContext(builder);


        builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
        builder.Services.AddSingleton<IProductMapper, ProductMapper>();
        // builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
    }


    private static void AddDbContext(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Products");
        builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite(connectionString));
    }
}