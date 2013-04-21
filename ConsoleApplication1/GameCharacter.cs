using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    //class to represent the users game character 
     public class GameCharacter
    {
         //different variables representing a game character
        public string name      { get; set; }
        public int strength     { get; set; }
        public int stamina      { get; set; }
        public int intellect    { get; set; }
        public int spirit       { get; set; }
        public int agility      { get; set; }
        public int currentHealth { get; set; }
        public int level        { get; set; }
        public int exp          { get; set; }
        public int magic        { get; set; }
        public int maxHealth    { get; set; }
        public int maxMagic { get; set; }
        public int charCl       { get; set; }
        public string className { get; set; }
        public string attack1 { get; set; }
        public int xPos { get; set; }
        public int yPos { get; set; }
        public string currentMap { get; set; }

       
        public GameCharacter(string n, int str, int sta, int it, int spir, int agi, int xp, int lvl, int cl, int curMag, int curHea)  //constructor that takes all variables, used for saved games
        {
            name = n;
            strength = str;
            stamina = sta;
            intellect = it;
            spirit = spir;
            agility = agi;
            currentHealth = curHea;
            maxHealth = Convert.ToInt32(stamina * 2.5);
            level = lvl;
            exp = xp;
            maxMagic = Convert.ToInt32(intellect * 2.5);
            magic = curMag;
            charCl = cl;

        }
        public GameCharacter(string n, int cl)                      //constructor for beginning character
        {
            //get variables
            name = n;
            charCl = cl;
            if (cl == 1)    //1 is the fighter class
            {
                strength = 10;
                intellect = 6;
                stamina = 12;
                spirit = 5;
                agility = 6;
                className = "Fighter";
                attack1 = "punch";

            }
            if (cl == 2)   //2 is the stalker class
            {
                strength = 6;
                intellect = 6;
                stamina = 8;
                spirit = 5;
                agility = 10;
                className = "Stalker";
                attack1 = "slash";
            }
            if (cl == 3)    //3 is the sorcerer class
            {
                strength = 5;
                intellect = 10;
                stamina = 6;
                spirit = 8;
                agility = 6;
                className = "Sorceror";
                attack1 = "fireball";
            }
            level = 1;      //all start as level 1
            currentHealth = Convert.ToInt32(stamina * 2.5);         //calc the health, magic and max health of char
            maxMagic = magic = Convert.ToInt32(intellect * 2.5);
            maxHealth = Convert.ToInt32(stamina * 2.5);
        

        }
        public GameCharacter()                                      //default constructor
        {
            name = null;
            strength = 10;
            intellect = 10;
            spirit = 10;
            stamina = 10; 
            agility = 10;
            currentHealth = Convert.ToInt32(stamina * 2.5);
            level = 1;
            exp = 0;
            maxMagic = magic = Convert.ToInt32(intellect * 2.5);
            maxHealth = Convert.ToInt32(stamina * 2.5);
            charCl = 1;
        }

        public bool checkXP()                                           //function to check xp after winning battles to see if a level has been gained
        {
            if (exp >= 100)
            {
                level = 2;
                increaseStats(1);                                       //if there was a level increase characters stats
                return true;
            }
            return false;
        }

        public void increaseStats(int i)                                //function to increase the stats by whatever is passed to it
        {
            strength+=i;
            intellect+=i;
            stamina +=i;
            intellect += i;
            spirit += i;
            agility += i;
            currentHealth = Convert.ToInt32(stamina * 2.5);
            magic = Convert.ToInt32(intellect * 2.5);
            maxHealth = Convert.ToInt32(stamina * 2.5);

        }

    }
}
