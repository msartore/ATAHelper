using System;
using Ionic.Zip;
using System.Net;
using System.Reflection;
using System.Globalization;
using System.IO;

namespace ATAHelper
{
    class ATA
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            Console.WriteLine("ATAHelper started");
            if (args.Length != 0)
            {
                //ServicePointManager internet manager
                if (CheckForInternetConnection())
                {
                    if (args.Length == 3)
                    {
                        switch (args[0])
                        {
                            case "e":
                                if (args.Length == 3)
                                {
                                    Unzip(args[1], args[2]);
                                }
                                break;

                            case "d":
                                if (CheckForInternetConnection())
                                {
                                    if (args.Length == 3)
                                    {
                                        downloadfile(args[1], args[2]);
                                    }
                                }
                                else
                                {
                                    Error(3);
                                }
                                break;
                            default:
                                Error(2);
                                break;
                        }
                    }
                    else
                    {
                        Error(2);
                    }
                }
                else
                {
                    Error(1);
                }
            }
            else
            {
                Console.WriteLine("\nWelcome to ATAHelper Version 0.1 [Copyright-2020 | Massimiliano Sartore]\n");
                Error(0);
            }
            Console.WriteLine("ATAHelper Finished");
        }
        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = new AssemblyName(args.Name);

            var path = assemblyName.Name + ".dll";
            if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false) path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);

            using (Stream stream = executingAssembly.GetManifestResourceStream(path))
            {
                if (stream == null) return null;

                var assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public static void Unzip(string DirectoryFile, string OutputDirectory)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(DirectoryFile))
                {
                    zip.ExtractAll(OutputDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex);
            }
        }
        public static void downloadfile(string Url, string Filename)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                Console.WriteLine("Download started " + Filename);
                using (var client = new WebClient())
                {
                    client.DownloadFile(Url, Filename);
                }
                Console.WriteLine(Filename + " Downloaded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex);
            }
        }
        private static void Error(int errorNumber)
        {
            string errorSentence = "";
            switch (errorNumber)
            {
                case 0:
                    errorSentence = "Error! [No arguments were provided]";
                    break;
                case 1:
                    errorSentence = "Error! [Internet not available]";
                    break;
                case 2:
                    errorSentence = "Error! [Wrong arguments were provided]";
                    break;
                case 3:
                    errorSentence = "Error! [Download failed]";
                    break;
                default:
                    break;
            }
            Console.WriteLine(errorSentence);
        }
    }
}
