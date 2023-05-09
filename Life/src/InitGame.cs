using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Life
{
    public class Initializer
    {
        public static void unaliveElements(Grid[] grid)
        {
            foreach (var element in grid)
            {
                element.Tag = "dead";
                element.Background = new SolidColorBrush(Colors.White);
            }
        }

        public static void aliveRandomElements(Grid[] grid)
        {
            Random random = new Random();                             //TODO реализовать рандомную генерацию
        }
    }
}