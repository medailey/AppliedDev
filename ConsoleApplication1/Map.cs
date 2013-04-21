using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogansDemise
{
    public class Map                                                                           //class map reads from files to a char array 
    {
        public Map()
        {
        }

        public Map(string theMap)
        {
            ReadMap(theMap);
        }

        public char[,] map {set; get;}
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
                        }//end inner loop
                    }//end outer loop

                }//end using
                return map;                                                         //return the array
            }//end try
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("CANNOT FIND FILE");
            }
            return null;

        }//end function ReadMap

    }       //end class
    
}//end namespace
