using System;
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

        public static void aliveRandomElements(Grid[,] grid)
        {
            Random random = new Random();
            int k, l;
            for (int i = 0; i < 200; i++)
            {
                k = random.Next(20);
                l = random.Next(20);
                grid[k, l].Tag = "alive";
            }
            Game.clickEvolve(grid);
        }
    }
}