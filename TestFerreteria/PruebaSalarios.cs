using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaSalarios
{
    private IConexion conexion;

    public PruebaSalarios()
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

        this.conexion.Salarios!.Add(new Salarios
        {
            id_cargo = 1,
            id_tiposDePago = 1,
            valorHora = 1000,
            horasTrabajadas = 8
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Salarios!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Salarios!.FirstOrDefault(e => e.valorHora == 1000);
        if (registro != null)
        {
            registro.valorHora = 2000;
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Salarios!.FirstOrDefault(e => e.id_cargo == 1 && e.valorHora == 2000);
        if (registro != null)
        {
            conexion.Salarios!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}