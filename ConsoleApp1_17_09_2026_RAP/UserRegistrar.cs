using System.Collections.Generic;

// Clase responsable solo de registrar usuarios en un repositorio (Single Responsibility - SRP)
public class UserRegistrar
{
    private readonly IUserRepository _repo;

    public UserRegistrar(IUserRepository repo)
    {
        _repo = repo;
    }

    public void Registrar(IEnumerable<Usuario> usuarios)
    {
        if (usuarios == null) throw new System.ArgumentNullException(nameof(usuarios));
        foreach (var u in usuarios)
        {
            _repo.Add(u);
        }
    }
}
