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
        


        public GameCharacter(string n, int str, int sta, int it, int spir, int agi)
        {
            name = n;
            strength = str;
            stamina = sta;
            intellect = it;
            spirit = spir;
            agility = agi;
            currentHealth = stamina * 10;

        }
        public GameCharacter()
        {
            name = null;
            strength = 10;
            intellect = 10;
            spirit = 10;
            stamina = 10; 
            agility = 10;
            currentHealth = stamina * 10;
        }

    }
}
