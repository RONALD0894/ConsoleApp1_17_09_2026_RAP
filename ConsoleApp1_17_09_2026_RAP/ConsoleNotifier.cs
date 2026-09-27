using System;

// Implementación concreta de INotification que escribe en consola
// (Dependency Inversion - DIP: la lógica depende de la abstracción INotification)
public class ConsoleNotifier : INotification
{
    public void Notify(string message)
    {
        Console.WriteLine(message);
    }
}
