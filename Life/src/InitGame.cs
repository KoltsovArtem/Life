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
            Evolution.evolveOnce(grid, random);
        }
    }
}