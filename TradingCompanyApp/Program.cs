using AutoMapper;
using BLL.Concrete;
using BLL.Interfaces;
using DAL;
using DAL.Concrete;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using TradingCompanyApp.App.Profiles;

var builder = WebApplication.CreateBuilder(args);

MapperConfiguration config = new MapperConfiguration(
    c =>
    {
        c.AddMaps(typeof(SoldGoodsDAL).Assembly, typeof(SoldGoodsProfile).Assembly);
    });

var connectionString = builder.Configuration.GetConnectionString("TradingCompany") ?? throw new InvalidOperationException("Invalid connection string!");
builder.Services
    .AddSingleton<IMapper>(config.CreateMapper())
    .AddScoped<ISoldGoodsDAL>(provider => new SoldGoodsDAL(connectionString))
    .AddScoped<IUserDAL>(provider => new UserDAL(connectionString))
    .AddScoped<ISoldGoodsServices, SoldGoodsServices>()
    .AddScoped<IUserServices, UserServices>();
// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
