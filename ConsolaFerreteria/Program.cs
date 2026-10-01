using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

try
{
    IConexion conexion = new Conexion();
    conexion.StringConexion = DatosGenerales.StringConexion();
    var listaCargos = conexion.Cargos!.ToList();
    var listaClientes = conexion.Clientes!.ToList();
    var listaCompras = conexion.Compras!.ToList();
    var listaCreditos = conexion.Creditos!.ToList();
    var listaCuotas = conexion.Cuotas!.ToList();
    var listaDetalleCompras = conexion.DetalleCompras!.ToList();
    var listaDetalleVentas = conexion.DetalleVentas!.ToList();
    var listaDomicilios = conexion.Domicilios!.ToList();
    var listaEmpleados = conexion.Empleados!.ToList();
    var listaEntregas = conexion.Entregas!.ToList();
    var listaEstadoCreditos = conexion.EstadoCreditos!.ToList();
    var listaEstanterias = conexion.Estanterias!.ToList();
    var listaPersonas = conexion.Personas!.ToList();
    var listaProductos = conexion.Productos!.ToList();
    var listaProveedores = conexion.Proveedores!.ToList();
    var listaSalarios = conexion.Salarios!.ToList();
    var listaTipos = conexion.Tipos!.ToList();
    var listaTiposDePagos = conexion.TiposDePagos!.ToList();
    var listaVehiculos = conexion.Vehiculos!.ToList();
    var listaVentas = conexion.Ventas!.ToList();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("csl_presentacion");