using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaDetalleVentas
{
    private IConexion conexion;



    public PruebaDetalleVentas()
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

        this.conexion.DetalleVentas!.Add(new DetalleVentas
        {
            id_producto = 1,
            cantidad = 1000,
            iva = 2,
            total = 2000
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.DetalleVentas!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.DetalleVentas!.FirstOrDefault(e => e.cantidad == 1000);
        if (registro != null)
        {
            registro.cantidad = 3000;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.DetalleVentas!.FirstOrDefault(e => e.id_producto == 1 && e.cantidad == 3000);
        if (registro != null)
        {
            conexion.DetalleVentas!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}