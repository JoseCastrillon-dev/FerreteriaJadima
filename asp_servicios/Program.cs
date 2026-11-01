using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IConexion, Conexion>();

builder.Services.AddScoped<IPersonasAplicacion, PersonasAplicacion>();
builder.Services.AddScoped<IEstadoCreditosAplicacion, EstadoCreditosAplicacion>();
builder.Services.AddScoped<ICargosAplicacion, CargosAplicacion>();
builder.Services.AddScoped<ITiposDePagosAplicacion, TiposDePagosAplicacioncs>();
builder.Services.AddScoped<ITiposAplicacion, TiposAplicacion>();
builder.Services.AddScoped<IVehiculosAplicacion, VehiculosAplicacion>();
builder.Services.AddScoped<IProveedoresAplicacion, ProveedoresAplicacion>();
builder.Services.AddScoped<IClientesAplicacion, ClientesAplicacion>();




var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
