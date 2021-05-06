using System;
using Microsoft.Extensions.Hosting;

namespace Bookinist
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
            //EntityFrameworkProfilerBootstrapper.PreStart();
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
#endif
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(App.ConfigureServices);
    }
}

