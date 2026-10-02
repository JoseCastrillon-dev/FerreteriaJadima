namespace lib_aplicaciones.nucleo
{
    public class DatosGenerales
    {
        public static string StringConexion()
        {
            return "server=localhost;database=Ferreteria_Jadima;Integrated Security=True;TrustServerCertificate=true;";
            //"server=.\\SQLEXPRESS;database=Ferreteria_Jadima;Integrated Security=True;TrustServerCertificate=true;" para simon q por algun motivo solo me sirve conectandome ahi y no con localhost
            //"server=localhost;database=Ferreteria_Jadima;Integrated Security=True;TrustServerCertificate=true;" el correcto para tenerlo aqui guardado
        }
    }
}