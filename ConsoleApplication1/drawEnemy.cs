using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class drawEnemy
    {
       public char[,] enemy { set; get; }

        public drawEnemy(String str)
        {
             enemy = new char[10,35];
            using (StreamReader sr = new StreamReader(str))
            {
                for (int i = 0; i < 8; i++)
                {
                    String line = sr.ReadLine();

                    for (int j = 0; j < 35; j++)
                    {
                        if (j < line.Length)
                            enemy[i, j] = (char)(line[j]);
                    }
                }

            }

        }


    }
}
