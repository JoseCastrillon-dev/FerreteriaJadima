using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaVehiculos
{
    private IConexion conexion;

    public PruebaVehiculos()
    {
        this.conexion = new Conexion();
        conexion.StringConexion = DatosGenerales.StringConexion();

    }

    [TestMethod]
    public void Ejecutar()
    {

        LimpiarResiduos();

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

        this.conexion.Vehiculos!.Add(new Vehiculos
        {
            matricula = "Matricula1",
            marca = "Marca1TEST",
            numeroDeChasis = "numChasis1TEST",
            numeroMotor = "numMotor1TEST"
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Vehiculos!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Vehiculos!.FirstOrDefault(e => e.matricula == "Matricula1");
        if (registro != null)
        {
            registro.matricula = "ESY288";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Vehiculos!.FirstOrDefault(e => e.matricula == "ESY288");
        if (registro != null)
        {
            conexion.Vehiculos!.Remove(registro);
            conexion.SaveChanges();
        }

    }


    //ESTE METODO ESTA GENERADO POR IA, ya que no encontre manera de podamos limpiar los registros antes de ejecutar el crud
    private void LimpiarResiduos()
    {
        var viejos = conexion.Vehiculos!
            .Where(e => e.matricula == "Matricula1" || e.matricula == "ESY288")
            .ToList();

        if (viejos.Any())
        {
            conexion.Vehiculos!.RemoveRange(viejos);
            conexion.SaveChanges();
        }
    }
}