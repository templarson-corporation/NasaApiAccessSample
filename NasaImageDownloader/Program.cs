using NasaApiGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;

namespace NasaImageDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string api = "apod";
            string dateString = String.Empty;
            string folder = String.Empty;
            string dateListFilePath = String.Empty;

            if (0 == args.Length || 0 == String.Compare(args[0], "/?"))
            {
                PrintUsage();
                //return;
            }
            else
            {
                List<string> arguments = args.ToList<string>();
                foreach (string param in arguments)
                {
                    if (param.StartsWith("/api:"))
                    {
                        string[] p = param.Split(':');
                        api = p[1];
                    }

                    if (param.StartsWith("/date:"))
                    {
                        string[] p = param.Split(':');
                        dateString = p[1];
                    }

                    if (param.StartsWith("/folder:"))
                    {
                        string[] p = param.Split(':');
                        if (2 < p.Length)
                        {
                            folder = p[1] + ":" + p[2];
                        }
                    }

                    if (param.StartsWith("/datelist:"))
                    {
                        string[] p = param.Split(':');
                        if (2 < p.Length)
                        {
                            dateListFilePath = p[1] + ":" + p[2];
                        }
                    }
                }
            }

             if (0 != String.Compare(api.ToUpper(), "APOD"))
            {
                Console.WriteLine("\nERROR: API \"{0}\" is not supported.", api);
                return;
            }

            //dateListFilePath = @"C:\Users\Mitko\source\NasaApiAccessSample\NasaApiGateway\NasaImageDownloader\bin\Debug\dates.txt";

            if (!String.Empty.Equals(dateString) && !String.Empty.Equals(dateListFilePath))
            {
                Console.WriteLine("\nERROR: /datelist and /date parameters cannot be used in a single command.", api);
                return;
            }

            if (String.Empty.Equals(folder))
            {
                folder = Environment.CurrentDirectory;
            }

            //folder = @"C:\Users\Mitko\Downloads";

            DateTime date = DateTime.Today;

            if (!String.Empty.Equals(dateListFilePath))
            {
                if (File.Exists(dateListFilePath))
                {
                    string[] fileLines = File.ReadAllLines(dateListFilePath);

                    foreach (string line in fileLines)
                    {
                        if(DateTime.TryParse(line, out date))
                        {
                            DownloadImage(api, date, folder);
                        }
                        else
                        {
                            try
                            {
                                CultureInfo provider = CultureInfo.InvariantCulture;
                                date = DateTime.ParseExact(line, "MM/dd/yy", provider);
                            }
                            catch
                            {
                                Console.WriteLine("\nERROR: The string \"{0}\" has invalid date format. Skipping line \"{0}\".", line);
                                continue;
                            }
                            DownloadImage(api, date, folder);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nERROR: The file \"{0}\" does not exist.", dateListFilePath);
                }
            }
            else
            {
                if (!String.Empty.Equals(dateString))
                {
                    if (!DateTime.TryParse(dateString, out date))
                    {
                        date = DateTime.Today;
                    }
                }

                DownloadImage(api, date, folder);
            }
        }

        private static void DownloadImage(string api, DateTime date, string folder)
        {
            if ("APOD".Equals(api.ToUpper()))
            {
                NasaApiGateway.NasaApodConsumer consumer = new NasaApiGateway.NasaApodConsumer();
                byte[] imageData;
                Apod apod;

                consumer.RetrieveImageContent(date, out apod, out imageData);

                Stream stream = new MemoryStream(imageData);
                Image image = Image.FromStream(stream);
                FileStream fileStream = new FileStream(Path.Combine(folder, String.Format("APOD-Image-{0}.jpg", apod.date)), FileMode.Create);

                Console.WriteLine(String.Format("\nDownloading Image using:\nAPI: {0}\nFor Date: {1}\nIn Folder: {2}", api, date.ToString("yyyy-MM-dd"), folder));
                image.Save(fileStream, ImageFormat.Jpeg);
            }
            else
            {
                Console.WriteLine("\nERROR: API \"{0}\" is not supported.", api);
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine(@"Ussage: NasaImageDownloader /api:apod /date:2017-02-01 /folder:C:\Downloads");
            Console.WriteLine(@"Ussage: NasaImageDownloader /api:apod /datelist:dates.txt /folder:C:\Downloads");
        }
    }
}
