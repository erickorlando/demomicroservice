using System;
using System.IO;
using System.Net;
using Data.Context;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var ctx = new RoutingDbContextFactory().CreateDbContext(args))
                {
                    Console.WriteLine("Prueba de Ingreso a la BD");

                    for (int i = 0; i < 10000; i++)
                    {
                        Console.WriteLine($"Insertando registro {i}");

                        var item = new Route
                        {
                            Code = $"R{i}",
                            Color = i,
                            CountryCode = "PE",
                            Name = $"Route {i:00}",
                        };

                        ctx.Database.ExecuteSqlCommand("uspInsertRoute @p0,@p1,@p2,@p3", item.Code, item.Name,
                            item.CountryCode, item.Color);

                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {ex.InnerException?.Message}");
            }
            finally
            {
                Console.ReadLine();
            }

        }
    }
}
