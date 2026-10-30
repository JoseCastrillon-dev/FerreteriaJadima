using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaProductos
{
    private IConexion conexion;
    private string nombreTest = "NOMBRE TEST_" + DateTime.Now.Ticks;
    private string nombreActualizado;


    public PruebaProductos()
    {
        this.conexion = new Conexion();
        conexion.StringConexion = DatosGenerales.StringConexion();

        this.nombreActualizado = nombreTest + "_ACT";

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

        this.conexion.Productos!.Add(new Productos
        {
            id_tipo = 1,
            codigo = "123",
            cantidad = 1000,
            nombre = nombreTest,
            valor = 2000
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Productos!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Productos!.FirstOrDefault(e => e.nombre == nombreTest);
        if (registro != null)
        {
            registro.nombre = nombreActualizado;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Productos!.FirstOrDefault(e => e.id_tipo == 1 && e.nombre == nombreActualizado);
        if (registro != null)
        {
            conexion.Productos!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}