using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Life
{
    public class Initializer
    {
        public static async Task unaliveElements(Grid[,] grid)
        {
            await Task.Delay(100);
            
            foreach (var element in grid)
            {
                element.Tag = "dead";
                element.Background = new SolidColorBrush(Colors.White);
            }
        }

        public static void initElements(Grid[,] grid)
        {
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
                /*using (FileStream fs = File.Create(path))
                {
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        for (int i = 0; i < gen.GetLength(0); i++)
                        {
                            for (int j = 0; j < gen.GetLength(1); j++)
                            {
                                writer.Write(gen[i,j] + " "); // записываем каждый элемент массива в файл с помощью метода Write()
                            }
                            writer.WriteLine(); // переходим на новую строку после записи каждой строки массива
                        }
                    }
                }*/
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void loadCondition(Grid[,] grid, int[,] gen, string path)
        {
            try
            {
                /*// Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }*/
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}