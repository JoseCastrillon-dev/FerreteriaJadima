using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaCreditos
{
    private IConexion conexion;

    public PruebaCreditos()
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
        this.conexion.Creditos!.Add(new Creditos
        {
            idVenta = 3,
            id_estadoCredito = 1,
            fechaPrestamo = DateTime.Now,
            valorPrestamo = 300000
        });
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Creditos!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.Creditos!.FirstOrDefault(e => e.valorPrestamo == 300000);
        if (registro != null)
        {
            registro.valorPrestamo = 450000;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Creditos!.FirstOrDefault(e => e.id_estadoCredito == 1 || e.valorPrestamo == 300000);
        if (registro != null)
        {
            conexion.Creditos!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}