
using LibreriaPresentacionFerreteria.Interfaces;
using LibreriaPresentacionFerreteria.implementaciones;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
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
    builder.Services.AddScoped<IAuditoriaPresentacion, AuditoriaPresentacion>();
    builder.Services.AddScoped<IUsuariosPresentacion, UsuariosPresentacion>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? "";
    if (path.StartsWith("/Ventanas", StringComparison.OrdinalIgnoreCase)
        && string.IsNullOrEmpty(context.Session.GetString("Usuario")))
    {
        context.Response.Redirect("/");
        return;
    }
    await next();
});
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.Run();
