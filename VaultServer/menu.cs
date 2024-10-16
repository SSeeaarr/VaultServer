using System;
using System.Collections.Generic;
using System.Linq;
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

                string selection = Console.ReadLine();

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
        }

        private static void OptionsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("\n1. Back");

                
                string option = Console.ReadLine();
                int parseoption = int.Parse(option);

                if (parseoption == 1) //return to main menu
                {
                    break;
                }
            }
        }

    }
}
