using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaEstadoCreditos
{
    private IConexion conexion;

    public PruebaEstadoCreditos()
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

        this.conexion.EstadoCreditos!.Add(new EstadoCreditos
        {
            estado = "ESTADO TEST"
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.EstadoCreditos!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.EstadoCreditos!.FirstOrDefault(e => e.estado == "ESTADO TEST");
        if (registro != null)
        {
            registro.estado = "TEST ESTADO ACTUALIZADO";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.EstadoCreditos!.FirstOrDefault(e => e.estado == "TEST ESTADO ACTUALIZADO");
        if (registro != null)
        {
            conexion.EstadoCreditos!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}