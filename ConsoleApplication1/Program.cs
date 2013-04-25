using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogansDemise
{
    class Program                                                                               //main program used to run the game
    {
        static bool endGame = false;
        static bool fileLoaded = false;
        static  bool characterFin = false;
        static void Main(string[] args)                                                         //main function
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
                                                        //set the buffer and window size and position
            System.Console.SetBufferSize(130, 70);
            System.Console.SetWindowSize(130, 70);
 
            titleScreen();
            Console.SetCursorPosition(25, 35);
            Console.WriteLine("Press Any Key to Continue");
            while (!Console.KeyAvailable)
            {

            }
                                                                                                     //bool variable to keep game running
            while(endGame != true)                                                                   //while loop to keep the mainloop running
            {
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

                Console.CursorVisible = false;                                                     //turn off the cursor in the console

                System.Console.SetWindowPosition(0, 0);
                
                Console.Clear();
                MainLoop();                                                                     //start main loop
                Console.Clear();
                characterFin = false;
                fileLoaded = false;

            }//end while
        }//end main function

        /// <summary>
        /// MainLoop function used to create the initial menu for the game
        /// </summary>
        static void MainLoop()
        {
           
            Console.SetCursorPosition(25, 20);
            Console.WriteLine("MENU SELECTION:");
                       titleScreen();
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
                            loadMenu();
                            //load game menu
                            
                            break;
                        }
                    case 51:
                        {
                            helpMenu();
                            break;
                        }
                    case 52:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }//end switch

            } //end mainloop

        public Program()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path = path + "\\Saves\\";
            System.IO.Directory.CreateDirectory(path);
        }
        /// <summary>
        /// Loadmenu function used to find save game files and display them.  Then let the user select which to load if any
        /// </summary>
        public static void loadMenu()
        {
            
            Console.Clear();
            titleScreen();
            while (fileLoaded != true)
            {
                List<string> fileNames = new List<string>();

                DirectoryInfo dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Saves\\"); //get the directory where the exe was ran from
                if (Directory.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Saves\\"))                    //look for the saves directory
                {
                    Console.SetCursorPosition(25, 20);
                    Console.Write("Please Select Save To Load: ");
                    var files = dir.GetFiles(); //get the files from the directory
                    if (files.Length > 0)   //test if there are files
                    {
                        //display files if there are any
                        for (int i = 0; i < files.Length; i++)
                        {
                            FileInfo f = files[i];
                            Console.SetCursorPosition(25, 21);
                            Console.Write((i + 1) + ": " + f.Name);
                            fileNames.Add(f.FullName);
                        } //end for
                        Console.SetCursorPosition(52, 20);

                        Console.ReadLine();
                        string fileNum = Console.ReadLine();
                        int fileNums; 
                        bool tryparse = Int32.TryParse(fileNum, out fileNums);
                        if (tryparse)
                        {

                            if (fileNums > (files.Length)) //test if user entered a valid save game
                            {
                                Console.SetCursorPosition(25, 19);
                                Console.Write("Invalid Selection. Try Again.");
                            } //end if
                            else if (fileNums >= 0) //if user did select a valid number load the file and deserialize it
                            {
                                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(GameCharacter));
                                GameCharacter gm = new GameCharacter();
                                using (StreamReader file = new StreamReader(fileNames[fileNums - 1]))
                                {

                                    gm = (GameCharacter)reader.Deserialize(file);

                                } Game startGame = new Game(gm, 0); //load the game with the deserialized file
                                fileLoaded = true; //mark the file loaded as true to end while loop when it returns
                            } //end else if
                        }
                        else
                        {
                            Console.SetCursorPosition(25, 19);
                            Console.Write("Invalid Selection. Try Again.");

                        }

                    } //end if

                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(25, 19);
                    Console.Write("No Saves Available. Press Any Key to Return to Main Menu");
                    while (!Console.KeyAvailable)
                    {

                    }
                    fileLoaded = true;
                } //end else//end if
                
            } //end while
        } //end function loadMenu 


        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.Write("{0} hit, quitting...", e.SpecialKey);
           
            e.Cancel = true;
        } //end console_cancelkeypress

        static void MainMenu()
        {
            Console.SetCursorPosition(25, 21);
            Console.Write("Please Select An Option:");
            Console.SetCursorPosition(25, 22);
            Console.Write("(1) Start New Game");
            Console.SetCursorPosition(25, 23);
            Console.Write("(2) Load Saved Game");
            Console.SetCursorPosition(25, 24);
            Console.Write("(3) Help Menu");
            Console.SetCursorPosition(25, 25);
            Console.Write("(4) Exit");
            Console.SetCursorPosition(50, 20);                
        } //end MainMenu function

        /// <summary>
        /// character creation menu function used to display and info and create a character for game
        /// </summary>
        static void characterMenu()
        {
            
            Console.Clear();
            titleScreen();
            while (characterFin != true)
            {
                //get character name
                Console.SetCursorPosition(25, 20);
                Console.WriteLine("Character Creation:");
                Console.SetCursorPosition(25, 21);
                Console.WriteLine("Please  Enter Your Character Name:");
                Console.SetCursorPosition(25, 22);
                Console.ReadLine();
                string charName = Console.ReadLine();
                
                Console.Clear();
                titleScreen();
                bool selectClass = false;
                while (selectClass != true)
                {
                    //select a class
                    Console.SetCursorPosition(25, 20);
                    Console.Write("Hello " + charName);
                    Console.SetCursorPosition(25, 21);
                    Console.Write("Please Select A Class:        ");
                    Console.SetCursorPosition(25, 22);
                    Console.Write("(1) Fighter : Uses fists as weapons to defeat enemies and has extra endurance");
                    Console.SetCursorPosition(25, 23);
                    Console.Write("(2) Stalker : Has Ability to escape mid fight and surprise enemies");
                    Console.SetCursorPosition(25, 24);
                    Console.Write("(3) Sorcerer : Fights enemys with fire and can heal during battle");
                    Console.SetCursorPosition(50, 21);


                    int classSel = Console.Read();
                    GameCharacter user = new GameCharacter();
                    //based on class selected create a new game character

                    switch (classSel)
                    {
                        case 49:
                            {
                                //warrior class
                                user = new GameCharacter(charName, 1);
                                storyTime(user);  //pass user to story function
                                characterFin = true;//close the loop when it returns here
                                selectClass = true;
                                break;
                            }
                        case 50:
                            {
                                //stalker class
                                user = new GameCharacter(charName, 2);
                                storyTime(user);  //pass user to story function
                                characterFin = true;//close the loop when it returns here 
                                selectClass = true;
                                break;
                            }
                        case 51:
                            {
                                //sorceror class
                                user = new GameCharacter(charName, 3);
                                storyTime(user);  //pass user to story function
                                characterFin = true;//close the loop when it returns here  
                                selectClass = true;
                                break;
                            }
                        default:
                            {
                                //incorrect selection
                                Console.SetCursorPosition(25, 19);
                                Console.Write("You have entered an incorrect value try again");
                                break;
                            }
                    }//end switch
                }
                 
            }//end while loop
        }//end characterMenu function

        static void storyTime(GameCharacter user)       //story method to display the story of the game
        {
            bool exit = false;
            while (exit != true)
            {
                Console.Clear();                    //general story for all character classes
                int x = 2000;
                Console.Write("The Story So Far: (Press Spacebar Now to Skip to End)\n");
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    x = 0;
                }
                else
                {
                    Console.ReadKey();
                }

                Console.Write("You live on a quiet horse farm near the Kingdom of Logan with your trusty pooch Harlie.\n");
                System.Threading.Thread.Sleep(x);
                Console.Write("Yesterday was just like an ordinary day, the sun was out the wind blowing ever so slightly.\n");
                System.Threading.Thread.Sleep(x);
                Console.Write("As the sun was going down you noticed a bright green flash in the sky!  You were forced to cover your eyes to protect yourself.\n");
                System.Threading.Thread.Sleep(x);
                Console.Write("You thought nothing about this strangeness and continued on your merry way.\n");
                System.Threading.Thread.Sleep(x);
                Console.Write("When you woke up this morning something had changed... There was something different about the world.\n");
                System.Threading.Thread.Sleep(x);


                if (user.charCl == 1)  //fighter class
                {
                    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                    {
                        x = 0;
                    }
                    System.Threading.Thread.Sleep(x);
                    Console.Write("As you rose out of bed you noticed your sleeves and pant legs had been torn.  You thought to yourself that is very unsual.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You reached for the door knob and noticed your hand dwarfed the knob. You looked down at your hands and noticed they were twice\nthe size that you remember.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("Walking through your hallway into your kitchen your new found size makes you clumsy and\n you accidently banged your hand on the stone wall.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("Out of instinct you bring your hand up thinking about waiting for the pain to shoot through your hand,\nbut the strange thing you don't feel a thing.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You turn back and look at the stone wall and now notice your fist took out a chunk of rock from the wall.\nLooking down at your hands in amazement you punch the wall again.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("Low and behold another chunk of stone bigger than the first is smashed from the wall.  Before you have time to process the\ninformation you hear the whine of your horses outside.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You open your front door and step outside...\n");
                    System.Threading.Thread.Sleep(x);

                    Console.Write("Press any key to continue...(Or Escape to Exit)");
                    while (!Console.KeyAvailable)
                    {

                    }
                    Game theGame = new Game(user);
                    exit = true;
                }//end if
                if (user.charCl == 2) //stalker class
                {
                    
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You roll over out of bed and fall on the floor.  Instead of smashing your face against the floor you are able to\nland on your hands and feet.  Strange you think.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("'Normally I would have fallen on my face.' You shrug, stand up and start walking through your room into the hallway.\n You feel lighter on your feet than yesterday. ");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("'Must've been those crunches I've been doing.' You think to yourself.\nAs you step on the creaky board in your house it doesn't make a sound. You stop and step on it again and again still no noise.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You look down at the floor puzzled and walk away and as you do you run into a side table and causing a water glass\nto fall off of the table. ");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("Without thinking you snatch the glass out of the air and manage to catch all the water\nback into it before it hits the ground. ");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You walk over to your tools by your front door and lift up the your sickle and slasher.\nThe tools feel lighter, you swing the slasher in the air and it moves faster than you have ever seen before.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("\nBefore you can think twice you hear a crash outside.\nYou open your front door and step outside...\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("Press any key to continue...");
                    while (!Console.KeyAvailable)
                    {

                    }
                    Game theGame = new Game(user);
                    exit = true;
                }//end if
                if (user.charCl == 3)  //sorceror class
                {
                    
                    System.Threading.Thread.Sleep(x);
                    Console.Write("A crash or noise outside startles you awake.  The sun hasn't come out yet and the room is still pitch dark.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You think to yourself you could use some light and go to reach for the lantern next to your bed,\nas your hand moves closer to it, the fire of the lantern appears.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("The bright light of the fire shines into your eyes and you instinctively cover your eyes with your hand.  Strangely the light goes out as you do so.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You reach again slowly towards the lantern and the fire ignites again.\nThe closer your hand gets to the lantern the brighter the light gets.\nYou realize you can feel the flame as if you were molding it in your hand.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("Without fear you reach out and grab the handle of the lantern.\nThe lantern becomes engulfed in flames. You are amazed as the flames wrap around the handle of the lantern and your hand.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("You can feel no pain as the flames coil around your hand as if wearing a soft leather glove.\nYou move your other hand towards the flaming lantern and into the flames.\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("The fire forms a sphere in your free hand and floats in your palm as you pull it away from the lantern.\nAs you stare at the fire you hear another crash outside and remember why you were awake in the first place.\nYou open your front door and step outside...\n");
                    System.Threading.Thread.Sleep(x);
                    Console.Write("Press any key to continue...");
                    while (!Console.KeyAvailable)
                    {

                    }
                    Game theGame = new Game(user);
                    exit = true;

                } //end if

            }//end while
        }//end function


        public static void helpMenu()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Help File\n");
            Console.WriteLine("\tBasic Game Info:");
            Console.WriteLine("All characters have the same basic stats (strength, stamina, intellect, spirit, agility).");
            Console.WriteLine("Primary stat for Fighter - Strength,  Sorceror - Intellect, Stalker - Agility.\nHealing spells and magic are based off characters Intellect.  Magic Regenerates while moving based off of users spirit.");
            Console.WriteLine("Game does not auto save.");
            Console.WriteLine("\tOn Screen Symbols:");
            Console.WriteLine("@-User Character\n!-Enemy\n%-Boss\n.-Indicates movement allowed\n| and '-' indicate no movement allowed.\n/-Indicates door.\n$-Indicates body of enemy.");
            Console.WriteLine("\n\tControls:\nArrow Keys for Movement\nA-Engages Battle and Attack for All Classes while in Battle\nR-Healing Spell for All Classes\nE-Enter and Exit Doors\nEsc-Exits Game\nH-Saves Game");
            Console.WriteLine("\n\tClass Specific:\nV-Disengages combat with non-bosses for Stalker\nB-Backstabs enemies as Stalker. Can only be performed after engaged battle and once per battle as the first attack\nDoes 2X normal damage");
            Console.WriteLine("R-Healing for Sorceror during combat.  Only class to be able to heal during combat.  Does take turn during battle.");
            Console.WriteLine("\n\tClass Info:\nFighter has no special attack but has more stamina then the other classes.\nStalker can backstab after engaging in combat and escape non-boss enemies. Enemies do regenerate health however\nSorceror can heal during combat.");
            Console.WriteLine("\nPress Any Key to Return.");
            while (!Console.KeyAvailable)
            {

            }
        }//end function help menu
        
        public static void titleScreen()                             //function used to load file and read file
        {
            char[,]screen = new char[18,70];
               using (StreamReader sr = new StreamReader("..\\art\\titlescreen.txt"))
               {
                   for (int i = 0; i < 18; i++)
                   {
                       String line = sr.ReadLine();
                       
                       for (int j = 0; j < 70; j++)
                       {
                           if (j < line.Length)
                               screen[i, j] = (char)(line[j]);
                       }//end inner loop
                }//end outer loop
            }//end using
               int top = 0;
               int left = 0;
               Console.SetCursorPosition(left, top);
               for (int i = 0; i < 18; i++)
               {
                   for (int k = 0; k < 70; k++)
                   {
                       Console.Write(screen[i, k]);
                       
                   }
                   top++;
                   Console.SetCursorPosition(left, top);
               }

           }

    } //end class
}//end namespace
