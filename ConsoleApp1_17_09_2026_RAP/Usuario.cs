using System.Collections.Generic;

public class Usuario : UsuarioBase
{
    // rolesAsignados es readonly y accesible para clases derivadas
    protected readonly List<Rol> rolesAsignados;

    public Usuario(string nombre, string password, List<Rol> roles)
        : base(nombre, password)
    {
        rolesAsignados = roles != null ? new List<Rol>(roles) : new List<Rol>();
    }

    // Exponer roles a las clases derivadas como colección de solo lectura
    protected IReadOnlyCollection<Rol> RolesAsignados => rolesAsignados.AsReadOnly();

    public override bool TienePermiso(Rol rol)
    {
        return rolesAsignados.Contains(rol);
    }
}
