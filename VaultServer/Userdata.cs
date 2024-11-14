using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace VaultServer
{
    internal class Userdata
    {
        public string IP { get; set; }
        public int Port { get; set; }

    }


    public class writedata
    {
        public static void Write(string ip = "0.0.0.0", int port = 0000)
        {

            var user = new Userdata
            {
                IP = ip,
                Port = port
            };
            

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(user, options);

            string defaultPath = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(defaultPath, "userdata.json");

            Directory.CreateDirectory(defaultPath);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(jsonString);
            }
        }

    }

    public class readdata
    {
        public static (string ip, int port) read()
        {
            
            string data;

            string defaultPath = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(defaultPath, "userdata.json");

            if (File.Exists(filePath)) {


                using (StreamReader ohio = new StreamReader(filePath))
                {
                    data = ohio.ReadToEnd();
                }

                var user = JsonSerializer.Deserialize<Userdata>(data);
                Console.WriteLine($"IP: {user.IP}, Port: {user.Port}");


                return (user.IP, user.Port);
            }
            else {
                Console.WriteLine("File does not exist! Creating new save.");

                Directory.CreateDirectory(defaultPath);
                using (File.Create(filePath)) { }

                    writedata.Write();

                string jdata = File.ReadAllText(filePath);
                var user = JsonSerializer.Deserialize<Userdata>(jdata);
                return (user.IP, user.Port);
            }

        }
    }
}





