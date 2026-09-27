// Abstracción para notificaciones/Salida de mensajes (Dependency Inversion - DIP)
public interface INotification
{
    void Notify(string message);
}
