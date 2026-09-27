using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_17_09_2026_RAP
{
    internal class Program
    {
        // Método para leer contraseña oculta con *
        public static string LeerPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

    static void Main(string[] args)
    {
        // Registrar usuarios desde SeedUsuarios a través de repositorio (más abajo)

        Console.WriteLine("=== Sistema de Autenticación ===\n");

        int intentos = 0;
        int maxIntentos = Config.MAX_INTENTOS;
        // Crear repositorio en memoria con datos de prueba
        var repo = new InMemoryUserRepository(SeedUsuarios.ObtenerUsuarios());
        Autenticador auth = new Autenticador(repo);

        while (intentos < maxIntentos)
        {
            Console.Write("Ingrese usuario: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese contraseña: ");
            string password = LeerPassword();

            Console.Write("Ingrese rol (Admin/Usuario/Invitado): ");
            string rolInput = Console.ReadLine();

            if (!Enum.TryParse<Rol>(rolInput, true, out Rol rol))
            {
                Console.WriteLine("[X] Rol inválido. Intente de nuevo.");
                continue; // no cuenta como intento
            }

            bool acceso = auth.ValidarAcceso(nombre, password, rol);

            if (acceso)
            {
                // Acceso correcto, salir del bucle
                break;
            }

            // Si no hubo acceso, incrementar intentos y permitir reintentos hasta maxIntentos
            intentos++;

            if (intentos < maxIntentos)
            {
                Console.WriteLine($"Intentos restantes: {maxIntentos - intentos}\n");
            }
            else
            {
                Console.WriteLine("Final de prueba. Se alcanzó el número máximo de intentos. El programa se cerrará.");
                Environment.Exit(0);
            }
        }
    }
    }
}
