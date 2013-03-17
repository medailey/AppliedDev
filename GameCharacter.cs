using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class GameCharacter
    {
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
        


        public GameCharacter(string n, int str, int sta, int it, int spir, int agi, int xp, int lvl)
        {
            name = n;
            strength = str;
            stamina = sta;
            intellect = it;
            spirit = spir;
            agility = agi;
            currentHealth = Convert.ToInt32(stamina * 2.5);
            maxHealth = Convert.ToInt32(stamina * 2.5);
            level = lvl;
            exp = xp;
            magic = Convert.ToInt32(intellect * 2.5);

        }
        public GameCharacter()
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
            magic = Convert.ToInt32(intellect * 2.5);
            maxHealth = Convert.ToInt32(stamina * 2.5);
        }

    }
}
