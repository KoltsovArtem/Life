using System;
using System.Windows.Controls;

namespace Life
{
    public class Game
    {
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
        
        public static bool CalculateNextState(Grid[,] grid, int [,] gen, int n, int n_1, int n_2)
        {
            int n1 = Math.Min(n_1, n_2);
            int n2 = Math.Max(n_1, n_2);
            int[,] arr = new int[20, 20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (grid[i, j].Tag == "dead" && countNeighbours(i, j, grid) == n)
                    {
                        arr[i, j] = 1;
                    }
                    else if (grid[i, j].Tag == "alive" && (countNeighbours(i, j, grid)<n1 && countNeighbours(i, j, grid)>n2))
                    {
                        arr[i, j] = 0;
                    }
                    else if (grid[i, j].Tag == "alive" && (countNeighbours(i, j, grid) == n1 || countNeighbours(i, j, grid) == n2))
                    {
                        arr[i, j] = 1;
                    }
                    else
                    {
                        arr[i, j] = 0;
                    }
                }
            }

            int k = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        if (grid[i, j].Tag != "alive")
                            k = 1;
                        gen[i, j]++;
                        grid[i, j].Tag = "alive";
                    }
                    else
                    {
                        if (grid[i, j].Tag != "dead")
                            k = 1;
                        gen[i, j] = 0;
                        grid[i, j].Tag = "dead";
                    }
                }
            }

            if (k != 0)
                return true;
            return false;
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