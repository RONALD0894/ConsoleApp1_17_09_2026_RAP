using System.Collections.Generic;

public interface IUserRepository
{
    IEnumerable<Usuario> GetAll();
    Usuario FindByName(string nombre);
    void Add(Usuario user);
}
