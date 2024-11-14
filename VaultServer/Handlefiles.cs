using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VaultServer
{
    internal class Handlefiles
    {
        public static async Task Receive(string path, TcpClient stream) //dumbass visual studio
        {

            try
            {
                NetworkStream ns = stream.GetStream();
                {

                    // Read the first 4 bytes to get the filename length
                    byte[] lengthBytes = new byte[4];
                    ns.Read(lengthBytes, 0, lengthBytes.Length);
                    int filenameLength = BitConverter.ToInt32(lengthBytes, 0);

                    // Now read the filename with the exact length
                    byte[] filenameBytes = new byte[filenameLength];
                    ns.Read(filenameBytes, 0, filenameLength);
                    string filename = Encoding.UTF8.GetString(filenameBytes);


                    // Recieve actual byte data of the file
                    using (FileStream fs = new FileStream(path + filename, FileMode.Create)) 
                    {
                        byte[] buffer = new byte[4096];
                        int readbytes;

                        while ((readbytes = ns.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, readbytes);
                        }
                        
                        Console.WriteLine("File: " + filename  + " was saved.");
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
