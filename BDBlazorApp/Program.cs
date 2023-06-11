using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BDBlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilt = CreateHostBuilder(args).Build();

            hostBuilt.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
