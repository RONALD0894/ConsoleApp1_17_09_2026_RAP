using System;
using System.Collections.Generic;

namespace ConsoleApp1_17_09_2026_RAP
{
    // Clase encargada del flujo de la aplicación: demo, menú y autenticación.
    // Mantiene Main limpio (Single Responsibility for Program.Main).
    public class AppMenu
    {
        public void Run(string[] args)
        {
            // Procesar argumentos de arranque (por ejemplo --leerpass) dentro de AppMenu
            if (args != null && args.Length > 0 && args[0] == "--leerpass")
            {
                Console.Write("Prueba LeerPassword desde AppMenu. Ingrese una contraseña: ");
                var p = ConsoleUtils.LeerPassword();
                Console.WriteLine($"(Contraseña leída con {p.Length} caracteres)");
            }

            try
            {
                Console.WriteLine("=== Sistema de Autenticación ===\n");

                int intentos = 0;
                int maxIntentos = Config.MAX_INTENTOS;

                // Crear repositorio en memoria vacío y usar UserRegistrar para añadir usuarios (ISP, SRP)
                var repo = new InMemoryUserRepository();
                var registrar = new UserRegistrar(repo);
                // Puede lanzar ArgumentNullException si SeedUsuarios no devuelve lista
                registrar.Registrar(SeedUsuarios.ObtenerUsuarios());

                // Crear notificador y autenticador que dependen de abstracciones (DIP)
                INotification notifier = new ConsoleNotifier();
                Autenticador auth = new Autenticador(repo, notifier);

                // Mostrar ejemplos LINQ antes de pedir credenciales
                notifier.Notify("=== Demo consultas LINQ ===");

                notifier.Notify("LINQ usado: Query(), Where(), OrderBy(), ToList()");
                var admins = UserQueryExamples.GetByRole(repo, Rol.Admin);
                notifier.Notify("Admins:");
                foreach (var u in admins)
                {
                    notifier.Notify($" - {u.Nombre}");
                }

                notifier.Notify("LINQ usado: Query(), Where(...) (IndexOf case-insensitive), OrderBy(), ToList()");
                var search = UserQueryExamples.SearchByName(repo, "an");
                notifier.Notify("\nSearch by name fragment 'an':");
                foreach (var u in search)
                {
                    notifier.Notify($" - {u.Nombre}");
                }

                notifier.Notify("LINQ usado: Query(), OrderBy(), Skip(), Take(), ToList()");
                var page1 = UserQueryExamples.GetPaged(repo, 1, 5);
                notifier.Notify("\nPaged (page 1, size 5):");
                foreach (var u in page1)
                {
                    notifier.Notify($" - {u.Nombre}");
                }

                notifier.Notify("LINQ usado: Query(), Count(predicate)");
                int countUsuarios = UserQueryExamples.CountByRole(repo, Rol.Usuario);
                notifier.Notify($"\nCount usuarios with role Usuario: {countUsuarios}");

                // Bucle de autenticación
                while (intentos < maxIntentos)
                {
                    notifier.Notify("\nIngrese usuario: ");
                    string nombre = Console.ReadLine();

                    notifier.Notify("Ingrese contraseña: ");
                    string password = ConsoleUtils.LeerPassword();

                    notifier.Notify("Ingrese rol (Admin/Usuario/Invitado): ");
                    string rolInput = Console.ReadLine();

                    if (!Enum.TryParse<Rol>(rolInput, true, out Rol rol))
                    {
                        notifier.Notify("[X] Rol inválido. Intente de nuevo.");
                        continue; // no cuenta como intento
                    }

                    bool acceso;
                    try
                    {
                        acceso = auth.ValidarAcceso(nombre, password, rol);
                    }
                    catch (Exception ex)
                    {
                        // Capturar errores inesperados durante la validación y relanzar si es crítico
                        notifier.Notify($"Error durante autenticación: {ex.Message}");
                        throw;
                    }

                    if (acceso)
                    {
                        // Acceso correcto, salir del bucle
                        break;
                    }

                    // Si no hubo acceso, incrementar intentos y permitir reintentos hasta maxIntentos
                    intentos++;

                    if (intentos < maxIntentos)
                    {
                        notifier.Notify($"Intentos restantes: {maxIntentos - intentos}\n");
                    }
                    else
                    {
                        notifier.Notify("Final de prueba. Se alcanzó el número máximo de intentos. El programa se cerrará.");
                        Environment.Exit(0);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                // Manejo específico de argumentos nulos
                Console.WriteLine($"Error de argumento: {ex.ParamName} - {ex.Message}");
            }
            catch (FormatException ex)
            {
                // Manejo de formatos (por ejemplo, hashed password mal formado)
                Console.WriteLine($"Error de formato: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Captura general: registrar o mostrar mensaje amigable
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }
}
