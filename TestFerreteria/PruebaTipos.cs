using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaTipos
{
    private IConexion conexion;

    public PruebaTipos()
    {
        this.conexion = new Conexion();
        conexion.StringConexion = DatosGenerales.StringConexion();

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

        this.conexion.Tipos!.Add(new Tipos
        {
            nombre = "NOMBRE TEST"
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Tipos!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Tipos!.FirstOrDefault(e => e.nombre == "NOMBRE TEST");
        if (registro != null)
        {
            registro.nombre = "NOMBRE TEST ACTUALIZADO";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Tipos!.FirstOrDefault(e => e.nombre == "NOMBRE TEST ACTUALIZADO");
        if (registro != null)
        {
            conexion.Tipos!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}