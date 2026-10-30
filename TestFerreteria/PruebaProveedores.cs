using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaProveedores
{
    private IConexion conexion;

    public PruebaProveedores()
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
        this.conexion.Proveedores!.Add(new Proveedores
        {
            nombre = "nomProTest",
            numeroDeTelefono = "123",
            direccion = "dirProTest",
            email = "emailpro@correo"
        });

        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Proveedores!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.Proveedores!.FirstOrDefault(e => e.nombre == "nomProTest");
        if (registro != null)
        {
            registro.numeroDeTelefono = "321";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Proveedores!.FirstOrDefault(e => e.nombre == "nomProTest");
        if (registro != null)
        {
            conexion.Proveedores!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}