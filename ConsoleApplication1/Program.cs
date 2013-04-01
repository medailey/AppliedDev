using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {

        static bool quit = false;
       
        static void Main(string[] args)
        {
            
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            Console.CursorVisible = false;

            System.Console.SetWindowPosition(0, 0);
            System.Console.SetBufferSize(130, 70);
            System.Console.SetWindowSize(130, 70);
            

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            MainLoop();
        }

        static void MainLoop()
        {      
            Console.WriteLine("MENU SELECTION:");
            MainMenu();
            int menuSelection = Console.Read();

            

            switch (menuSelection)
            {
                case 49:
                    {
                        Game newGame = new Game(); 
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        break;
                    }
            }

                       
        }


       

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("{0} hit, quitting...", e.SpecialKey);
            quit = true;
            e.Cancel = true;
        }

        static void MainMenu()
        {
            Console.SetCursorPosition(25, 15);
            Console.WriteLine("Please Select An Option:");
            Console.WriteLine("(1) Start New Game");
            Console.WriteLine("(2) Load Saved Game");
            Console.WriteLine("(3) Help Menu");
        }


    }


}
