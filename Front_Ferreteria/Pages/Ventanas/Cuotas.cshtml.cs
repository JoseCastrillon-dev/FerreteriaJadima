using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class CuotasModel : PageModel
    {
        private ICuotasPresentacion ICuotasPresentacion;
        private ICreditosPresentacion ICreditosPresentacion;
        private ITiposDePagosPresentacion ITiposDePagosPresentacion;


        public CuotasModel(ICuotasPresentacion ICuotasPresentacion,
                                    ICreditosPresentacion ICreditosPresentacion,
                                    ITiposDePagosPresentacion ITiposDePagosPresentacion)
        {
            this.ICuotasPresentacion = ICuotasPresentacion;
            this.ICreditosPresentacion = ICreditosPresentacion;
            this.ITiposDePagosPresentacion = ITiposDePagosPresentacion;

            this.Filtro = new Cuotas();
            Filtro._credito = new Creditos();
            Filtro._tipoDePago = new TiposDePagos();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public List<Cuotas> Lista { get; set; }
        [BindProperty] public Cuotas? Filtro { get; set; }
        [BindProperty] public Cuotas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Creditos> ListaCreditos { get; set; }
        [BindProperty] public List<TiposDePagos> listaTiposDePagos { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._credito == null)
                {
                    this.Filtro._credito = new Creditos();
                }
                if (this.Filtro!._credito._estadoCredito == null)
                {
                    this.Filtro._credito._estadoCredito = new EstadoCreditos();
                }

                if (this.Filtro!._tipoDePago == null)
                {
                    this.Filtro._tipoDePago = new TiposDePagos();
                }


                this.Filtro!._credito._estadoCredito.estado = this.Filtro._credito._estadoCredito.estado ?? "";
                Lista = this.ICuotasPresentacion.Buscar(this.Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (FormFile != null)
                {
                    var memoryStream = new MemoryStream();
                    FormFile.CopyToAsync(memoryStream).Wait();
                    Actual!.imagen = Convert.ToBase64String(memoryStream.ToArray());
                    memoryStream.Dispose();
                }
                if (this.Actual!.id == 0)
                    this.Actual = this.ICuotasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.ICuotasPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarCuotas();
            }
            finally
            {
                OnPostBtBuscar();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Cuotas();
                CargarCuotas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtEditar(string data)
        {
            try
            {

                if (this.Actual!._tipoDePago == null)
                {
                    this.Actual._tipoDePago = new TiposDePagos();
                }


                if (this.Actual!._credito == null)
                {
                    this.Actual._credito = new Creditos ();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarCuotas();
                Actual = this.Lista.FirstOrDefault(x => x.id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtPregBorrar(string data)
        {
            try
            {
                Borrar = true;
                OnPostBtBuscar();
                Actual = this.Lista.FirstOrDefault(x => x.id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (this.Actual!.id == 0)
                    return;
                this.Actual = this.ICuotasPresentacion.Borrar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }


        private void CargarCuotas()
        {
            var FiltroCreditos = new Creditos
            {
                _estadoCredito = new EstadoCreditos()
                {
                    estado = ""
                },

                _venta = new Ventas()
                {
                    _cliente = new Clientes()
                    {
                        _persona = new Personas()
                        {
                            nombre = ""
                        }
                    },

                    _detalleVenta = new DetalleVentas()
                }
            };

            this.ListaCreditos =
                this.ICreditosPresentacion.Buscar(FiltroCreditos);

            var FiltroTiposDePagos = new TiposDePagos
            {
                metodo = ""
            };

            this.listaTiposDePagos =
                this.ITiposDePagosPresentacion.Buscar(FiltroTiposDePagos);
        }





    }
}
