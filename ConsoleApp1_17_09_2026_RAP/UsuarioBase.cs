using System;

public abstract class UsuarioBase
{
    private string _nombre;
    private string _password;

    // Propiedad con validación para Nombre
    public string Nombre
    {
        get { return _nombre; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("El nombre no puede estar vacío. Se asignará 'Desconocido'.");
                _nombre = "Desconocido";
            }
            else
            {
                _nombre = value;
            }
        }
    }

    // Propiedad con validación para Password
    public string Password
    {
        get { return _password; }
        protected set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
            {
                Console.WriteLine("La contraseña debe tener al menos 4 caracteres. Se asignará '1234'.");
                _password = "1234";
            }
            else
            {
                _password = value;
            }
        }
    }

    public UsuarioBase(string nombre, string password)
    {
        Nombre = nombre;
        Password = password;
    }

    public abstract bool TienePermiso(Rol rol);
}
