using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class NPChar
    {
        public string name { get; set; }
        public int currentHealth { get; set; }
        public int strength { get; set; }
        public int stamina { get; set; }
        

        public NPChar(string n, int str, int sta)
        {
            name = n;
            strength = str;
            stamina = sta;
            currentHealth = Convert.ToInt32(stamina * 2.5);
        }
        public NPChar()
        {
            name = null;
            strength = 8;
            stamina = 8;
            currentHealth = Convert.ToInt32(stamina * 2.5);
        }
    }


}
