using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaEstanterias
{
    private IConexion conexion;



    public PruebaEstanterias()
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

        this.conexion.Estanterias!.Add(new Estanterias
        {
            id_empleado = 1,
            id_producto = 1,
            codigo = "CodigoTest",
            Stock = 10
        });

        this.conexion.SaveChanges();

    }

    public void Consultar()
    {
        var lista = conexion.Estanterias!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }

    }

    public void Actualizar()
    {
        var registro = conexion.Estanterias!.FirstOrDefault(e => e.codigo == "CodigoTest");
        if (registro != null)
        {
            registro.codigo = "CodigoTestACTUALIZADO";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Estanterias!.FirstOrDefault(e => e.id_empleado == 1 && e.codigo == "CodigoTestACTUALIZADO");
        if (registro != null)
        {
            conexion.Estanterias!.Remove(registro);
            conexion.SaveChanges();
        }

    }
}