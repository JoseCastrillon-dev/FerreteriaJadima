using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaCargos
{
    private IConexion conexion;

    private string nombreTest = "NOMBRE TEST_" + DateTime.Now.Ticks;
    private string nombreActualizado;

    public PruebaCargos()
    {
        this.conexion = new Conexion();
        conexion.StringConexion = DatosGenerales.StringConexion();
        this.nombreActualizado = nombreTest + "_ACT";
    }

    [TestMethod]
    public void Ejecutar()
    {
        Insertar();
        Consultar();
        Actualizar();
        Borrar();
    }

    public void Insertar()
    {

        this.conexion.Cargos!.Add(new Cargos
        {
            nombre = nombreTest
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Cargos!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Cargos!.FirstOrDefault(e => e.nombre == nombreTest);
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
        var registro = conexion.Cargos!.FirstOrDefault(e => e.nombre == nombreActualizado);
        if (registro != null)
        {
            conexion.Cargos!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}