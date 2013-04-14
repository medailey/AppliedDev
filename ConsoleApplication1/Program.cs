using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program                                                                               //main program used to run the game
    {

        

        static void Main(string[] args)                                                         //main function
        {

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);            

            Console.CursorVisible = false;                                                      //turn off the cursor in the console

            System.Console.SetWindowPosition(0, 0);                                             //set the buffer and window size and position
            System.Console.SetBufferSize(130, 70);
            System.Console.SetWindowSize(130, 70);


            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            MainLoop();
        }

        static void MainLoop()
        {           
                Console.SetCursorPosition(25, 14);
                Console.WriteLine("MENU SELECTION:");
                MainMenu();
                int menuSelection = Console.Read();


            
                switch (menuSelection)
                {
                    case 49:
                        {
                            characterMenu();
                            //Game newGame = new Game(); 
                            
            
                break;
            }
                    case 50:
                        {
                            break;
                        }
                    case 51:
                        {
                            break;
                        }
                }

            
        }




        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.Write("{0} hit, quitting...", e.SpecialKey);
           
            e.Cancel = true;
        }

        static void MainMenu()
        {
            Console.SetCursorPosition(25, 15);
            Console.Write("Please Select An Option:");
            Console.SetCursorPosition(25, 16);
            Console.Write("(1) Start New Game");
            Console.SetCursorPosition(25, 17);
            Console.Write("(2) Load Saved Game");
            Console.SetCursorPosition(25, 18);
            Console.Write("(3) Help Menu");
            Console.SetCursorPosition(50, 15);
                
        }

        static void characterMenu()
        {
            MainLoop();
            Console.Clear();
            bool characterFin = false;
            while (characterFin != true)
            {
                Console.SetCursorPosition(25, 14);
                Console.WriteLine("Character Creation:");
                Console.SetCursorPosition(25, 15);
                Console.WriteLine("Please  Enter Your Character Name:");
                Console.SetCursorPosition(25, 16);
                Console.ReadLine();
                string charName = Console.ReadLine();


                Console.Clear();

                Console.SetCursorPosition(25, 14);
                Console.Write("Hello " + charName);
                Console.SetCursorPosition(25, 15);
                Console.Write("Please Select A Class:");
                Console.SetCursorPosition(25, 16);
                Console.Write("(1) Fighter : Uses fists as weapons to defeat enemies");
                Console.SetCursorPosition(25, 17);
                Console.Write("(2) Stalker : Has Ability to escape mid fight and surprise enemies");
                Console.SetCursorPosition(25, 18);
                Console.Write("(3) Sorcerer : Fights enemys with fire and water");
                Console.SetCursorPosition(50, 15);

                
                int classSel = Console.Read();
               GameCharacter user = new GameCharacter();
                switch (classSel)
                {
                    case 49:
                        {
                            user = new GameCharacter(charName, 1);
                            
                            //Game newGame = new Game(); 
                            break;
                        }
                    case 50:                    
                        {
                             user = new GameCharacter(charName, 2);
                            
                            break;
                        }
                    case 51:
                        {
                           user = new GameCharacter(charName, 3);

                            break;
                        }
                    default:
                        {
                            Console.Write("You have entered an incorrect value try again");
                            break;
                        }
                }

                //storyTime(user);
                characterFin = true;
                Game theGame = new Game(user);
                MainLoop();



                // Console.WriteLine("(1) Start New Game");
                //Console.SetCursorPosition(25, 17);
                //Console.WriteLine("(2) Load Saved Game");
                //Console.SetCursorPosition(25, 18);
                //Console.WriteLine("(3) Help Menu");

            }


        }

        static void storyTime(GameCharacter user)
        {
            Console.Clear();
            Console.Write("The Story So Far:\n");
            Console.Write("You live on a quiet horse farm near the Kingdom of Logan with your trusty pooch Harlie.\n");
            System.Threading.Thread.Sleep(2000);
            Console.Write("Yesterday was just like an ordinary day, the sun was out the wind blowing ever so slightly.\n");
            System.Threading.Thread.Sleep(2000);
            Console.Write("As the sun was going down you noticed a bright green flash in the sky!  You were forced to cover your eyes to protect yourself.\n");
            System.Threading.Thread.Sleep(2000);
            Console.Write("You thought nothing about this strangeness and continued on your merry way.\n");
            System.Threading.Thread.Sleep(2000);
            Console.Write("When you woke up this morning something had changed... There was something different about the world.\n");
            System.Threading.Thread.Sleep(2000);

            if (user.charCl == 1)
            {
                System.Threading.Thread.Sleep(2000);
                Console.Write("As you rose out of bed you noticed your sleeves and pant legs had been torn.  You thought to yourself that is very unsual.\n");
                System.Threading.Thread.Sleep(2000);
                Console.Write("You reached for the door knob and noticed your hand dwarfed the knob. You looked down at your hands and noticed they were twice\n the size that you remember.\n");
                System.Threading.Thread.Sleep(2000);
                Console.Write("Walking through your hallway into your kitchen your new found size makes you clumsy and you accidently banged your hand on the stone wall.\n");
                System.Threading.Thread.Sleep(2000);
                Console.Write("Out of instinct you bring your hand up thinking about how much it is going to hurt, but the strange thing you don't feel a thing.\n");
                System.Threading.Thread.Sleep(2000);
                Console.Write("You turn back and look at the stone wall and now notice your fist took out a chunk of rock from the wall.  Looking down at your hands in amazement you punch the wall again.\n");
                System.Threading.Thread.Sleep(2000);
                Console.Write("Low and behold another chunk of stone bigger than the first is smashed from the wall.  Before you have time to process the information you hear the whine of your horses outside.\n");
                System.Threading.Thread.Sleep(2000);
                Console.Write("You open your front door and step outside...\n"); 
                System.Threading.Thread.Sleep(2000);

                Console.Write("Press any key to continue...");
                while (!Console.KeyAvailable)
                {

                }
                Game theGame = new Game(user);

            }
            if (user.charCl == 1)
            {
                Console.Write("As you rose out of bed you noticed tears in your sleeves and pant legs.  You thought to yourself that is very unsual.\n");
                Console.Write("You reached for the door knob and noticed your hand dwarfed the knob. You looked down at your hands and noticed they were twice the size that you remember.\n");
                Console.Write("Walking through your hallway into your kitchen your new found size makes you clumsy and you accidently banged your hand on the stone wall.\n");
                Console.Write("Out of instinct you bring your hand up thinking about how much it is going to hurt, but the strange thing you don't feel a thing.\n");
                Console.Write("You turn back and look at the stone wall and now notice your fist took out a chunk of rock from the wall.  Looking down at your hands in amazement you punch the wall again.\n");
                Console.Write("Low and behold another chunk of stone bigger than the first is smashed from the wall.  Before you have time to process the information you hear the whine of your horses outside.\n");
                Console.Write("You open your front door and step outside...\n");
                Game theGame = new Game(user);

            }
            if (user.charCl == 1)
            {
                Console.Write("As you rose out of bed you noticed tears in your sleeves and pant legs.  You thought to yourself that is very unsual.\n");
                Console.Write("You reached for the door knob and noticed your hand dwarfed the knob. You looked down at your hands and noticed they were twice the size that you remember.\n");
                Console.Write("Walking through your hallway into your kitchen your new found size makes you clumsy and you accidently banged your hand on the stone wall.\n");
                Console.Write("Out of instinct you bring your hand up thinking about how much it is going to hurt, but the strange thing you don't feel a thing.\n");
                Console.Write("You turn back and look at the stone wall and now notice your fist took out a chunk of rock from the wall.  Looking down at your hands in amazement you punch the wall again.\n");
                Console.Write("Low and behold another chunk of stone bigger than the first is smashed from the wall.  Before you have time to process the information you hear the whine of your horses outside.\n");
                Console.Write("You open your front door and step outside...\n");
                Game theGame = new Game(user);

            }

           
        }

    }
}
