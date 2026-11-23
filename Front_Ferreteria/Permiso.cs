namespace Front_Ferreteria
{
    public static class Permiso
    {
        public static bool Puede(ISession s, string accion)
            => s.GetString("P_" + accion) == "1";
    }
}