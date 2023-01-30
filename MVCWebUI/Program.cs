using System.Globalization;
using Business.Services;
using Business.Services.Report;
using DataAccess.Contexts;
using DataAccess.Repostitories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Localization
// Web uygulamasýnýn bölgesel ayarý aþaðýdaki þekilde tek seferde konfigüre edilerek tüm projenin bu ayarý kullanmasý saðlanabilir,
// dolayýsýyla veri formatlama veya dönüþtürme gibi iþlemlerde her seferinde CultureInfo objesinin kullaným gereksinimi ortadan kalkar.
// Bu þekilde sadece tek bir bölgesel ayar projede kullanýlabilir.
List<CultureInfo> cultures = new List<CultureInfo>()
{
	new CultureInfo("en-US") // eðer uygulama Türkçe olacaksa CultureInfo constructor'ýnýn parametresini ("tr-TR") yapmak yeterlidir.
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
	options.SupportedCultures = cultures;
	options.SupportedUICultures = cultures;
});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(config =>
	{
		config.LoginPath = "/Account/Users/Login";
		config.AccessDeniedPath = "/Account/Users/AccessDenied";
		config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
		config.SlidingExpiration = true;
	});
#endregion

#region Session
builder.Services.AddSession(config =>
{
	config.IdleTimeout = TimeSpan.FromMinutes(40); // default: 20 dakika
	config.IOTimeout = Timeout.InfiniteTimeSpan;
});
#endregion



#region IoC Container (Inversion of Control)
// Autofac, Ninject
var connectionString = builder.Configuration.GetConnectionString("ETradeDb");
builder.Services.AddDbContext<ETradeContext>(options => options.UseSqlServer(connectionString));

// builder.Services.AddTransient<ProductRepoBase, ProductRepo>();   her enjeksiyonda yeni obje oluşturur
// builder.Services.AddSingleton<ProductRepoBase, ProductRepo>();   statik obje kullanmanı sağlar
builder.Services.AddScoped<ProductRepoBase, ProductRepo>(); // önemli
builder.Services.AddScoped<CategoryRepoBase, CategoryRepo>();
builder.Services.AddScoped<StoreRepoBase, StoreRepo>();

builder.Services.AddScoped<UserRepoBase, UserRepo>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStoreService, StoreService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IReportService, ReportService>();

#endregion

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
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot altındaki dosyaları kullanmayı sağlar

app.UseRouting();

#region  Authentication
app.UseAuthentication();
#endregion

app.UseAuthorization(); // yetki kontrolü


#region Session
app.UseSession();
#endregion


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
