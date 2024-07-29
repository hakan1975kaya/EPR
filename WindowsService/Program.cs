using Autofac.Extensions.DependencyInjection;
using Autofac;
using Topshelf;
using System.Reflection;
using Business.DependencyResolvers.Autofac;
using WindowsService.Concrete;
using WindowsService.Abstract;
using Microsoft.Extensions.Logging.EventLog;

namespace WindowsService
{
    public class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            RunWindowsServiceWithHost(host);
        }

        private static void RunWindowsServiceWithHost(IHost host)
        {
            var rc = HostFactory.Run(x =>
            {
                x.SetDisplayName("WindowsService");

                x.SetDescription("WindowsService");

                x.SetServiceName("WindowsService");

                var WindowsService = host.Services.GetRequiredService<IWindowsService>();

                x.Service<IWindowsService>(s =>
                {
                    s.ConstructUsing(() => WindowsService);

                    s.WhenStarted(s => s.OnStart());
                    s.WhenStopped(s => s.OnStop());
                    s.WhenPaused(s => s.OnPause());
                    s.WhenContinued(s => s.OnContinue());
                });


                x.RunAsLocalSystem();

                x.StartAutomatically();
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());

            Environment.ExitCode = exitCode;

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)

                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())

                 .ConfigureContainer<ContainerBuilder>(builder =>
                 {

                     builder.RegisterModule(new AutofacBusinessModule());

                 })
                .ConfigureLogging(builder =>
                {
                    builder.AddEventLog(new EventLogSettings
                    {
                        SourceName = "WindowsService",
                        LogName = "WindowsService"
                    });
                })
                .ConfigureServices((_, services) => services

                .AddSingleton<IWindowsService, WindowsManager>()

                );


    }
}