using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repostitories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region IoC Container (Inversion of Control)
// Autofac, Ninject
var connectionString = builder.Configuration.GetConnectionString("ETradeDb");
builder.Services.AddDbContext<ETradeContext>(options => options.UseSqlServer(connectionString));

// builder.Services.AddTransient<ProductRepoBase, ProductRepo>();   her enjeksiyonda yeni obje olu�turur
// builder.Services.AddSingleton<ProductRepoBase, ProductRepo>();   statik obje kullanman� sa�lar
builder.Services.AddScoped<ProductRepoBase, ProductRepo>(); // �nemli
builder.Services.AddScoped<CategoryRepoBase, CategoryRepo>(); 

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot alt�ndaki dosyalar� kullanmay� sa�lar

app.UseRouting(); 

app.UseAuthorization(); // yetki kontrol�

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
