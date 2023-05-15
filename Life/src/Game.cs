using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Life
{
    public class Game
    {
        public static async Task Evolve(Grid[,] grid)
        {
            Random random = new Random();

            while (MainWindow.isEvolving)
            {
                await Task.Delay(100);

                CalculateNextState(grid);

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

        public static void clickEvolve(Grid[,] grid)
        {
            Random random = new Random();

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

        static int countNeighbours (int i, int j, Grid[,] grid)
        {
            var count = 0;
            for(int k = i - 1; k <= i + 1; k++)
            {
                for (int l = j - 1; l <= j + 1; l++)
                {
                    if (!IsInsideMap(k, l))
                        continue;
                    if (k == i && l==j)
                        continue;
                    if (grid[k, l].Tag == "alive")
                        count++;
                }
            }
            return count;
        }
        
        public static void CalculateNextState(Grid[,] grid)
        {
            int[,] arr = new int[20, 20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (grid[i, j].Tag == "dead" && countNeighbours(i, j, grid) == 3)
                    {
                        arr[i, j] = 1;
                    }
                    else if (grid[i, j].Tag == "alive" && (countNeighbours(i, j, grid)<2 && countNeighbours(i, j, grid)>3))
                    {
                        arr[i, j] = 0;
                    }
                    else if (grid[i, j].Tag == "alive" && (countNeighbours(i, j, grid) == 2 || countNeighbours(i, j, grid) == 3))
                    {
                        arr[i, j] = 1;
                    }
                    else
                    {
                        arr[i, j] = 0;
                    }
                }
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        grid[i, j].Tag = "alive";
                    }
                    else
                    {
                        grid[i, j].Tag = "dead";
                    }
                }
            }
        }
        
        static bool IsInsideMap(int i,int j)
        {
            if(i<0 || i>=20 || j<0 || j >= 20)
            {
                return false;
            }
            return true;
        }
    }
}