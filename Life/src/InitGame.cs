using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Life
{
    public class Initializer
    {
        public static async Task unaliveElements(Grid[,] grid, int[,] gen)
        {
            await Task.Delay(100);

            for (int i = 0; i < gen.GetLength(0); i++)
            {
                for (int j = 0; j < gen.GetLength(1); j++)
                {
                    gen[i, j] = 0;
                }
            }
            
            foreach (var element in grid)
            {
                element.Tag = "dead";
                element.Background = new SolidColorBrush(Colors.White);
            }
        }

        public static void aliveRandomElements(Grid[,] grid, int[,] gen)
        {
            Random random = new Random();
            
            int k, l;
            for (int i = 0; i < 200; i++)
            {
                k = random.Next(20);
                l = random.Next(20);
                grid[k, l].Tag = "alive";
                gen[k, l] = 1;
            }
            Game.evolveOnce(grid, random);
        }

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
            
            Game.evolveOnce(grid, new Random());
            if (g != 0)
            {
                return true;
            }

            return false;
        }
    }
}