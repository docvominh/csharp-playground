using CqrsWithEfCoreAndMediaR.Data;
using CqrsWithEfCoreAndMediaR.Data.Entity;
using CqrsWithEfCoreAndMediaR.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    o => o.UseSqlite("Data Source=Database.db")
);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var context = DbContextFactory.CreateDbContext();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();

context.Products.AddRange(new List<Product>
{
    new()
    {
        Id = 1,
        Category = "Mobile",
        Manufacture = "Apple",
        Name = "IphoneX",
        Price = 800
    },
    new()
    {
        Id = 2,
        Category = "Mobile",
        Manufacture = "Google",
        Name = "Pixel 10",
        Price = 800
    }
});

context.SaveChanges();

app.MapGet("/products", (ProductService service) => service.GetAllProduct());
app.MapPost("/products", (Product product, ProductService service) => service.CreateProduct(product));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
