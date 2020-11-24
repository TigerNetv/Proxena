using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Drawing;
using Console = Colorful.Console;

namespace Proxena
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Title = "Proxena 0.01 | Made by TigerNet [Ariel Shabaev]";


            Console.WriteLine("");
            Console.WriteLine("██████╗ ██████╗  ██████╗ ██╗  ██╗███████╗███╗   ██╗ █████╗ ", ColorTranslator.FromHtml("#1E90FF"));
            Console.WriteLine("██╔══██╗██╔══██╗██╔═══██╗╚██╗██╔╝██╔════╝████╗  ██║██╔══██╗", ColorTranslator.FromHtml("#1E90FF"));
            Console.WriteLine("██████╔╝██████╔╝██║   ██║ ╚███╔╝ █████╗  ██╔██╗ ██║███████║", ColorTranslator.FromHtml("#1E90FF"));
            Console.WriteLine("██╔═══╝ ██╔══██╗██║   ██║ ██╔██╗ ██╔══╝  ██║╚██╗██║██╔══██║", ColorTranslator.FromHtml("#1E90FF"));
            Console.WriteLine("██║     ██║  ██║╚██████╔╝██╔╝ ██╗███████╗██║ ╚████║██║  ██║", ColorTranslator.FromHtml("#1E90FF"));
            Console.WriteLine("╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝", ColorTranslator.FromHtml("#1E90FF"));


            Console.WriteLine("v0.01 | Author : TigerNet [Ariel Shabaev]", ColorTranslator.FromHtml("#1E90FF"));
            Console.WriteLine("");


            var proxies = File.ReadAllLines("proxies.txt");
            var threads = 50;
            var requestTimeout = 5 * 1000; // 5 Seconds
            var proxyTimeout = 2 * 1000; // 2 Seconds
            var proxyType = ProxyType.Socks4;
            var good = 0;



            Parallel.ForEach(proxies, new ParallelOptions() {
                MaxDegreeOfParallelism = threads }, proxy =>
            {
                try
                {
                    using (var request = new HttpRequest())
                    {
                        request.ConnectTimeout = requestTimeout;
                        request.Proxy = ProxyClient.Parse(proxyType, proxy);
                        request.Proxy.ConnectTimeout = proxyTimeout;


                        request.Get("https://azenv.net/");
                        Console.WriteLine("[GOOD PROXY]" + proxy, ColorTranslator.FromHtml("#009900"));
                        

                        Interlocked.Increment(ref good);
                        File.AppendAllText("good.txt", proxy + Environment.NewLine);
                    }
                }
                catch { Console.WriteLine("[BAD PROXY]" + proxy, ColorTranslator.FromHtml("#FF0000")); }
            });

            Console.WriteLine("Done!\n\n" + good + " good proxies out of " + proxies);

        }
    }
}
