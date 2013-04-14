using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Map                                                                           //class map reads from files to a char array 
    {
        public static char[,] ReadMap(string themap)
        {

            try
            {
                char[,] map = new char[21, 105];                                        //set the size of the array (files for maps have to fit 21 x 105 to work
                StreamReader sr = new StreamReader("..\\maps\\"+themap);                //get the map from file based on the variable passed
                using (sr)                                                              //read the stream
                {                                                                       //cycle through the stream and insert the characters from file into the array
                    for (int i = 0; i < 21; i++)
                    {
                        String line = sr.ReadLine();

                        for (int j = 0; j < 105; j++)
                        {
                            if (j < line.Length)
                                map[i, j] = (char)(line[j]);
                        }
                    }

                }
                return map;                                                         //return the array
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("CANNOT FIND FILE");
            }
            return null;

        }//end function ReadMap

    }       //end class
    
}//end namespace
