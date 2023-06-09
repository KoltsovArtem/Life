﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using Grpc.Net.Client;

namespace Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static bool isEvolving;
        private Grid[,] theGrid;
        private int[,] arrGen = new int[20,20];
        
        public MainWindow()
        {
            InitializeComponent();
            theGrid = new Grid[,]
            {
                {
                    r0c0, r0c1, r0c2, r0c3, r0c4, r0c5, r0c6, r0c7, r0c8, r0c9, r0c10, r0c11, r0c12, r0c13, r0c14,
                    r0c15, r0c16, r0c17, r0c18, r0c19
                },
                {
                    r1c0, r1c1, r1c2, r1c3, r1c4, r1c5, r1c6, r1c7, r1c8, r1c9, r1c10, r1c11, r1c12, r1c13, r1c14,
                    r1c15, r1c16, r1c17, r1c18, r1c19
                },
                {
                    r2c0, r2c1, r2c2, r2c3, r2c4, r2c5, r2c6, r2c7, r2c8, r2c9, r2c10, r2c11, r2c12, r2c13, r2c14,
                    r2c15, r2c16, r2c17, r2c18, r2c19
                },
                {
                    r3c0, r3c1, r3c2, r3c3, r3c4, r3c5, r3c6, r3c7, r3c8, r3c9, r3c10, r3c11, r3c12, r3c13, r3c14,
                    r3c15, r3c16, r3c17, r3c18, r3c19
                },
                {
                    r4c0, r4c1, r4c2, r4c3, r4c4, r4c5, r4c6, r4c7, r4c8, r4c9, r4c10, r4c11, r4c12, r4c13, r4c14,
                    r4c15, r4c16, r4c17, r4c18, r4c19
                },
                {
                    r5c0, r5c1, r5c2, r5c3, r5c4, r5c5, r5c6, r5c7, r5c8, r5c9, r5c10, r5c11, r5c12, r5c13, r5c14,
                    r5c15, r5c16, r5c17, r5c18, r5c19
                },
                {
                    r6c0, r6c1, r6c2, r6c3, r6c4, r6c5, r6c6, r6c7, r6c8, r6c9, r6c10, r6c11, r6c12, r6c13, r6c14,
                    r6c15, r6c16, r6c17, r6c18, r6c19
                },
                {
                    r7c0, r7c1, r7c2, r7c3, r7c4, r7c5, r7c6, r7c7, r7c8, r7c9, r7c10, r7c11, r7c12, r7c13, r7c14,
                    r7c15, r7c16, r7c17, r7c18, r7c19
                },
                {
                    r8c0, r8c1, r8c2, r8c3, r8c4, r8c5, r8c6, r8c7, r8c8, r8c9, r8c10, r8c11, r8c12, r8c13, r8c14,
                    r8c15, r8c16, r8c17, r8c18, r8c19
                },
                {
                    r9c0, r9c1, r9c2, r9c3, r9c4, r9c5, r9c6, r9c7, r9c8, r9c9, r9c10, r9c11, r9c12, r9c13, r9c14,
                    r9c15, r9c16, r9c17, r9c18, r9c19
                },
                {
                    r10c0, r10c1, r10c2, r10c3, r10c4, r10c5, r10c6, r10c7, r10c8, r10c9, r10c10, r10c11, r10c12,
                    r10c13, r10c14, r10c15, r10c16, r10c17, r10c18, r10c19
                },
                {
                    r11c0, r11c1, r11c2, r11c3, r11c4, r11c5, r11c6, r11c7, r11c8, r11c9, r11c10, r11c11, r11c12,
                    r11c13, r11c14, r11c15, r11c16, r11c17, r11c18, r11c19
                },
                {
                    r12c0, r12c1, r12c2, r12c3, r12c4, r12c5, r12c6, r12c7, r12c8, r12c9, r12c10, r12c11, r12c12,
                    r12c13, r12c14, r12c15, r12c16, r12c17, r12c18, r12c19
                },
                {
                    r13c0, r13c1, r13c2, r13c3, r13c4, r13c5, r13c6, r13c7, r13c8, r13c9, r13c10, r13c11, r13c12,
                    r13c13, r13c14, r13c15, r13c16, r13c17, r13c18, r13c19
                },
                {
                    r14c0, r14c1, r14c2, r14c3, r14c4, r14c5, r14c6, r14c7, r14c8, r14c9, r14c10, r14c11, r14c12,
                    r14c13, r14c14, r14c15, r14c16, r14c17, r14c18, r14c19
                },
                {
                    r15c0, r15c1, r15c2, r15c3, r15c4, r15c5, r15c6, r15c7, r15c8, r15c9, r15c10, r15c11, r15c12,
                    r15c13, r15c14, r15c15, r15c16, r15c17, r15c18, r15c19
                },
                {
                    r16c0, r16c1, r16c2, r16c3, r16c4, r16c5, r16c6, r16c7, r16c8, r16c9, r16c10, r16c11, r16c12,
                    r16c13, r16c14, r16c15, r16c16, r16c17, r16c18, r16c19
                },
                {
                    r17c0, r17c1, r17c2, r17c3, r17c4, r17c5, r17c6, r17c7, r17c8, r17c9, r17c10, r17c11, r17c12,
                    r17c13, r17c14, r17c15, r17c16, r17c17, r17c18, r17c19
                },
                {
                    r18c0, r18c1, r18c2, r18c3, r18c4, r18c5, r18c6, r18c7, r18c8, r18c9, r18c10, r18c11, r18c12,
                    r18c13, r18c14, r18c15, r18c16, r18c17, r18c18, r18c19
                },
                {
                    r19c0, r19c1, r19c2, r19c3, r19c4, r19c5, r19c6, r19c7, r19c8, r19c9, r19c10, r19c11, r19c12,
                    r19c13, r19c14, r19c15, r19c16, r19c17, r19c18, r19c19
                }
            };
            if (File.ReadAllText(@"..\..\Files\Condition.txt") != string.Empty)
            {
                if (Initializer.loadCondition(theGrid, arrGen, @"..\..\Files\Condition.txt"))
                {
                    stop();
                }
                File.WriteAllText(@"..\..\Files\Condition.txt", string.Empty);
            }
            var channel = GrpcChannel.ForAddress("https://localhost:5111");
            var client = new Greeter.GreeterClient(channel);
        }

        public void stop()
        {
            Restart.Visibility = Visibility.Collapsed;
            Start.Visibility = Visibility.Collapsed;
            Stop.Visibility = Visibility.Collapsed;
            Continue.Visibility = Visibility.Visible;
            ClickMove.Visibility = Visibility.Visible;
            Random.Visibility = Visibility.Collapsed;
        }
        
        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            Restart.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Start.Visibility = Visibility.Collapsed;
            Continue.Visibility = Visibility.Collapsed;
            Random.Visibility = Visibility.Collapsed;
            isEvolving = true;

            await Game.Evolve(theGrid, arrGen);
        }

        private async void Restart_Click(object sender, RoutedEventArgs e)
        {
            Continue.Visibility = Visibility.Collapsed;
            Restart.Visibility = Visibility.Collapsed;
            Stop.Visibility = Visibility.Collapsed;
            Start.Visibility = Visibility.Visible;
            Random.Visibility = Visibility.Visible;
            isEvolving = false;
            
            await Initializer.unaliveElements(theGrid, arrGen);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            stop();
            isEvolving = false;
        }
        
        private async void Continue_Click(object sender, RoutedEventArgs e)
        {
            Restart.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Start.Visibility = Visibility.Collapsed;
            Continue.Visibility = Visibility.Collapsed;
            ClickMove.Visibility = Visibility.Collapsed;
            isEvolving = true;
            
            await Game.Evolve(theGrid, arrGen);
        }
        
        private void Random_OnClick_Click(object sender, RoutedEventArgs e)
        {
            Initializer.aliveRandomElements(theGrid, arrGen);
            Random.Visibility = Visibility.Collapsed;
        }
        
        private void ClickMove_OnClick(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            Game.CalculateNextState(theGrid, arrGen);
            Game.evolveOnce(theGrid, random);
        }
        
        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            Stop_Click(sender, e);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TXT files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;
                Initializer.saveCondition(theGrid, arrGen, path);
            }
        }
        
        private void Load_OnClick(object sender, RoutedEventArgs e)
        {
            Stop_Click(sender, e);
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                Initializer.loadCondition(theGrid, arrGen, path);
            }
        }
        
        
        
        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            Random random = new Random();
            
            Grid square = (Grid)sender;
            int row = Grid.GetRow(square);
            int column = Grid.GetColumn(square);
            
            if (!isEvolving && square.Tag == "dead")
            {
                square.Tag = "alive";
                arrGen[row, column] = 1;
                square.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(random.Next(255)),
                    Convert.ToByte(random.Next(255)), Convert.ToByte(random.Next(255))));
            }
            else if (!isEvolving && square.Tag == "alive")
            {
                arrGen[row, column] = 0;
                square.Tag = "dead";
                square.Background = new SolidColorBrush(Colors.White);
            }
        }
        
        private void R0c0_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Grid square = (Grid)sender;
            int row = Grid.GetRow(square);
            int column = Grid.GetColumn(square);
            var x = e.GetPosition(this).X;
            var y = e.GetPosition(this).Y;

            if (square.Tag == "alive")
            {
                Canvas.SetLeft(Box, x);
                Canvas.SetTop(Box, y);
                Box.Content = $"Поколение: {arrGen[row, column]}";
                Box.Visibility = Visibility.Visible;
            }
            /*else
            {
                Canvas.SetLeft(Box, x);
                Canvas.SetTop(Box, y);
                Box.Content = "Поколение: -1";
                Box.Visibility = Visibility.Visible;
            }*/
        }

        private void R0c0_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Box.Visibility = Visibility.Collapsed;
        }

        
        
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            isEvolving = false;
            
            if (MessageBox.Show("Сохранить текущее состояние? При следующем запуске оно будет восстановлено.",
                    "Save file",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Initializer.saveCondition(theGrid, arrGen, @"..\..\Files\Condition.txt");
                MessageBox.Show("Файл сохранён. При следующем запуске будет загружено текущее состояние.");
            }
        }
    }
}