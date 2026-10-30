using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaCompras
{
    private IConexion conexion;

    public PruebaCompras()
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
        this.conexion.Compras!.Add(new Compras
        {
            id_proveedor = 6,
            id_empleado = 1,
            fechaCompra = DateTime.Now,
            total = 500000
        });
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Compras!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.Compras!.FirstOrDefault(e => e.total == 500000);
        if (registro != null)
        {
            registro.total = 750000;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Compras!.FirstOrDefault(e => e.id_empleado == 1 || e.total == 750000);
        if (registro != null)
        {
            conexion.Compras!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}