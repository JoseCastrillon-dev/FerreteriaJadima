using lib_aplicaciones.nucleo;
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Implementaciones;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    // Base comun de las pruebas: crea la conexion y arma los registros "padre" que piden las llaves foraneas.
    // Todo lo que se crea queda registrado y se borra al final (en orden inverso) con Limpiar().
    public abstract class PruebaBase
    {
        protected IConexion conexion;
        private readonly List<Action> limpieza = new List<Action>();

        protected PruebaBase()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = DatosGenerales.StringConexion();
        }

        // Texto aleatorio para las columnas UNIQUE (cargos, productos, vehiculos...) y para que las pruebas
        // no se pisen entre si, porque corren en paralelo.
        protected static string Sufijo(int largo)
        {
            return Guid.NewGuid().ToString("N").Substring(0, largo).ToUpper();
        }

        protected void Registrar<T>(T entidad) where T : class
        {
            this.limpieza.Add(() =>
            {
                try
                {
                    var id = (int)typeof(T).GetProperty("id")!.GetValue(entidad)!;
                    ((DbContext)this.conexion).Database.ExecuteSqlRaw(
                        "DELETE FROM [" + typeof(T).Name + "] WHERE [id] = {0}", id);
                }
                catch (Exception)
                {
                    // Si la fila ya no existe o todavia tiene hijos, se sigue con el resto de la limpieza.
                }
            });
        }

        protected T Guardar<T>(T entidad) where T : class
        {
            this.conexion.Entry<T>(entidad).State = EntityState.Added;
            this.conexion.SaveChanges();
            Registrar(entidad);
            return entidad;
        }

        protected void Limpiar()
        {
            for (int i = this.limpieza.Count - 1; i >= 0; i--)
                this.limpieza[i]();
            this.limpieza.Clear();
        }

        protected Personas CrearPersona()
        {
            return Guardar(new Personas()
            {
                cedula = Sufijo(10),
                nombre = "Juan",
                apellido = "Perez",
                numeroTelefono = "3001234567"
            });
        }

        protected Cargos CrearCargo()
        {
            return Guardar(new Cargos()
            {
                nombre = "Vendedor-" + Sufijo(8)
            });
        }

        protected Tipos CrearTipo()
        {
            return Guardar(new Tipos()
            {
                nombre = "Herramientas"
            });
        }

        protected TiposDePagos CrearTipoDePago()
        {
            return Guardar(new TiposDePagos()
            {
                metodo = "Efectivo"
            });
        }

        protected EstadoCreditos CrearEstadoCredito()
        {
            return Guardar(new EstadoCreditos()
            {
                estado = "Vigente"
            });
        }

        protected Vehiculos CrearVehiculo()
        {
            return Guardar(new Vehiculos()
            {
                matricula = Sufijo(8),
                marca = "Chevrolet",
                numeroDeChasis = "CH" + Sufijo(20),
                numeroMotor = "MT" + Sufijo(20)
            });
        }

        protected Proveedores CrearProveedor()
        {
            return Guardar(new Proveedores()
            {
                nombre = "Ferrosur",
                numeroDeTelefono = "3109876543",
                direccion = "Calle 50 # 10-20",
                email = "ventas@ferrosur.com"
            });
        }

        protected Salarios CrearSalario()
        {
            var cargo = CrearCargo();
            var tipoPago = CrearTipoDePago();
            return Guardar(new Salarios()
            {
                id_cargo = cargo.id,
                id_tiposDePago = tipoPago.id,
                valorHora = 8000m,
                horasTrabajadas = 160
            });
        }

        protected Clientes CrearCliente()
        {
            var persona = CrearPersona();
            return Guardar(new Clientes()
            {
                id_persona = persona.id
            });
        }

        protected Productos CrearProducto()
        {
            var tipo = CrearTipo();
            return Guardar(new Productos()
            {
                id_tipo = tipo.id,
                codigo = "P-" + Sufijo(8),
                cantidad = 100,
                nombre = "Martillo-" + Sufijo(8),
                valor = 10000m
            });
        }

        protected DetalleVentas CrearDetalleVenta()
        {
            var producto = CrearProducto();
            return Guardar(new DetalleVentas()
            {
                id_producto = producto.id,
                cantidad = 2,
                iva = 3800m,
                total = 23800m
            });
        }

        protected Empleados CrearEmpleado()
        {
            var persona = CrearPersona();
            var salario = CrearSalario();
            return Guardar(new Empleados()
            {
                id_persona = persona.id,
                id_salario = salario.id,
                carnet = "E" + Sufijo(10),
                Genero = "Masculino"
            });
        }

        protected Compras CrearCompra()
        {
            var proveedor = CrearProveedor();
            var empleado = CrearEmpleado();
            return Guardar(new Compras()
            {
                id_proveedor = proveedor.id,
                id_empleado = empleado.id,
                fechaCompra = DateTime.Now,
                total = 150000m
            });
        }

        protected Ventas CrearVenta()
        {
            var detalle = CrearDetalleVenta();
            var tipoPago = CrearTipoDePago();
            var empleado = CrearEmpleado();
            var cliente = CrearCliente();
            return Guardar(new Ventas()
            {
                id_detalleVenta = detalle.id,
                id_tipoDePago = tipoPago.id,
                id_empleado = empleado.id,
                id_cliente = cliente.id,
                iva = 3800m,
                total = 23800m
            });
        }

        protected Creditos CrearCredito()
        {
            var venta = CrearVenta();
            var estado = CrearEstadoCredito();
            return Guardar(new Creditos()
            {
                idVenta = venta.id,
                id_estadoCredito = estado.id,
                fechaPrestamo = DateTime.Now,
                valorPrestamo = 500000m
            });
        }

        protected Domicilios CrearDomicilio()
        {
            var venta = CrearVenta();
            var vehiculo = CrearVehiculo();
            return Guardar(new Domicilios()
            {
                id_venta = venta.id,
                id_vehiculo = vehiculo.id,
                numeroDeTelefono = "3001234567",
                direccion = "Calle 10 # 20-30"
            });
        }
    }
}
// todo esto sirve para tener datos q llamar cuando son necesarias clases foraneas para no tener q tener los datos alacenados el los crea y los limpia cuando son necesarios