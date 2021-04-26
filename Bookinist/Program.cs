using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Bookinist
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //EntityFrameworkProfilerBootstrapper.PreStart();
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(App.ConfigureServices);
    }
}

