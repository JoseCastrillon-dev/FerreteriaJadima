using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Implementaciones;
using LibreriaAplicacionFerreteria.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IConexion, Conexion>();

builder.Services.AddScoped<IPersonasAplicacion, PersonasAplicacion>();
builder.Services.AddScoped<IEstadoCreditosAplicacion, EstadoCreditosAplicacion>();
builder.Services.AddScoped<ICargosAplicacion, CargosAplicacion>();
builder.Services.AddScoped<ITiposDePagosAplicacion, TiposDePagosAplicacion>();
builder.Services.AddScoped<ITiposAplicacion, TiposAplicacion>();
builder.Services.AddScoped<IVehiculosAplicacion, VehiculosAplicacion>();
builder.Services.AddScoped<IProveedoresAplicacion, ProveedoresAplicacion>();
builder.Services.AddScoped<IClientesAplicacion, ClientesAplicacion>();
builder.Services.AddScoped<ISalariosAplicacion, SalariosAplicacion>();
builder.Services.AddScoped<IProductosAplicacion, ProductosAplicacion>();
builder.Services.AddScoped<IDetalleVentasAplicacion, DetalleVentasAplicacion>();
builder.Services.AddScoped<IEmpleadosAplicacion, EmpleadosAplicacion>();
builder.Services.AddScoped<IEstanteriasAplicacion, EstanteriasAplicacion>();
builder.Services.AddScoped<IComprasAplicacion, ComprasAplicacion>();
builder.Services.AddScoped<IDetalleComprasAplicacion, DetalleComprasAplicacion>();
builder.Services.AddScoped<IVentasAplicacion, VentasAplicacion>();
builder.Services.AddScoped<ICreditosAplicacion, CreditosAplicacion>();
builder.Services.AddScoped<ICuotasAplicacion, CuotasAplicacion>();
builder.Services.AddScoped<IDomiciliosAplicacion, DomiciliosAplicacion>();
builder.Services.AddScoped<IEntregasAplicacion, EntregasAplicacion>();









var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
