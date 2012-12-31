using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace SolrIKVM
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                if (args.Length == 0)
                {
                    RunAsConsoleApplication();
                }
                else if (args[0].StartsWith("/i"))
                {
                    InstallAsWindowsService();
                }
                else if (args[0].StartsWith("/u"))
                {
                    UninstallAsWindowsService();
                }
            }
            else
            {
                RunAsWindowsService();
            }
            return 0;
        }

        private static void RunAsWindowsService()
        {
            ServiceBase.Run(new SolrWindowsService());
        }

        private static void UninstallAsWindowsService()
        {

            ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
        }

        private static void InstallAsWindowsService()
        {
            ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
        }

        private static void RunAsConsoleApplication()
        {
            try
            {
                var solrService = new SolrWindowsService();
                solrService.Start();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Exception!!!!");
                Console.WriteLine("-----------------------------");
                WriteException(ex);
                Console.WriteLine("-----------------------------");
                Console.ReadKey();
            }
        }

        public static void WriteException(Exception ex)
        {
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("-----------------------------");
                WriteException(ex.InnerException);
            }
        }
    }
}