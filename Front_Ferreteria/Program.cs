
using LibreriaPresentacionFerreteria.Interfaces;
using LibreriaPresentacionFerreteria.implementaciones;
using LibreriaPresentacionFerreteria.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
    builder.Services.AddScoped<IComunicaciones, Comunicaciones>();

    builder.Services.AddScoped<IPersonasPresentacion, PersonasPresentacion>();
    builder.Services.AddScoped<IEstadoCreditosPresentacion, EstadoCreditosPresentacion>();
    builder.Services.AddScoped<ICargosPresentacion, CargosPresentacion>();
    builder.Services.AddScoped<ITiposDePagosPresentacion, TiposDePagosPresentacion>();
    builder.Services.AddScoped<ITiposPresentacion, TiposPresentacion>();
    builder.Services.AddScoped<IVehiculosPresentacion, VehiculosPresentacion>();
    builder.Services.AddScoped<IProveedoresPresentacion, ProveedoresPresentacion>();
    builder.Services.AddScoped<IClientesPresentacion, ClientesPresentacion>();
    builder.Services.AddScoped<ISalariosPresentacion, SalariosPresentacion>();
    builder.Services.AddScoped<IProductosPresentacion, ProductosPresentacion>();
    builder.Services.AddScoped<IDetalleVentasPresentacion, DetalleVentasPresentacion>();
    builder.Services.AddScoped<IEmpleadosPresentacion, EmpleadosPresentacion>();
    builder.Services.AddScoped<IEstanteriasPresentacion, EstanteriasPresentacion>();
    builder.Services.AddScoped<IComprasPresentacion, ComprasPresentacion>();
    builder.Services.AddScoped<IDetalleComprasPresentacion, DetalleComprasPresentacion>();
    builder.Services.AddScoped<IVentasPresentacion, VentasPresentacion>();
    builder.Services.AddScoped<ICreditosPresentacion, CreditosPresentacion>();
    builder.Services.AddScoped<ICuotasPresentacion, CuotasPresentacion>();
    builder.Services.AddScoped<IDomiciliosPresentacion, DomiciliosPresentacion>();
    builder.Services.AddScoped<IEntregasPresentacion, EntregasPresentacion>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
