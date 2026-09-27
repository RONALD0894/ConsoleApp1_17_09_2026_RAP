using System.Collections.Generic;

public static class SeedUsuarios
{
    private static readonly List<Usuario> _usuarios = new List<Usuario>
    {
        new Usuario("ana", PasswordHasher.HashPassword("admin123"), new List<Rol> { Rol.Admin, Rol.Usuario }),
        new Usuario("luis", PasswordHasher.HashPassword("userpwd1"), new List<Rol> { Rol.Usuario }),
        new Usuario("maria", PasswordHasher.HashPassword("guestpwd"), new List<Rol> { Rol.Invitado }),
        new Usuario("pedro", PasswordHasher.HashPassword("pedro123"), new List<Rol> { Rol.Admin }),
        new Usuario("juan", PasswordHasher.HashPassword("juan2026"), new List<Rol> { Rol.Usuario }),
        new Usuario("carla", PasswordHasher.HashPassword("carla!@#"), new List<Rol> { Rol.Usuario, Rol.Invitado }),
        new Usuario("sofia", PasswordHasher.HashPassword("sofiapwd"), new List<Rol> { Rol.Invitado }),
        new Usuario("diego", PasswordHasher.HashPassword("diego456"), new List<Rol> { Rol.Usuario }),
        new Usuario("laura", PasswordHasher.HashPassword("laura789"), new List<Rol> { Rol.Usuario }),
        new Usuario("jose", PasswordHasher.HashPassword("jose000"), new List<Rol> { Rol.Invitado }),
        new Usuario("marta", PasswordHasher.HashPassword("marta111"), new List<Rol> { Rol.Usuario }),
        new Usuario("pablo", PasswordHasher.HashPassword("pablo222"), new List<Rol> { Rol.Usuario, Rol.Invitado }),
        new Usuario("lucas", PasswordHasher.HashPassword("lucas333"), new List<Rol> { Rol.Usuario }),
        new Usuario("elena", PasswordHasher.HashPassword("elena444"), new List<Rol> { Rol.Invitado }),
        new Usuario("rafael", PasswordHasher.HashPassword("rafael555"), new List<Rol> { Rol.Usuario }),
        new Usuario("isabel", PasswordHasher.HashPassword("isabel66"), new List<Rol> { Rol.Usuario }),
        new Usuario("andres", PasswordHasher.HashPassword("andres77"), new List<Rol> { Rol.Invitado }),
        new Usuario("julia", PasswordHasher.HashPassword("julia88"), new List<Rol> { Rol.Usuario }),
        new Usuario("roberto", PasswordHasher.HashPassword("roberto99"), new List<Rol> { Rol.Usuario }),
        new Usuario("silvia", PasswordHasher.HashPassword("silvia00"), new List<Rol> { Rol.Invitado })
    };

    public static IReadOnlyList<Usuario> ObtenerUsuarios()
    {
        return _usuarios.AsReadOnly();
    }
}

