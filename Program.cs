using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using trabajo_final_grupo_verde.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using trabajo_final_grupo_verde.Models;
using Microsoft.OpenApi.Models;
using trabajo_final_grupo_verde.Service;
using trabajo_final_grupo_verde.Integration.currencyexchange;
using AnalisisSentimental;
using Microsoft.Extensions.ML;
using trabajo_final_grupo_verde.Controllers;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddPredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput>()
    .FromFile("MLModel1.mlnet");

// Add services to the container.
/*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
*/
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Registro mi logica customizada y reuzable
builder.Services.AddScoped<ProductoService, ProductoService>();
builder.Services.AddScoped<PedidoService, PedidoService>();
builder.Services.AddScoped<PagoService, PagoService>();
builder.Services.AddScoped<MLController, MLController>();

builder.Services.AddScoped<CurrencyExchangeApiIntegration, CurrencyExchangeApiIntegration>();

// Aquí es donde debes hacer el cambio, usa builder.Configuration en lugar de Configuration
builder.Services.AddTransient<IMyEmailSender, EmailSender>(i =>
        new EmailSender(
            builder.Configuration["Email:SmtpServer"],
            int.Parse(builder.Configuration["Email:SmtpPort"]),
            builder.Configuration["Email:SmtpUsername"],
            builder.Configuration["Email:SmtpPassword"]
        )
    );

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1500);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1",
        Description = "Descripción de la API"
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapPost("/predict",
    async (PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool, MLModel1.ModelInput input) =>
        await Task.FromResult(predictionEnginePool.Predict(input)));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
