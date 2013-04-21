using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogansDemise
{
    /// <summary>
    /// class used to load an enemy portrait from file
    /// </summary>
    class drawEnemy
    {
       public char[,] enemy { set; get; }  //variable used to hold the enemy

        public drawEnemy(String str)                //function used to load file and read file
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
                    }//end inner loop
                }//end outer loop

            }//end using

        }//end function drawEnemy


    }//end class drawenemy
}//end namespace
