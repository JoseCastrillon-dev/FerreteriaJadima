using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaEntregas
{
    private IConexion conexion;

    public PruebaEntregas()
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
        this.conexion.Entregas!.Add(new Entregas
        {
            id_domicilio = 1,
            id_empleado = 1,
            fechaSalida = DateTime.Now,
            fechaLlegada = DateTime.Now.AddHours(1)
        });
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Entregas!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.Entregas!.FirstOrDefault(e => e.fechaSalida == DateTime.Now);
        if (registro != null)
        {
            registro.fechaLlegada = DateTime.Now.AddHours(2);
            conexion.SaveChanges();
        }

    }

    public void Borrar()
    {
        var registro = conexion.Entregas!.FirstOrDefault(e => e.id_empleado == 1 && e.fechaSalida == DateTime.Now);
        if (registro != null)
        {
            conexion.Entregas!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}