using Microsoft.EntityFrameworkCore;
using e_commerce_blackcat_api.Data;
using e_commerce_blackcat_api.Repositories;
using e_commerce_blackcat_api.Interfaces;
using Microsoft.AspNetCore.Identity;
using e_commerce_blackcat_api.Src.Models;
using e_commerce_blackcat_api.Src.Services.Interface;
using e_commerce_blackcat_api.Src.Services.Implements;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMapperService, MapperService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

DbInitializer.InitDb(app);

app.MapControllers();

app.Run();
