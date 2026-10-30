using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaPersona
{
    private IConexion conexion;

    public PruebaPersona()
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

            this.conexion.Personas!.Add(new Personas
            {
                cedula = "123",
                nombre = "TEST NOMBRE",
                apellido = "TEST APELLIDO",
                numeroTelefono = "123"
            });

            this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Personas!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Personas!.FirstOrDefault(e => e.cedula == "123");
        if (registro != null)
        {
            registro.nombre = "TEST NOMBRE ACTUALIZADO";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Personas!.FirstOrDefault(e => e.cedula == "123");
        if (registro != null)
        {
            conexion.Personas!.Remove(registro);
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para borrar.");
        }
    }
}