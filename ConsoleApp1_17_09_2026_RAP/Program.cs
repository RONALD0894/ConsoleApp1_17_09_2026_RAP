using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_17_09_2026_RAP
{
    internal class Program
    {
        // Program solo contiene Main (la función de ejecución)

    static void Main(string[] args)
    {
        // Main se limita a ejecutar la aplicación; la lógica está en AppMenu.Run()
        var app = new AppMenu();
        app.Run(args);
    }
    }
}
