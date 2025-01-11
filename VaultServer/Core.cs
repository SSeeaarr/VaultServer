using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VaultServer
{
    internal class Core
    {
        public static async Task Main()
        {

            // Initialization Tasks
            (string initializeip, int initializeport, bool autostart) = readdata.read(); //read from json file
            int port = initializeport; //assign to vars
            IPAddress ip = IPAddress.Parse(initializeip);
            //

            if (autostart == false)
            {
                menu.MainMenu(); //enter main menu
            }
            else if (autostart == true)
            {
                
                await AutoruncountWarning();
                
            }

            


            try
            {

                bool isconnected = false;
                var cancellationTokenSource = new CancellationTokenSource();
                bool firstconneciton = true;

                TcpListener server = new TcpListener(ip, port);
                server.Start();

                string saveDir = @"C:\ServerFiles\";

               if (!Directory.Exists(saveDir))
               {
                    Directory.CreateDirectory(saveDir);
               }

                var connectionstatus = Connectionstatus(cancellationTokenSource.Token, () => isconnected);

                while (true)
                {
                    
                    TcpClient stream = await server.AcceptTcpClientAsync();
                    isconnected = stream.Connected;

                    if (isconnected)
                    {
                        if (firstconneciton)
                        {
                            cancellationTokenSource.Cancel();
                            Console.Clear();
                            Console.WriteLine("Connected to client!");
                        }
                        firstconneciton = false;
                        await Handlefiles.Receive(saveDir, stream);
                    }
                    
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong: {e.Message} ");
                System.Diagnostics.Debug.WriteLine($"Something went wrong: {e.Message} ");
            }
        }

        private static async Task Connectionstatus(CancellationToken cancellationToken, Func<bool> isconnected)
        {
            char[] chars = { '/', '-', '\\' ,'|' };
            int i = 0;
            Console.Clear();

            while (!isconnected() && !cancellationToken.IsCancellationRequested)
            {
                Console.Write("\rWaiting for a connection... " + chars[i]);
                i = (i + 1) % chars.Length;
                await Task.Delay(200);
                
            }

        }

        private static async Task AutoruncountWarning()
        {
            var seconds = 5;
            while (seconds > 0)
            {
                Thread.Sleep(1000);
                seconds--;
                Console.Write("\rAutostart in: " + seconds + " seconds. Press esc to enter main menu.");
                
            }

        }

        private static async Task ReadInput(CancellationToken endtask)
        {
            var endautostart = new CancellationTokenSource();
            ConsoleKeyInfo cki = Console.ReadKey();
            string convert = cki.Key.ToString();
            System.Diagnostics.Debug.WriteLine(convert);
            if (convert == "Escape")
            {
                
            }
        }


    }
}
