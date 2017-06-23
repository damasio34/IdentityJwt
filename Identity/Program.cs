using System;
using Microsoft.Owin.Hosting;

namespace Identity
{
    class Program
    {
        static void Main()
        {
            var baseAddress = "http://localhost:9001/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Web API disponível na URL {baseAddress}");
                Console.ReadLine();
            }
        }
    }
}
