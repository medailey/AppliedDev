using System;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Game
    {
        public static COORD User { get; set; }
        static Map map1 = new Map();
        public static char[,] theMap = Map.ReadMap();
        static bool quit = false;
        public static GameCharacter mainCharacter;       
        

        public Game()        
        {           
                test();         
        }

        public struct COORD
        {
            public int X; public int Y;
            public COORD(int X, int Y)
            { this.X = X; this.Y = Y; }
        };

        static void GameLoop()
        {   
                Console.Clear();
                Map.ReadMap();
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
                User = new COORD()
                {
                    X = 1,
                    Y = 26
                };
                characterMenu();
                Console.SetCursorPosition(User.X, User.Y);
                Console.WriteLine("@");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            {
                                moveCharacter(0, 1);
                                break;
                            }
                        case ConsoleKey.RightArrow:
                            {
                                moveCharacter(1, 0);
                                break;
                            }
                        case ConsoleKey.UpArrow:
                            {
                                moveCharacter(0, -1);
                                break;
                            }
                        case ConsoleKey.LeftArrow:
                            {
                                moveCharacter(-1, 0);
                                break;
                            }
                    }
                }
            }
        
       
        static void moveCharacter(int x, int y)
        {
            characterMenu();
            COORD newPos = new COORD()
              {
                  X = User.X + x,
                  Y = User.Y + y
              };
            int val1 = newPos.X;
            int val2 = (newPos.Y-25);
            char currChar = theMap[val2, val1];
            
            if (canMove(newPos))
            {
                clearCharacter();
                Console.SetCursorPosition(newPos.X, newPos.Y);
                Console.WriteLine("@");
                User = newPos;
                characterMenu();
            }
            else
            {
                newPos = User;
                characterMenu();
            }
        }

        public static void clearCharacter()
        {
            Console.SetCursorPosition(User.X, User.Y);
            Console.WriteLine(".");
        }
        public static Boolean canMove(COORD posit)
        {    
            if(posit.X >0)
            {
                char c = theMap[posit.Y-25,posit.X];
                if(c.CompareTo('.')==0)
                {
                return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }  


        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("{0} hit, quitting...", e.SpecialKey);
            quit = true;
            e.Cancel = true;
        }

        static void characterMenu()
        {
            Console.SetCursorPosition(0, 0);            
            Console.Write("Name: MIKE\tHealth "+ mainCharacter.currentHealth);            
        }

        public void test()
        {
            Console.Clear();
            CreateCharacter cc = new CreateCharacter();
            cc.getCharacterName();
            Console.Clear();
            string name = Console.ReadLine();
            GameCharacter character1 = new GameCharacter(name, 10, 10, 10, 10, 10);
            mainCharacter = character1;
            GameLoop();
        }

        public void battle()
        {
            bool battleFinished = false;
            int mobHealth = 100;
            int mobStr = 6;
            Random rand = new Random();
            int currentHealth = mainCharacter.currentHealth;

            while (battleFinished == false)
            {
                Console.SetCursorPosition(0, 5);
                int damage = 0;
                damage = rand.Next(mobStr * 10);
                Console.WriteLine("Monster does " + damage + " to YOU!");
                currentHealth = currentHealth - damage;                
            }
            mainCharacter.currentHealth = currentHealth;
        }
    }
}

