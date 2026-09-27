using System.Collections.Generic;
using System.Linq;

// Interface segregada: solo lectura de usuarios (Interface Segregation - ISP)
// Añadimos Query() para permitir consultas LINQ desde la capa de aplicación
public interface IReadOnlyUserRepository
{
    IEnumerable<Usuario> GetAll();
    Usuario FindByName(string nombre);
    // Permite ejecutar consultas LINQ (IQueryable) sobre la colección de usuarios
    IQueryable<Usuario> Query();
}
