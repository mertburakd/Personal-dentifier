using Microsoft.AspNetCore.Identity;
using WEBUI.Entities.Valiadations;
using WEBUI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();


ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyData")));
builder.Services.AddSingleton<IPersonalService, PersonalManager>();
builder.Services.AddSingleton<IPersonalDal, EfPersonalDal>();
builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>(_ =>
{
    _.Password.RequiredLength = 5; //En az ka� karakterli olmas� gerekti�ini belirtiyoruz.
    _.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunlulu�unu kald�r�yoruz.
    _.Password.RequireLowercase = false; //K���k harf zorunlulu�unu kald�r�yoruz.
    _.Password.RequireUppercase = false; //B�y�k harf zorunlulu�unu kald�r�yoruz.
    _.Password.RequireDigit = false; //0-9 aras� say�sal karakter zorunlulu�unu kald�r�yoruz.
    _.User.RequireUniqueEmail = true; //Email adreslerini tekille�tiriyoruz.
    _.User.AllowedUserNameCharacters = "abc�defghi�jklmno�pqrs�tu�vwxyzABC�DEFGHI�JKLMNO�PQRS�TU�VWXYZ0123456789-._@+"; //Kullan�c� ad�nda ge�erli olan karakterleri belirtiyoruz.
}).AddPasswordValidator<CustomPasswordValidation>()
     .AddUserValidator<CustomUserValidation>()
     .AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<CustomIdentityDbContext>()
     .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(_ =>
{
    _.LoginPath = new PathString("/Account/Login");
    _.LogoutPath = new PathString("/Account/LogOff");
    _.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityExampleCookie", //Olu�turulacak Cookie'yi isimlendiriyoruz.
        HttpOnly = false, //K�t� niyetli insanlar�n client-side taraf�ndan Cookie'ye eri�mesini engelliyoruz.
        //Expiration = TimeSpan.FromMinutes(20), //Olu�turulacak Cookie'nin vadesini belirliyoruz.
        SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin g�nderilmemesini belirtiyoruz.
        SecurePolicy = CookieSecurePolicy.Always //HTTPS �zerinden eri�ilebilir yap�yoruz.
    };
    _.SlidingExpiration = true; //Expiration s�resinin yar�s� kadar s�re zarf�nda istekte bulunulursa e�er geri kalan yar�s�n� tekrar s�f�rlayarak ilk ayarlanan s�reyi tazeleyecektir.
    _.ExpireTimeSpan = TimeSpan.FromMinutes(20); //CookieBuilder nesnesinde tan�mlanan Expiration de�erinin varsay�lan de�erlerle ezilme ihtimaline kar��n tekrardan Cookie vadesi burada da belirtiliyor.
    _.AccessDeniedPath = new PathString("/Account/Login");
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PersonalIdentity", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseStatusCodePages();
app.UseStatusCodePagesWithReExecute("/Home/ErrorPage", "?code={0}");
app.UseAuthorization();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseSwagger();

app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
    option.RoutePrefix = "swagger";
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
