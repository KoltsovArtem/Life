using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;

namespace Life
{
    public class Game
    {
        /*void Update()
        {
            if (MainWindow.isEvolving)
            {
                InfiniteCoroutine()
            }
        }*/
        public static void Evolve(bool evolving, Grid[] grid)
        {
            /*if (MainWindow.isEvolving)
            {
                InfiniteCoroutine(grid);
            }*/
            //StartCoroutine evolver();
            Random random = new Random();
            
            while (evolving)
            {
                if (!MainWindow.isEvolving) break;
            }

            /*if (MainWindow.isEvolving)
            {
                Grid square = grid[25];
                square.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(random.Next(255)),
                    Convert.ToByte(random.Next(255)), Convert.ToByte(random.Next(255))));
            }*/
        }

        static IEnumerator InfiniteCoroutine(Grid[] grid)
        {
            Random random = new Random();
            
            while (MainWindow.isEvolving)
            {
                Grid square = grid[25];
                square.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(random.Next(255)),
                    Convert.ToByte(random.Next(255)), Convert.ToByte(random.Next(255))));
            }

            return null;
        }
    }
}