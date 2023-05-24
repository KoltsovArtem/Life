using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Life
{
    public class Evolution
    {
        public static async Task Evolve(Grid[,] grid, int[,] gen, int n, int n1, int n2)
        {
            Random random = new Random();

            while (MainWindow.isEvolving)
            {
                await Task.Delay(100);

                if (Game.CalculateNextState(grid, gen, n, n1, n2))
                    evolveOnce(grid, random);
                else
                {
                    MessageBox.Show("Позиция не поменялась, выполнение остановлено.");
                    MainWindow.isEvolving = false;
                }
            }
        }

        public static void evolveOnce(Grid[,] grid, Random random)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (grid[i, j].Tag == "alive")
                    {
                        Grid square = grid[i, j];
                        square.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(random.Next(255)),
                            Convert.ToByte(random.Next(255)), Convert.ToByte(random.Next(255))));
                    }
                    else
                    {
                        Grid square = grid[i, j];
                        square.Background = new SolidColorBrush(Colors.White);
                    }
                }
            }
        }
    }
}