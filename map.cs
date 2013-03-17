using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Map
    {
        public static char[,] ReadMap()
        {

            try
            {
                
                char[,] map = new char[21, 105];
                using (StreamReader sr = new StreamReader("maptest.txt"))
                {
                    for (int i = 0; i < 21; i++)
                    {
                        String line = sr.ReadLine();
                        
                        for (int j = 0; j < 105; j++)
                        {
                            if(j < line.Length)
                                map[i,j] = (char)(line[j]);
                        }
                    }            

                }
                return map;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("CANNOT FIND FILE");
            }
            return null;

        }

    }
    
}
