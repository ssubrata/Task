using Api.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api {
    public class Program {
        public static void Main (string[] args) {
            var host = CreateHostBuilder (args).Build ();

            /*
              Intialize In Memory Database When First Time Load;
            */
            using (var scope = host.Services.CreateScope ()) {
                var context = scope.ServiceProvider.GetRequiredService<DataDbContext> ();
                /*
                  In Memory Data Load;
                */
                Seed.DbIntitalize (context);
            };
            host.Run ();

        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseStartup<Startup> ();
            });
    }
}