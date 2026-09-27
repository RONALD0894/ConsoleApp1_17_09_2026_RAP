using System;
using System.Collections.Generic;

public class Autenticador : IAutenticacion
{
    private Dictionary<string, Usuario> usuarios = new Dictionary<string, Usuario>();

    public Autenticador()
    {
    }

    public Autenticador(IUserRepository repo)
    {
        if (repo != null)
        {
            foreach (var u in repo.GetAll())
            {
                usuarios[u.Nombre] = u;
            }
        }
    }

    public void RegistrarUsuario(Usuario usuario)
    {
        usuarios[usuario.Nombre] = usuario;
    }

    public bool ValidarAcceso(string nombre, string password, Rol rol)
    {
        if (usuarios.ContainsKey(nombre))
        {
            Usuario u = usuarios[nombre];

            if (PasswordHasher.Verify(u.Password, password))
            {
                // Verificar rol
                if (u.TienePermiso(rol))
                {
                    MostrarAcceso(nombre, rol);
                    return true;
                }
                Console.WriteLine("[X] El usuario no tiene el rol requerido.");
            }
            else
            {
                Console.WriteLine("[X] Contraseña incorrecta.");
            }
        }
        else
        {
            Console.WriteLine("[X] Usuario no encontrado.");
        }
        return false;
    }

    private void MostrarAcceso(string nombre, Rol rol)
    {
        switch (rol)
        {
            case Rol.Admin:
                Console.WriteLine($"{nombre} tiene acceso completo.");
                break;
            case Rol.Usuario:
                Console.WriteLine($"{nombre} puede consultar y modificar datos.");
                break;
            case Rol.Invitado:
                Console.WriteLine($"{nombre} solo puede leer información.");
                break;
        }
    }

    public void ListarUsuarios()
    {
        Console.WriteLine("=== Usuarios registrados ===");
        foreach (var kvp in usuarios)
        {
            Console.WriteLine($"Usuario: {kvp.Key}");
        }
    }
}
