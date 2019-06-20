using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Soko
{
    /// <summary>
    /// Логика взаимодействия для HelloWindow.xaml
    /// </summary>
    public partial class HelloWindow : Window
    {
        MediaPlayer player = new MediaPlayer();

        public HelloWindow()

        {
            InitializeComponent();
            player.Open(new Uri(@"background.wav", UriKind.Relative));
            player.Play();
            player.Volume = 40.0 / 100.0;

        }

        private void newgame_Click(object sender, RoutedEventArgs e)

        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
            player.Stop();
        }

        private void exit1_Click(object sender, RoutedEventArgs e)

        {
            this.Close();
        }

        private void loadmap_Click(object sender, RoutedEventArgs e)
        {
            MapEditor mapeditor = new MapEditor();
            mapeditor.Show();
        }

    }
}
