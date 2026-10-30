using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaEmpleado
{
    private IConexion conexion;



    public PruebaEmpleado()
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

        this.conexion.Empleados!.Add(new Empleados
        {
            id_persona = 11,
            id_salario = 1,
            carnet = "carnetTest",
            Genero = "generotest"
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Empleados!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Empleados!.FirstOrDefault(e => e.carnet == "carnetTest");
        if (registro != null)
        {
            registro.carnet = "carnetTestACTUALIZADO";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Empleados!.FirstOrDefault(e => e.id_persona == 11 && e.carnet == "carnetTestACTUALIZADO");
        if (registro != null)
        {
            conexion.Empleados!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}