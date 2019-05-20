using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq;

namespace Dlw.Techorama2019.Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json.user", optional: false, reloadOnChange: true)
                .Build();

            using (var context = new EFCoreContext(config.GetConnectionString(nameof(EFCoreContext))))
            {
                // Ensure context is initialized.
                context.Products.FirstOrDefault();
                Console.WriteLine("Database initialized.");

                QueryProducts(context);
            }
        }

        private static void QueryProducts(EFCoreContext context)
        {
            var sw = Stopwatch.StartNew();

            // Takes about 30 seconds.
            var results = context.Products
                .Include(p => p.Properties)
                .Where(pd => pd.Properties.Any(prop => prop.Name == "prop-20"
                    && prop.Value.StartsWith("Property value 20"))
                )
                .GroupBy(pd => pd.IsActive == null ? false : pd.IsActive)
                .Select(g => new
                {
                    Active = g.Key,
                    Count = g.Count()
                })
                .ToList();

            Console.WriteLine($"Queried {results.Count} results in: {sw.Elapsed}");
            foreach (var result in results)
            {
                Console.WriteLine($"Active {result.Active?.ToString() ?? "NULL"}: {result.Count}");
            }
        }
    }
}