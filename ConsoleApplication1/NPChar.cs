using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /// <summary>
    /// NPChar class used to create enemy characters
    /// two constructors one default constructor and another that takes 3 parameters
    /// </summary>
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
        }//end NPChar

        public NPChar()
        {
            name = null;
            strength = 8;
            stamina = 8;
            currentHealth = Convert.ToInt32(stamina * 2.5);
        }//end NPChar
    }//end Class NPChar

}//End Namespace
