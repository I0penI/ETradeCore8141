using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repostitories;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Localization
// Web uygulamas?n?n bölgesel ayar? a?a??daki ?ekilde tek seferde konfigüre edilerek tüm projenin bu ayar? kullanmas? sa?lanabilir,
// dolay?s?yla veri formatlama veya dönü?türme gibi i?lemlerde her seferinde CultureInfo objesinin kullan?m gereksinimi ortadan kalkar.
// Bu ?ekilde sadece tek bir bölgesel ayar projede kullan?labilir.
List<CultureInfo> cultures = new List<CultureInfo>()
{
	new CultureInfo("en-US") // e?er uygulama Türkçe olacaksa CultureInfo constructor'?n?n parametresini ("tr-TR") yapmak yeterlidir.
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
	options.SupportedCultures = cultures;
	options.SupportedUICultures = cultures;
});
#endregion

// Add services to the container.
#region IoC Container (Inversion of Control)
// Autofac, Ninject
var connectionString = builder.Configuration.GetConnectionString("ETradeDb");
builder.Services.AddDbContext<ETradeContext>(options => options.UseSqlServer(connectionString));

// builder.Services.AddTransient<ProductRepoBase, ProductRepo>();   her enjeksiyonda yeni obje oluþturur
// builder.Services.AddSingleton<ProductRepoBase, ProductRepo>();   statik obje kullanmaný saðlar
builder.Services.AddScoped<ProductRepoBase, ProductRepo>(); // önemli
builder.Services.AddScoped<CategoryRepoBase, CategoryRepo>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
	DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
	SupportedCultures = cultures,
	SupportedUICultures = cultures,
});
#endregion


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
