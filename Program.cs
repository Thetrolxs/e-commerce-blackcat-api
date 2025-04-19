using Microsoft.EntityFrameworkCore;
using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Repositories;
using e_commerce_blackcat_api.Interfaces;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

DbInitializer.InitDb(app);

app.MapControllers();

app.Run();
