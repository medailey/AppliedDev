using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LogansDemise
{
    /// <summary>
    /// Main Game Class 
    /// </summary>
    class Game
    {
        public static COORD User { get; set; }                              //structure used to keep track of  X/Y coordinates of user and it's place on the map
        public static char[,] theMap;                                       //holds the map used for the current screen
        public static bool quit = false;
        public static GameCharacter mainCharacter;                          //holds all information about current user character
        public static Queue<string> themessages = new Queue<string>();      //queue used to go through messages needed for information for the user
        public static Queue<string> combatMessages = new Queue<string>();   //queue to display the combat messages 
        public static bool enemyNear;                                       //boolean to check if the user has discovered an enemy
        public static COORD enemyPosit { get; set; }                        //keep track of the position of the enemy
        public static NPChar currentEnemy;                                  //holds the current enemy if there is one
        public static string currentMap;                                    //keeps track of the current map
        public static string prevMap;                                       //keeps track of the previous map if any
        public static double magicRegen=0;                                  //keeps track of the regeneration of magic as the user moves around
        
        public static bool enemyBoss = false;                               //keep track if the user has found the boss
        public static bool doorFound = false;                               //tracks if the user has found a door

        public Game()        
        {                         
        }   //end Game Constructor

        public Game(GameCharacter user, int i)         //constructor that takes a gamecharacter from a saved game 
        {
            quit = false;
            themessages.Clear();
            combatMessages.Clear();
            
            while (quit != true)
            {
                mainCharacter = user;
                if (mainCharacter.currentMap != null)
                {
                    theMap = Map.ReadMap(mainCharacter.currentMap);     //get the map from the user saved game
                }
                else
                {
                    theMap = Map.ReadMap("map_1.txt");                  //load default map if saved game has none
                }
                User = new COORD() { X = user.xPos, Y = user.yPos };    //set the users position on map
                startGame();                                            //start the game again
                quit = true;                                            //end the loop when user exits
            }//end while

        }
        public Game(GameCharacter user)                 //constructor that takes a user (from new game)
        {
            themessages.Clear();
            combatMessages.Clear();
            quit = false;
            while (quit != true)                        //loop to keep game going until it has been quit
            {                           
                mainCharacter = user;                   //create the main character                    
                theMap = Map.ReadMap("map_1.txt");      //read the map
                currentMap = "map_1.txt";               //set the current map
                prevMap = "0";                          //set previous map
                User = new COORD() { X = 1, Y = 26 };   //set start of the user
                startGame();                            //start game
                quit = true;                            //exit loop when quitting
            }//end while
        }//end Game constructor

        public struct COORD
        {
            public int X; public int Y;
            public COORD(int X, int Y)
            { this.X = X; this.Y = Y; }
        };                                              //structure to hold the coordinates for characters on screen

        /// <summary>
        /// basic game loop used to continue game until quit is initiated
        /// </summary>
        static void GameLoop()          
        {   
                Console.Clear();                                                                        //prepare console           
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);        //cancel key press declared
                enemyNear = false;                                                                      //set enemy near to false

                
                Console.SetCursorPosition(User.X, User.Y);                                              //set the cursor
                Console.WriteLine("@");                                                                 //place the user
                moveCharacter(0, 0);
                characterMenu();
                ConsoleKeyInfo keyInfo;

               //while loop that handles the movement and battle functions of the game.
                //determines what happens and when based on the keys pressed by the user

                while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape && quit != true)
                {
                    
                    switch (keyInfo.Key)
                    {
                            //arrow keys move character
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

                            //"A" key initiates a battle if there is an enemy
                        case ConsoleKey.A:
                            {
                                if(enemyNear ==true)
                                {
                                    if (enemyBoss == true)
                                    {
                                        NPChar boss = new NPChar("Harlie", 10, 10);         //check if the enemy is a boss
                                        battle(mainCharacter, boss);                        //start battle
                                    }
                                    else
                                    {
                                        battle(mainCharacter, currentEnemy);                //start battle                        
                                    }
                                }//end if
                                break;
                            }
                            //"R" key casts a healing spell
                        case ConsoleKey.R:
                            {
                                healingSpell();
                                break;
                            }
                            //"E" will enter a door if the user has found one
                        case ConsoleKey.E:
                            {
                                if (doorFound == true)
                                {
                                    newArea();
                                    doorFound = false;
                                }
                                   break;
                            }

                            //"H" key saves the game
                        case ConsoleKey.H:
                            {
                                saveGame();
                                break;
                            }
                    } //end switch
                    characterMenu();
                }//end while loop
            } //end function GameLoop

        /// <summary>
        /// function used to save the game whenever user pressed "h" during gameplay
        /// </summary>
        static void saveGame()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path = path + "\\Saves\\";            
            System.IO.Directory.CreateDirectory(path);
            string saveName = mainCharacter.name + "_" + mainCharacter.className + "_Lvl" + mainCharacter.level + ".xml";
            mainCharacter.xPos = User.X;
            mainCharacter.yPos = User.Y;
            mainCharacter.currentMap = currentMap;

            using (FileStream FS = new FileStream(path + saveName, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(GameCharacter));
                xSer.Serialize(FS, mainCharacter);
            }
            writeMessages("Your game has been saved as: "+saveName);
        } //end function saveGame()
        /// <summary>
        /// function moveCharacter takes an x, y change of position to determine if the user can move to the location
        /// also if anything happens each time the user moves this occurs
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        static void moveCharacter(int x, int y)
        {         
            COORD newPos = new COORD()
              {
                  X = User.X + x,
                  Y = User.Y + y
              };
            int val1 = newPos.X;
            int val2 = (newPos.Y-25);               //have to compensate for the fact the map starts 25 rows from the top of the console
            char currChar = theMap[val2, val1];     //get the actual value for the spot the user wishes to move to
            
            if (canMove(newPos)) //if the user can move to the new position
            {
               clearCharacter(newPos);
                Console.SetCursorPosition(newPos.X, newPos.Y);
                Console.WriteLine("@");
                magicRegen += mainCharacter.spirit * .2;
                //regen magic while out of combat based on characters spirit
                //auto regen if the magic isn't full
                if (magicRegen > 10)
                {

                    if (mainCharacter.magic == mainCharacter.maxMagic)
                    {
                        //if at full magic do nothing
                    }
                    else if (mainCharacter.magic + 5 >= mainCharacter.maxMagic)
                    {//if magic + regen is greater than max magic make magic the max
                        combatDialogue("You regen " + (mainCharacter.maxMagic - mainCharacter.magic) + " magic");
                        mainCharacter.magic = mainCharacter.maxMagic;
                    }//end else if

                    else
                    {//otherwise just add 5 magic to user
                        mainCharacter.magic += 5;
                        combatDialogue("You regen 5 magic");
                    }//end else
                    magicRegen = 0; //reset regen
                }//end if
                User = newPos;                
                
            }//end if
            else //if the user cannot move to the new position
            {
                Console.SetCursorPosition(newPos.X,newPos.Y);
                Console.WriteLine(theMap[newPos.Y - 25, newPos.X]);                
                newPos = User;
               
            }//end else
        }//end function move character

        /// <summary>
        /// function used to clear the trace of the user
        /// also function displays the spaces around the user as they discover the map
        /// </summary>
        /// <param name="newPos"></param>
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
        }//end clear character

        //function used to determine if the user can move based on character defined on the map
        public static Boolean canMove(COORD posit)
        {    
            //makesure the position is valid
            if(posit.X >0)
            {
                char c = theMap[posit.Y-25,posit.X]; //get the character at the desired position

                //based on the map display what the user is doing and can do
                switch (c)
                {
                    case '.':
                        {
                            writeMessages("You move through the area");
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
                    case '/':
                        {
                            writeMessages("Continue to next area?");
                            doorFound = true;
                            return false;
                        }
                    case '%':
                        {
                            writeMessages("YOU HAVE ENCOUNTERED HARLIE THE ANGRY DOG!!");
                            enemyPosit = posit;
                            getNPC();
                            enemyNear = true;
                            enemyBoss = true;
                            return false;
                        }


                    default:
                        {
                            return false;
                        }
                }//end switch
                
               
            }//end if
            return false;
        }  //end function canMove


        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("{0} hit, quitting...", e.SpecialKey);
            quit = true;
            e.Cancel = true;
        } //function defines the quit key

        /// <summary>
        /// function used to display the characters HUD on the screen at all times
        /// </summary>
        static void characterMenu()
        {
            Console.SetCursorPosition(0, 0);            
            Console.Write("Name: "+ mainCharacter.name + " \tLevel: " + mainCharacter.level + " "+ mainCharacter.className +  "\nHP: " +mainCharacter.currentHealth + "/" + mainCharacter.maxHealth + "\tMagic: " + mainCharacter.magic + "/" + mainCharacter.maxMagic +  
                "\tXP: "+mainCharacter.exp +"\nSTR: " +mainCharacter.strength + "\tSTA: " + mainCharacter.stamina + "\tSPI: " + mainCharacter.spirit + "\nINT: " + mainCharacter.intellect + "\tAGI: " + mainCharacter.agility);


            if (mainCharacter.charCl == 2)
            {
                Console.SetCursorPosition(0, 68);
                Console.Write("Controls:  Arrow Keys For Movement");
                Console.SetCursorPosition(0, 69);
                Console.Write("(a)attack/initiate battle\t(b)backstab\t(v)disengage(r) Healing spell\t(e)To interact with objects\t(h)save game");
            }
            else
            {
                Console.SetCursorPosition(0, 68);
                Console.Write("Controls:  Arrow Keys For Movement");
                Console.SetCursorPosition(0, 69);
                Console.Write("(a)attack/initiate battle\t(r) Healing spell\t(e) To interact with objects\t(h)save game");
            }
            Console.SetCursorPosition(0, 0);
        }//end charactermenu

        //function called to start the game
        public void startGame()
        {           
                Console.Clear();
                Console.Out.Flush();
                GameLoop();           
        }//end function start game

        /// <summary>
        /// function used to battle user and enemy
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mob"></param>
        /// <returns></returns>
        public static bool battle(GameCharacter user, NPChar mob)
        {
            bool battleFinished = false;
            bool backstab = true;
            int top = 0;
            int left = 50;
            drawEnemy enemy;                                //class/object used to load enemy portrait
            if (enemyBoss == true)
            {
                enemy = new drawEnemy("..\\art\\dog.txt");  //if enemy is a boss
            }
            else
            {
                enemy = new drawEnemy("..\\art\\unicorn.txt");  //otherwise draw generic enemy
            }
            char[,] pic = new char[10, 35];
            pic = enemy.enemy;
            //draw the portrait
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 35; k++)
                {
                    Console.Write(pic[i, k]);

                }
                top++;
                Console.SetCursorPosition(left, top);
            }//end for
            combatDialogue("Begin Battle!");
            
            //loop through battle until mob or user is dead
            while (battleFinished == false && mob.currentHealth > 0)
            {
                ConsoleKeyInfo keyInfo;               
                keyInfo = Console.ReadKey();
                Console.SetCursorPosition(80, 0);
                Console.WriteLine("Enemy: " + mob.name + " Health: " + mob.currentHealth + " ");
                switch (keyInfo.Key)
                {
                        //"A" used for generic attack
                    case ConsoleKey.A:
                        {
                            if (enemyNear == true)      //if there is an enemy 
                            {
                                Random rand = new Random();
                                int currentHealth = mainCharacter.currentHealth;
                                int damage;
                                    
                                damage = rand.Next(mob.strength);
                                combatDialogue(mob.name + " does " + damage + " damage to YOU!");
                                currentHealth = currentHealth - damage;
                               //get the damage caused based on users class (1 fighter, 2 stalker, 3 sorceror)
                                if (mainCharacter.charCl == 1)
                                {
                                    damage = rand.Next(2, mainCharacter.strength);
                                }
                                else if (mainCharacter.charCl == 2)
                                {
                                    damage = rand.Next(2, mainCharacter.agility);
                                }
                                else if (mainCharacter.charCl == 3)
                                {
                                    damage = rand.Next(2, mainCharacter.intellect);
                                }
                                //damage the mob
                                mob.currentHealth = mob.currentHealth - damage;
                                combatDialogue(mainCharacter.name + " does " + damage + " damage to " + mob.name + " using a " + mainCharacter.attack1);
                                mainCharacter.currentHealth = currentHealth;
                            }//end if
                            break;
                        }
                        //user can cast a healing spell during combat but will lose their turn
                    case ConsoleKey.R:
                        {
                            if (mainCharacter.charCl == 3)  //sorceror can heal during battle
                            {
                                Random rand = new Random();
                                healingSpell();
                                int currentHealth = mainCharacter.currentHealth;
                                int damage;

                                damage = rand.Next(mob.strength);
                                combatDialogue(mob.name + " does " + damage + " damage to YOU!");
                                currentHealth = currentHealth - damage;
                            }
                            break;
                        }
                    case ConsoleKey.V:  //Disengages battle with non-boss enemies (stalker only)
                        {
                            if(mainCharacter.charCl == 2 && enemyBoss !=true)
                            {
                                battleFinished = true;
                            }
                            break;
                        }

                    case ConsoleKey.B:  //can backstab for first attack (stalker only)
                        {
                            
                            if (mainCharacter.charCl == 2 && backstab == true)
                            {
                                Random rand = new Random();
                                int currentHealth = mainCharacter.currentHealth;
                                int damage;
                               
                                damage = rand.Next(4, (mainCharacter.agility*2));
                                
                               //damage the mob
                                mob.currentHealth = mob.currentHealth - damage;
                                combatDialogue(mainCharacter.name + " backstabs " + mob.name + " for " + damage+".");
                                mainCharacter.currentHealth = currentHealth;
                                backstab = false;
                            } //end if
                            break;
                        } 
                    default:
                        {
                            break;
                        }
                }//end switch
                characterMenu();  //show the character menu

                Console.SetCursorPosition(80, 0);
                Console.WriteLine("Enemy: " + mob.name + " Health: " + mob.currentHealth + " ");
                //check if the enemy is dead
                //if so replace enemy on map with body of enemy and give user experience
                if (mob.currentHealth <= 0)
                {
                    combatDialogue(mob.name + " IS DEAD!");

                    theMap[enemyPosit.Y - 25, enemyPosit.X] = '$';
                    Console.SetCursorPosition(enemyPosit.X, enemyPosit.Y);
                    Console.Write('$');
                    clearEnemy();
                    if (enemyBoss == true)
                    {
                        combatDialogue("You gain 25 experience.");
                        mainCharacter.exp += 25;
                        enemyBoss = false;
                    }//end if
                    else
                    {
                        combatDialogue("You gain 10 experience.");
                        mainCharacter.exp += 10;
                    }//end else
                    bool level = mainCharacter.checkXP();
                    if (level)
                    {
                        combatDialogue("You have gained a level! Your stats have increased!");
                    }//end if
                    battleFinished = true;
                    enemyNear = false;
                }//end if
                    //if the user is the one dead
                else if (mainCharacter.currentHealth <= 0)
                {
                    combatDialogue("You have died of your wounds!");
                    combatDialogue("Game Over.  Press Any Key to Return to Main Menu.");
                    while (!Console.KeyAvailable)
                    {

                    }
                    battleFinished = true;
                    quit = true;
                }//end else if
            }//end  loop                
        return true;
    }//end function
        //function used to clear the enemy from the screen
        public static void clearEnemy()
        {
            Console.SetCursorPosition(50, 0);
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("                                                            ");
                Console.SetCursorPosition(50, 0 + i);
            }
        }//end function clearEnemy

        //function used to display combat dialogue using a queue that can hold upto 10 items
        public static void combatDialogue(string newStr)
        {
            int TOP = 5;
            int LEFT = 0;

            if (combatMessages.Count >= 9)
            {
                combatMessages.Dequeue();
                combatMessages.Enqueue(newStr);
            }//end if
            else
            {
                combatMessages.Enqueue(newStr);
            }//end else

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

                }//end foreach
            }//end if
        }//end function combatdialogue
        /// <summary>
        /// function used to display the messages for the user on the screen using a queue holding upto 10 items
        /// </summary>
        /// <param name="newStr"></param>
        public static void writeMessages(string newStr)
        {
            int TOP=52;
            int LEFT=25;

            if (themessages.Count >= 9)
            {
                themessages.Dequeue();
                themessages.Enqueue(newStr);
            } //end if
            else
            {
                themessages.Enqueue(newStr);
            }//end else

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
                    
                }//end foreach
            }//end if
        }//end function writemessages

        /// <summary>
        /// function used to handle the healing spell when cast
        /// </summary>
        public static void healingSpell()
        {
            Random rand = new Random();
            int healingCost = Convert.ToInt32(mainCharacter.level * 1.8);                   //determine the magic cost of the spell
            if (mainCharacter.magic > healingCost)
            {
                int healing = rand.Next(1, mainCharacter.intellect);                        //get random healing based on users intellect
                combatDialogue(mainCharacter.name + " casts healing spell for " + healing); //display healing spell
                if (mainCharacter.currentHealth + healing >= mainCharacter.maxHealth)
                {
                    mainCharacter.currentHealth = mainCharacter.maxHealth;                  //heal the user to max health if it will do so
                   
                }//end if
                else
                {
                    mainCharacter.currentHealth += healing;                                 //heal user if they won't be at max health
                }//end else 
                mainCharacter.magic -= healingCost;                                         //remove magic from user
            }//end if
        }//end healingspell function

        //function to create enemy
        public static void getNPC()
        {
            currentEnemy = new NPChar("Unicorn", 6, 6);           
        }//end getNPC function

        /// <summary>
        /// function used to move from map to map
        /// </summary>
        public static void newArea()
        {
            User = new COORD(1, 26);
            if (currentMap == "map_1.txt")
            {
                theMap = Map.ReadMap("map_2.txt");
                prevMap = "map_1.txt";
                currentMap = "map_2.txt";
            }//end if
            else if(currentMap == "map_2.txt")
            {
                theMap = Map.ReadMap("map_1.txt");
                prevMap = "map_2.txt";
                currentMap = "map_1.txt";
            }//end else if
            doorFound = false;
            Console.Clear();
        }//end function new area
        
    }//end class Game
}//end namespace

