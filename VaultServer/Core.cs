using System;
using System.Collections.Generic;
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

        
        
            try
            { 
                int port = 4999;
                var ip = IPAddress.Parse("10.0.0.6");
                bool isconnected = false;
                var cancellationTokenSource = new CancellationTokenSource();


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
                        cancellationTokenSource.Cancel();
                        Console.Clear();
                        Console.WriteLine("Connected to client!");
                        await Handlefiles.Recieve(saveDir, stream);
                    }
                }


            }
            catch (SocketException e)
            {
                Console.WriteLine("Something went wrong: {e.Message} ");
            }
        }

        private static async Task Connectionstatus(CancellationToken cancellationToken, Func<bool> isconnected)
        {
            char[] chars = { '/', '-', '|' };
            int i = 0;

            while (!isconnected() && !cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Waiting for a connection... " + chars[i]);
                i = (i + 1) % chars.Length;
                await Task.Delay(500);
                Console.Clear();
            }

        }


    }
}
