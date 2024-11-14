using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VaultServer
{
    internal class menu
    {
        public static void MainMenu() {

            while (true)
            {
                
                Console.Clear();
                Console.WriteLine("Welcome to vault server.");

                Console.WriteLine("\nChoose from the following options:");
                Console.WriteLine("");
                Console.WriteLine("\n1. Start Server");
                Console.WriteLine("\n2. Options");
                Console.WriteLine("\n3. Quit");

                var selection = Console.ReadLine();

                bool verifyint = Int32.TryParse(selection, out _);

                if (selection != null && verifyint)
                {
                    var selectionparse = int.Parse(selection);
                    if (selectionparse == 1) //start
                    {
                        break;
                    }
                    else if (selectionparse == 2) //options
                    {
                        OptionsMenu();
                    }
                    else if (selectionparse == 3) //quit
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Please choose a valid option.");
                }
            }
        }

        private static void OptionsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n1. Change IP");
                Console.WriteLine("\n2. Change Port");
                Console.WriteLine("\n3. Back");
                Console.WriteLine("\n0. Display Current IP and Port");


                string option = Console.ReadLine();
                bool verifyint = Int32.TryParse(option, out _);

                if (option != null && verifyint)
                {
                    
                    int parseoption = int.Parse(option);

                    if (parseoption == 1)
                    {
                        IPChange();
                    }
                    else if (parseoption == 2)
                    {
                        PortChange();
                    }
                    else if (parseoption == 3) //return to main menu
                    {
                        break;
                    }
                    else if (parseoption == 0)
                    {
                        DisplayInfo();
                    }
                }
            }
        }

        private static void IPChange()
        {
            Console.Clear();
            Console.WriteLine("Please enter the IP you would like to use.");
            var ip = Console.ReadLine();


            
            bool verifyip = IPAddress.TryParse(ip, out _);
            if ((ip != null && verifyip))
            {
                    (string unchangedip, int existingport) = readdata.read(); //grab port and pass through to write function since -
                    writedata.Write(ip, existingport); // - we dont want to overwrite existing port. Vice versa for portchange func.
                    Console.Write("IP: " + ip + "Successfuly changed");
            }
            else
            {
                    Console.WriteLine("Please enter a valid IP.");
                    Console.ReadLine();
            }
            
        }

        private static void PortChange()
        { 
            Console.Clear();
            Console.WriteLine("Please enter the Port you would like to use.");
            var port = Console.ReadLine();

            bool verifyint = Int32.TryParse(port, out _);
            if (port != null && verifyint)
            {
                var input = int.Parse(port);
                (string existingip, int unchangedport) = readdata.read(); //grab ip and pass through to write function
                writedata.Write(existingip, input);
                Console.Write("Port: " + port + "Successfuly changed");
            }
        }

        private static void DisplayInfo() {
            Console.Clear();
            (string initializeip, int initializeport) = readdata.read();
            Console.WriteLine("\nPress enter to continue.");
            Console.ReadLine();
        }

    }
}
