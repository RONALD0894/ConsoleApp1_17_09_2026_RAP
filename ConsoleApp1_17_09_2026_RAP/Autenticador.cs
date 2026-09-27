using System;
using System.Collections.Generic;

// Autenticador responsable únicamente de la lógica de autenticación (Single Responsibility - SRP)
// Depende de abstracciones (IReadOnlyUserRepository, INotification) para cumplir Dependency Inversion (DIP)
public class Autenticador : IAutenticacion
{
    private readonly IReadOnlyUserRepository _repo;
    private readonly INotification _notifier;

    public Autenticador(IReadOnlyUserRepository repo, INotification notifier)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
    }

    public bool ValidarAcceso(string nombre, string password, Rol rol)
    {
        var u = _repo.FindByName(nombre);
        if (u == null)
        {
            _notifier.Notify("[X] Usuario no encontrado.");
            return false;
        }

        if (!PasswordHasher.Verify(u.Password, password))
        {
            _notifier.Notify("[X] Contraseña incorrecta.");
            return false;
        }

        if (!u.TienePermiso(rol))
        {
            _notifier.Notify("[X] El usuario no tiene el rol requerido.");
            return false;
        }

        MostrarAcceso(u.Nombre, rol);
        return true;
    }

    private void MostrarAcceso(string nombre, Rol rol)
    {
        switch (rol)
        {
            case Rol.Admin:
                _notifier.Notify($"{nombre} tiene acceso completo.");
                break;
            case Rol.Usuario:
                _notifier.Notify($"{nombre} puede consultar y modificar datos.");
                break;
            case Rol.Invitado:
                _notifier.Notify($"{nombre} solo puede leer información.");
                break;
        }
    }

    // Método de utilidad para listar usuarios (usa la abstracción de notificador)
    public void ListarUsuarios()
    {
        _notifier.Notify("=== Usuarios registrados ===");
        foreach (var u in _repo.GetAll())
        {
            _notifier.Notify($"Usuario: {u.Nombre}");
        }
    }
}
