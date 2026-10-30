using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaTiposDePagos
{
    private IConexion conexion;

    public PruebaTiposDePagos()
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

        this.conexion.TiposDePagos!.Add(new TiposDePagos
        {
            metodo = "METODO TEST"
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.TiposDePagos!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.TiposDePagos!.FirstOrDefault(e => e.metodo == "METODO TEST");
        if (registro != null)
        {
            registro.metodo = "METODO TEST ACTUALIZADO";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.TiposDePagos!.FirstOrDefault(e => e.metodo == "METODO TEST ACTUALIZADO");
        if (registro != null)
        {
            conexion.TiposDePagos!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}