using CaféCozyApp.Data;
using CaféCozyApp.Models.Identity;
using CaféCozyApp.Services.Interfaces;
using CaféCozyApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connections"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IAboutFeatureService, AboutFeatureService>();
builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<IProductService, ProductService>();




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

app.UseSession();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
