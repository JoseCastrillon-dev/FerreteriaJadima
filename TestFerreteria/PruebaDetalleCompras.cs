using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaDetalleCompras
{
    private IConexion conexion;

    public PruebaDetalleCompras()
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
        this.conexion.DetalleCompras!.Add(new DetalleCompras
        {
            id_compra = 4,
            id_producto = 1,
            cantidad = 10,
            preciosCompra = 15000
        });
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.DetalleCompras!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.DetalleCompras!.FirstOrDefault(e => e.cantidad == 10);
        if (registro != null)
        {
            registro.cantidad = 20;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.DetalleCompras!.FirstOrDefault(e => e.id_compra == 4 || e.cantidad == 20);
        if (registro != null)
        {
            conexion.DetalleCompras!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}