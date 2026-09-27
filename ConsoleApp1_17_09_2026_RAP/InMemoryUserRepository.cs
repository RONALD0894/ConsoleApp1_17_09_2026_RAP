using System.Collections.Generic;
using System.Linq;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<Usuario> _users;

    public InMemoryUserRepository(IEnumerable<Usuario> initial)
    {
        _users = initial != null ? new List<Usuario>(initial) : new List<Usuario>();
    }

    public IEnumerable<Usuario> GetAll()
    {
        return _users.AsReadOnly();
    }

    public Usuario FindByName(string nombre)
    {
        return _users.FirstOrDefault(u => u.Nombre == nombre);
    }

    public void Add(Usuario user)
    {
        if (user == null) return;
        _users.RemoveAll(u => u.Nombre == user.Nombre);
        _users.Add(user);
    }
}
