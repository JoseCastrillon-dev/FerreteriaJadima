using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaVentas
{
    private IConexion conexion;

    public PruebaVentas()
    {
        this.conexion = new Conexion();
        conexion.StringConexion = DatosGenerales.StringConexion();
    }

    [TestMethod]
    public void Ejecutar()
    {
        try
        {
            Insertar();
            Consultar();
            Actualizar();
        }
        finally
        {
            Borrar();
        }
    }

    public void Insertar()
    {
        this.conexion.Ventas!.Add(new Ventas
        {
            id_detalleVenta = 1,
            id_tipoDePago = 1,
            id_empleado = 1,
            id_cliente = 1,
            iva = 4,
            total = 119000
        });
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Ventas!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.Ventas!.FirstOrDefault(e => e.total == 119000);
        if (registro != null)
        {
            registro.total = 238000;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Ventas!.FirstOrDefault(e => e.id_cliente == 1 || e.total == 238000);
        if (registro != null)
        {
            conexion.Ventas!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}