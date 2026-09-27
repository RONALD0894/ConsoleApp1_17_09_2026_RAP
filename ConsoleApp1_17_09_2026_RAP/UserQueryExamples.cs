using System;
using System.Collections.Generic;
using System.Linq;

// Ejemplos de uso del patrón Repositorio junto con LINQ
public static class UserQueryExamples
{
    // Obtener usuarios que tienen un rol específico
    public static IEnumerable<Usuario> GetByRole(IReadOnlyUserRepository repo, Rol role)
    {
        // LINQ methods used: Query() -> IQueryable<Usuario>, Where(...), OrderBy(...), ToList()
        return repo.Query()
                   .Where(u => u.TienePermiso(role))
                   .OrderBy(u => u.Nombre)
                   .ToList();
    }

    // Buscar usuarios cuyo nombre contiene el fragmento (case-insensitive)
    public static IEnumerable<Usuario> SearchByName(IReadOnlyUserRepository repo, string fragment)
    {
        if (string.IsNullOrEmpty(fragment)) return Enumerable.Empty<Usuario>();
        // LINQ methods used: Query(), Where(...) (with IndexOf for case-insensitive match), OrderBy(), ToList()
        return repo.Query()
                   .Where(u => u.Nombre.IndexOf(fragment, StringComparison.OrdinalIgnoreCase) >= 0)
                   .OrderBy(u => u.Nombre)
                   .ToList();
    }

    // Paginación y ordenación por nombre
    public static IEnumerable<Usuario> GetPaged(IReadOnlyUserRepository repo, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        // LINQ methods used: Query(), OrderBy(), Skip(...), Take(...), ToList()
        return repo.Query()
                   .OrderBy(u => u.Nombre)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
    }

    // Contar usuarios por rol
    public static int CountByRole(IReadOnlyUserRepository repo, Rol role)
    {
        // LINQ methods used: Query(), Count(predicate)
        return repo.Query().Count(u => u.TienePermiso(role));
    }
}
