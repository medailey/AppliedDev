using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogansDemise
{
    class Item
    {
        public string name {set; get;}
        public char stat { set; get; }
        public int power { set; get; }


        public Item()
        {
            name = "Health Potion";
            stat = 'H';
            power = 10;
        }

        public Item(string n, char s, int p)
        {
            name = n;
            stat = s;
            power = p;
        }
           

    }
}
