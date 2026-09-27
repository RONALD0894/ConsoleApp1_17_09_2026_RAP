using System.Collections.Generic;
using System.Linq;

// Implementa IUserRepository e IReadOnlyUserRepository utilizando un Dictionary interno
// para búsquedas O(1) por nombre (mejora de rendimiento).
public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, Usuario> _usersByName;

    public InMemoryUserRepository(IEnumerable<Usuario> initial)
    {
        _usersByName = new Dictionary<string, Usuario>();
        if (initial != null)
        {
            foreach (var u in initial)
            {
                if (u != null)
                    _usersByName[u.Nombre] = u;
            }
        }
    }

    // Constructor sin parámetros para crear repo vacío
    public InMemoryUserRepository()
    {
        _usersByName = new Dictionary<string, Usuario>();
    }

    public IEnumerable<Usuario> GetAll()
    {
        return _usersByName.Values;
    }

    public Usuario FindByName(string nombre)
    {
        if (nombre == null) return null;
        _usersByName.TryGetValue(nombre, out var user);
        return user;
    }

    public IQueryable<Usuario> Query()
    {
        return _usersByName.Values.AsQueryable();
    }

    public void Add(Usuario user)
    {
        if (user == null) return;
        _usersByName[user.Nombre] = user;
    }
}
