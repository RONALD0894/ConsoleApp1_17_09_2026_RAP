public interface IAutenticacion
{
    bool ValidarAcceso(string nombre, string password, Rol rol);
}
