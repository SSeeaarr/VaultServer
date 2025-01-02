using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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

                    byte[] filesize = new byte[8];
                    int sizebytesread = ns.Read(filesize, 0, filesize.Length);
                    long filesizenum = BitConverter.ToInt64(filesize, 0);


                    //int finalsize = BitConverter.ToInt32(filesize, 0);
                   // Console.WriteLine(finalsize);


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
                        int bufferSize = filesizenum < 10 * 1024 * 1024 ? 4096 : 8192;
                        byte[] buffer = new byte[bufferSize];
                        int readbytes;
                        int currentprogress = 0;

                        while ((readbytes = ns.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, readbytes);
                            currentprogress += readbytes;
                            await DisplayStatus(currentprogress, filesizenum);
                        }
                        
                        Console.WriteLine("\nFile: " + filename  + " was saved.");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured during the recieve process.");
            }

        }

        public static async Task DisplayStatus(int current, long filesize)
        {

            if (current < filesize)
            {
                var status = (double)current / filesize * 100;
                var rounded = Math.Round(status);
                Console.Write("\rDownloading: " + rounded + "% ");
            }

        }




    }
}
