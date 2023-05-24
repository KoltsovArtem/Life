using System;
using System.IO;
using System.Windows.Controls;

namespace Life
{
    public class SLCondition
    {
        public static void saveCondition(Grid[,] grid, int[,] gen, string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    for (int i = 0; i < gen.GetLength(0); i++)
                    {
                        for (int j = 0; j < gen.GetLength(1); j++)
                        {
                            writer.Write(gen[i, j] + " ");
                        }
                        writer.WriteLine();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static bool loadCondition(Grid[,] grid, int[,] gen, string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < 20; i++)
                {
                    string[] valuesStringArray=lines[i].Split(' ');
                    for (int j = 0;j < 20; j++){
                        gen[i,j] = Convert.ToInt32(valuesStringArray[j]);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            int g = 0;
            for (int k = 0; k < 20; k++)
            {
                for (int l = 0; l < 20; l++)
                {
                    if (gen[k, l] != 0)
                    {
                        g++;
                        grid[k, l].Tag = "alive";
                    }
                    else
                        grid[k, l].Tag = "dead";
                }
            }
            
            Evolution.evolveOnce(grid, new Random());
            if (g != 0)
            {
                return true;
            }

            return false;
        }
    }
}