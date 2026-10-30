using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestClass]
public sealed class PruebaDomicilios
{
    private IConexion conexion;

    public PruebaDomicilios()
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
        this.conexion.Domicilios!.Add(new Domicilios
        {
            id_venta = 3,
            id_vehiculo = 6,
            numeroDeTelefono = "3001234567",
            direccion = "Calle Test #123"
        });
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Domicilios!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        var registro = conexion.Domicilios!.FirstOrDefault(e => e.numeroDeTelefono == "3001234567");
        if (registro != null)
        {
            registro.direccion = "Calle Actualizada #456";
            conexion.SaveChanges();
        }
        else
        {
            throw new Exception("No se encontró el registro para actualizar.");
        }
    }

    public void Borrar()
    {
        var registro = conexion.Domicilios!.FirstOrDefault(e => e.id_venta == 6 || e.direccion == "Calle Actualizada #456");
        if (registro != null)
        {
            conexion.Domicilios!.Remove(registro);
            conexion.SaveChanges();
        }
    }
}