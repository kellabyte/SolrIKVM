using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace SolrIKVM.Host.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            if (Environment.UserInteractive || Environment.OSVersion.Platform == PlatformID.Unix)
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
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("-----------------------------");
                System.Console.WriteLine("Exception!!!!");
                System.Console.WriteLine("-----------------------------");
                WriteException(ex);
                System.Console.WriteLine("-----------------------------");
                System.Console.ReadKey();
            }
        }

        public static void WriteException(Exception ex)
        {            
            System.Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
            {
                System.Console.WriteLine("-----------------------------");
                WriteException(ex.InnerException);
            }
        }    
    }
}
