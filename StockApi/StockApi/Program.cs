using Microsoft.EntityFrameworkCore;
using Stock.DataAccess.Application;
using Stock.Repository.Common;
using Stock.RepositoryContract.Common;
using Stock.Service.Common;
using Stock.ServiceContract.Common;
using StockApi.Infrastructures.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddDbContext<StockContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IItemLocationService, ItemLocationService>();
builder.Services.AddScoped<ISupplierItemService, SupplierItemService>();

builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<ILocationRepository, LocationRepository>();
builder.Services.AddTransient<ISupplierItemRepository, SupplierItemRepository>();
builder.Services.AddTransient<IItemLocationRepository, ItemLocationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
