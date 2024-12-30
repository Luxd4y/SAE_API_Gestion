using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.DataManager;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GestionDBContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("SaeGestionRemoteDev")));
builder.Services.AddScoped<IDataRepository<Batiment>, BatimentManager>();
builder.Services.AddScoped<IDataRepository<Capteur>, CapteurManager>();
builder.Services.AddScoped<IDataRepository<CapteurInstalle>, CapteurInstalleManager>();
builder.Services.AddScoped<IDataRepository<Equipement>, EquipementManager>();

builder.Services.AddScoped<IDataRepository<EquipementInstalle>, EquipementInstalleManager>();
builder.Services.AddScoped<IDataRepository<MarqueCapteur>, MarqueCapteurManager>();
builder.Services.AddScoped<IDataRepository<ParametreCapteur>, ParametreCapteurManager>();
builder.Services.AddScoped<IDataRepositoryParametreCapteur, ParametreCapteurManager>();

builder.Services.AddScoped<IDataRepository<PositionSurface>, PositionSurfaceManager>();
builder.Services.AddScoped<IDataRepository<Salle>, SalleManager>();

builder.Services.AddScoped<IDataRepositorySalle, SalleManager>();
builder.Services.AddScoped<IDataRepository<Surface>, SurfaceManager>();
builder.Services.AddScoped<IDataRepository<TypeEquipement>, TypeEquipementManager>();
builder.Services.AddScoped<IDataRepository<TypeSalle>, TypeSalleManager>();
builder.Services.AddScoped<IDataRepository<UniteMesurer>, UniteMesurerManager>();











var app = builder.Build();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());





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
