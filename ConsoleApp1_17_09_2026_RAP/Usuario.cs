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

// Ejemplo de Liskov Substitution Principle (LSP): UsuarioVIP es un Usuario especializado
// que añade funcionalidad sin romper el comportamiento esperado de Usuario.
public class UsuarioVIP : Usuario
{
    public int DiscountPercent { get; }

    public UsuarioVIP(string nombre, string password, List<Rol> roles, int discount)
        : base(nombre, password, roles)
    {
        DiscountPercent = discount;
    }

    // Comportamiento adicional no afecta a la sustitución: sigue siendo un Usuario válido.
}


    // Exponer roles a las clases derivadas como colección de solo lectura
    protected IReadOnlyCollection<Rol> RolesAsignados => rolesAsignados.AsReadOnly();

    public override bool TienePermiso(Rol rol)
    {
        return rolesAsignados.Contains(rol);
    }
}
