using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaCuotas
{
    private IConexion conexion;

    public PruebaCuotas()
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
        this.conexion.Cuotas!.Add(new Cuotas
        {
            id_credito = 3,
            id_tipoDePago = 1,
            fechaDePago = DateTime.Now,
            valorDeCuota = 100000,
            numeroDeCuota = 1
        });
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Cuotas!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.Cuotas!.FirstOrDefault(e => e.valorDeCuota == 100000);
        if (registro != null)
        {
            registro.valorDeCuota = 150000;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Cuotas!.FirstOrDefault(e => e.id_tipoDePago == 1 || e.valorDeCuota == 150000);
        if (registro != null)
        {
            conexion.Cuotas!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}