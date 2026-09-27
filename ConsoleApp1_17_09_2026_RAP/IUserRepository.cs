using System.Collections.Generic;

// Interface que extiende la interfaz de solo lectura (Interface Segregation - ISP)
public interface IUserRepository : IReadOnlyUserRepository
{
    void Add(Usuario user);
}
