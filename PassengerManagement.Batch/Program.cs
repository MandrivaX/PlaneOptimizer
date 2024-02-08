using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PassengerManagement.Entities;
using PassengerManagement.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PassengerManagement.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                })
               .AddScoped<IPassengerManagementService, PassengerManagementService>() 
               .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            //l'ajout du stop watch pour avoir une idée sur les performences
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine(string.Format("Démarrage du batch à {0}" , DateTime.Now));

            var service = serviceProvider.GetService<IPassengerManagementService>();
            List<Family> families = service.CheckRulesAndGetFamilies(Extensions.GetPassengers());
            decimal optimizedTurnover = service.GetOptimizedTurnover(families, Extensions.AvailablePlace);
            
            sw.Stop();
            Console.WriteLine(string.Format(Extensions.OptimizedTurnoverMessage, optimizedTurnover));

            Console.WriteLine(string.Format("Batch excecuté en {0}", sw.ElapsedMilliseconds));
            Console.WriteLine(string.Format("Fin du batch à {0}", DateTime.Now));
        }
    }
}
