using BulkyBook.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// here you can register example: Mail or DB
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Making connection with the connection string inside the appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
   builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// you have singleton, scope and transient
// In this case we will use scope => everytime you hit a button and make a request to the db => this will be the scope of that abject
// this will give of the implementation of the Icategory repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AddRazorRuntimeCompilation(); you need to add if you are not working with .net6, because of Hot reload it's not required anymore!
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
