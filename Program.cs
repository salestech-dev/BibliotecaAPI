using Microsoft.EntityFrameworkCore;
using Biblioteca.Db;
using Biblioteca.Interfaces;
using Biblioteca.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ✅ CORRETO: Configure os serviços ANTES de builder.Build()

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ MOVER para ANTES de builder.Build() - Isso resolve o erro!
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

// ✅ AGORA pode construir a aplicação
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