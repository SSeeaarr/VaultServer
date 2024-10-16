using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VaultServer
{
    internal class Handlefiles
    {
        public static async Task Recieve(string path, TcpClient stream)
        {

            try
            {
                NetworkStream ns = stream.GetStream();
                {
                    byte[] sigma = new byte[4096];
                    
                    NetworkStream namestream = stream.GetStream();
                     var name = namestream.Read(sigma, 0, sigma.Length);
                     Console.WriteLine("name is: " + name);
                    
                        

                        string filename = "ohio.jpg";

                    using (FileStream fs = new FileStream(path + filename, FileMode.Create))
                    {
                        byte[] buffer = new byte[4096];
                        int readbytes;

                        while ((readbytes = ns.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, readbytes);
                        }
                        
                        Console.WriteLine("File saved.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured during the recieve process.");
            }

        }
        
        
        
    }
}
