using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication1
{
    class Game
    {
        public static COORD User { get; set; }
        static Map map1 = new Map();
        public static char[,] theMap = Map.ReadMap();
        static bool quit = false;
        public static GameCharacter mainCharacter = new  GameCharacter("Michael",10,10,10,10,10,0,1);       
        public static Queue<string> themessages = new Queue<string>();
        public static Queue<string> combatMessages = new Queue<string>();
        public static bool enemyNear;
        public static COORD enemyPosit { get; set; }
        public static NPChar currentEnemy;

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
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
                enemyNear = false;    
            User = new COORD()
                {
                    X = 1,
                    Y = 26
                };
                //characterMenu();
                Console.SetCursorPosition(User.X, User.Y);
                Console.WriteLine("@");

                ConsoleKeyInfo keyInfo;
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
                        case ConsoleKey.A:
                            {
                                if(enemyNear ==true)
                                {
                                    
                                    battle(mainCharacter, currentEnemy);

                                }
                                break;
                            }
                        case ConsoleKey.R:
                            {
                                healingSpell();
                                break;
                            }

                    }
                    characterMenu();
                }
            }
        
       
        static void moveCharacter(int x, int y)
        {
            //characterMenu();
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
               clearCharacter(newPos);
                Console.SetCursorPosition(newPos.X, newPos.Y);
                Console.WriteLine("@");
                User = newPos;                
                //characterMenu();
            }
            else
            {
                Console.SetCursorPosition(newPos.X,newPos.Y);
                Console.WriteLine(theMap[newPos.Y - 25, newPos.X]);                
                newPos = User;
               // characterMenu();
            }
        }

        public static void clearCharacter(COORD newPos)
        {
            Console.SetCursorPosition(newPos.X - 1, newPos.Y);
            Console.WriteLine(theMap[newPos.Y - 25, newPos.X - 1]);

            Console.SetCursorPosition(newPos.X + 1, newPos.Y);
            Console.WriteLine(theMap[newPos.Y - 25, newPos.X + 1]);

            Console.SetCursorPosition(newPos.X, newPos.Y + 1);
            Console.WriteLine(theMap[newPos.Y - 24, newPos.X]);

            Console.SetCursorPosition(newPos.X, newPos.Y - 1);
            Console.WriteLine(theMap[newPos.Y - 26, newPos.X]);

            Console.SetCursorPosition(User.X, User.Y);
            Console.WriteLine(theMap[User.Y-25,User.X]);
        }
        public static Boolean canMove(COORD posit)
        {    
            if(posit.X >0)
            {
                char c = theMap[posit.Y-25,posit.X];

                switch (c)
                {
                    case '.':
                        {
                            writeMessages("You move through the dungeon");
                            return true;
                        }
                    case '|':
                        {
                           
                            writeMessages("You have encountered a rock wall.");
                            return false;
                        }
                    case '-':
                        {
                            
                            writeMessages("There is a wall blocking your path.");
                            return false;
                        }
                    case '!':
                        {
                            writeMessages("YOU HAVE ENCOUNTERED AN ANGRY UNICORN!");
                            enemyPosit = posit;
                            getNPC();
                            enemyNear = true;
                            return false;
                        }

                    case '$':
                        {
                            writeMessages("You stand over your enemies body.");
                            return false;
                        }


                    default:
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
            Console.Write("Name: Nameless Hero\tLevel: " + mainCharacter.level + " Warrior" +  "\nHP: " +mainCharacter.currentHealth + "/" + mainCharacter.maxHealth + "\tMagic: " + mainCharacter.magic +  
                "\tXP: "+mainCharacter.exp +"\nSTR: " +mainCharacter.strength + "\tSTA: " + mainCharacter.stamina + "\tSPI: " + mainCharacter.spirit + "\nINT: " + mainCharacter.intellect + "\tAGI: " + mainCharacter.agility);

            Console.SetCursorPosition(0, 68);
            Console.Write("Controls:  Arrow Keys For Movement");
            Console.SetCursorPosition(0, 69);
            Console.Write("(a) To attack a monster with primary attack\t(r) To cast healing spell\t(e) To interact with objects");
            Console.SetCursorPosition(0, 0);
        }

        public void test()
        {
            //Console.Clear();
            //CreateCharacter cc = new CreateCharacter();
            //cc.getCharacterName();
            //Console.Clear();
            //string name = Console.ReadLine();
            //GameCharacter character1 = new GameCharacter(name, 10, 10, 10, 10, 10,0,1);
            //mainCharacter = character1;
            GameLoop();
        }

        public static bool battle(GameCharacter user, NPChar mob)
        {
            bool battleFinished = false;

            
            int top = 0; 
            int left = 50;
            drawEnemy enemy = new drawEnemy("..\\art\\unicorn.txt");
            char[,] pic =new char[8,35];
            pic = enemy.enemy;
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < 8; i++)
            {
                for(int k = 0; k < 35; k++)
                {
                    Console.Write(pic[i,k]);

                }
                top++;
                Console.SetCursorPosition(left, top);

            }

            Console.SetCursorPosition(80, 0);
            Console.WriteLine("Enemy: " + mob.name + " Health: " + mob.currentHealth + " ");
            if (battleFinished == false && mob.currentHealth>0)
            {
                Random rand = new Random();
                int currentHealth = mainCharacter.currentHealth;
                int damage;

                Console.SetCursorPosition(0, 5);
                combatDialogue("Begin Battle!");
                damage = rand.Next(mob.strength);
                combatDialogue(mob.name + " does " + damage + " damage to YOU!");
                currentHealth = currentHealth - damage;
                damage = rand.Next(mainCharacter.strength);
                mob.currentHealth = mob.currentHealth - damage;
                combatDialogue(mainCharacter.name + " does " + damage + " damage to " + mob.name);
                mainCharacter.currentHealth = currentHealth;
                if (mob.currentHealth <= 0)
                {
                    combatDialogue(mob.name + " IS DEAD!");
                    
                    theMap[enemyPosit.Y - 25, enemyPosit.X] = '$';
                    Console.SetCursorPosition(enemyPosit.X, enemyPosit.Y);
                    Console.Write('$');
                    clearEnemy();
                    battleFinished = true;
                    
                }
            }
           
            

            return true;
        }

        public static void clearEnemy()
        {
            Console.SetCursorPosition(50, 0);
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("                                                            ");
                Console.SetCursorPosition(50, 0 + i);
            }
        }


        public static void combatDialogue(string newStr)
        {
            int TOP = 5;
            int LEFT = 0;

            if (combatMessages.Count >= 9)
            {
                combatMessages.Dequeue();
                combatMessages.Enqueue(newStr);
            }
            else
            {
                combatMessages.Enqueue(newStr);
            }

            Console.SetCursorPosition(LEFT, TOP);
            if (combatMessages.Count > 0)
            {
                foreach (string str in combatMessages)
                {
                    Console.SetCursorPosition(LEFT, TOP);
                    Console.WriteLine("                                                               ");
                    Console.SetCursorPosition(LEFT, TOP);
                    Console.WriteLine(str);
                    TOP = TOP + 1;

                }
            }
        }
       public static void writeMessages(string newStr)
        {
           int TOP=52;
           int LEFT=25;

           if (themessages.Count >= 9)
           {
               themessages.Dequeue();
               themessages.Enqueue(newStr);
           }
           else
           {
               themessages.Enqueue(newStr);
           }

            Console.SetCursorPosition(LEFT, TOP);
            if (themessages.Count>0)
            {
                foreach (string str in themessages)
                {
                    Console.SetCursorPosition(LEFT, TOP);
                    Console.WriteLine("                                                               ");
                    Console.SetCursorPosition(LEFT, TOP);
                    Console.WriteLine(str);
                    TOP = TOP + 1;
                    
                }
            }
        }

       public static void healingSpell()
       {
           Random rand = new Random();
           int healingCost = Convert.ToInt32(mainCharacter.level * 2);
           if (mainCharacter.magic > healingCost)
           {
               int healing = rand.Next(1, mainCharacter.intellect);
               combatDialogue(mainCharacter.name + " casts healing spell for " + healing);
               if (mainCharacter.currentHealth + healing >= mainCharacter.maxHealth)
               {
                   mainCharacter.currentHealth = mainCharacter.maxHealth;
                   
               }
               else
               {
                   mainCharacter.currentHealth += healing;
               }
               mainCharacter.magic -= healingCost;
           }
       }

       public static void getNPC()
       {
           currentEnemy = new NPChar("Unicorn", 6, 6);
           
       }

    }
}

