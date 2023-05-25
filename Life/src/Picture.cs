using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Life
{
    public class Picture
    {
        public static void Save(int[,] gen, string path)
        {
            int[,] array = new int[20, 20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (gen[i, j] != 0)
                        array[i, j] = 1;
                }
            }

            WriteableBitmap bitmap = ConvertArrayToImage(array);

            SaveImage(bitmap, path);
        }

        static WriteableBitmap ConvertArrayToImage(int[,] array)
        {
            int width = array.GetLength(1);
            int height = array.GetLength(0);

            WriteableBitmap bitmap = new WriteableBitmap(20, 20, 96, 96, PixelFormats.Gray8, null);

            byte[] pixelData = new byte[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int value = array[y, x];
                    byte pixelValue = (byte)(value == 1 ? 255 : 0);
                    int position = y * width + x;
                    pixelData[position] = pixelValue;
                }
            }

            int stride = (width * bitmap.Format.BitsPerPixel + 7) / 8;
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixelData, stride, 0);
            return bitmap;
        }

        static void SaveImage(WriteableBitmap bitmap, string filePath)
        {
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
        
        public static void Load(string filePath, Grid[,] grid, int[,] array)
        {
            BitmapSource bitmap = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));

            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;

            byte[] pixelData = new byte[width * height];
            bitmap.CopyPixels(pixelData, width, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int position = y * width + x;
                    byte pixelValue = pixelData[position];
                    array[y, x] = pixelValue == 255 ? 1 : 0;
                    grid[y, x].Tag = pixelValue == 255 ? "alive" : "dead";
                }
            }
        }
    }
}