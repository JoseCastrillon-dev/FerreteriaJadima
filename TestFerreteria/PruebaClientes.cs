using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Tests;

[TestClass]
public sealed class PruebaClientes
{
    private IConexion conexion;
    private Personas persona;
    private Personas nuevapersona;
    private Clientes cliente;

    public PruebaClientes()
    {
        this.conexion = new Conexion();
        conexion.StringConexion = DatosGenerales.StringConexion();

        this.persona = new Personas
        {
            cedula = "PersonaTestC",
            nombre = "PersonaTestCliente",
            apellido = "TESTAPELLIDO",
            numeroTelefono = "1234"
        };

        this.nuevapersona = new Personas
        {
            cedula = "PersonaTestCl",
            nombre = "PersonaTestCl",
            apellido = "TESTAPELLIDO",
            numeroTelefono = "1234"
        };

        this.cliente = new Clientes();
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
        this.conexion.Personas!.Add(this.persona);
        this.conexion.SaveChanges();

        this.cliente.id_persona = this.persona.id;
        this.conexion.Clientes!.Add(this.cliente);
        this.conexion.SaveChanges();
    }

    public void Consultar()
    {
        var lista = conexion.Clientes!.AsNoTracking().ToList();
        if (lista.Count == 0)
        {
            throw new Exception("Lista vacia");
        }
    }

    public void Actualizar()
    {
        this.conexion.Personas!.Add(this.nuevapersona);
        this.conexion.SaveChanges();

        this.cliente.id_persona = this.nuevapersona.id;
        conexion.SaveChanges();
    }

    public void Borrar()
    {
        // Como ya están siendo rastreados por la conexión, los removemos directamente sin buscar a ciegas
        if (this.cliente != null && this.cliente.id != 0)
        {
            conexion.Clientes!.Remove(this.cliente);
            conexion.SaveChanges();
        }

        if (this.nuevapersona != null && this.nuevapersona.id != 0)
        {
            conexion.Personas!.Remove(this.nuevapersona);
            conexion.SaveChanges();
        }

        if (this.persona != null && this.persona.id != 0)
        {
            conexion.Personas!.Remove(this.persona);
            conexion.SaveChanges();
        }
    }
}