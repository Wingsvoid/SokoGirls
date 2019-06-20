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

namespace Soko
{
    /// <summary>
    /// Логика взаимодействия для MapEditor.xaml
    /// </summary>
    public partial class MapEditor : Window
    {
        

        public MapEditor()
        {
            InitializeComponent();

            ////указыается количество строк и столбцов в сетке
            //head.Rows = 4;
            //head.Columns = 1;
            ////указываются размеры сетки (число ячеек * (размер кнопки в ячейки + толщина её границ))
            //head.Width = 4 * (50 + 4);
            //head.Height = 4 * (50 + 4);
            ////толщина границ сетки
            //head.Margin = new Thickness(5, 5, 5, 5);


            //for (int i = 0; i < 2 * 2; i++)
            //{
            //    //создание кнопки
            //    Button btn = new Button();
            //    //запись номера кнопки
            //     btn.Tag = i;
            //    //установка размеров кнопки
            //    btn.Width = 50;
            //     btn.Height = 50;
            //    //текст на кнопке
            //    btn.Content = " ";
            //    //толщина границ кнопки
            //    btn.Margin = new Thickness(2);
            //    //при нажатии кнопки, будет вызываться метод Btn_Click
            //     btn.Click += btn_Click;
            //    //добавление кнопки в сетку
            //    head.Children.Add(btn);


               


                
            //}



            //указыается количество строк и столбцов в сетке
            head.Rows = 15;
            head.Columns = 25;
            //указываются размеры сетки (число ячеек * (размер кнопки в ячейки + толщина её границ))
            head.Width = 25 * (50 + 4);
            head.Height = 15 * (50 + 4);
            //толщина границ сетки
            head.Margin = new Thickness(5, 5, 5, 5);


            for (int i = 0; i < 15 * 25; i++)
            {
                //создание кнопки
                Button btn = new Button();
                //запись номера кнопки
                btn.Tag = i;
                //установка размеров кнопки
                btn.Width = 50;
                btn.Height = 50;
                //текст на кнопке
                btn.Content = " ";
                //толщина границ кнопки
                btn.Margin = new Thickness(2);
                //при нажатии кнопки, будет вызываться метод Btn_Click
                btn.MouseDown += btn_MouseDown;
                //добавление кнопки в сетку
                head.Children.Add(btn);
                                                                      
            }

            
        }

        private void btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
          //  btn.PreviewMouseDown += btn_MouseDown;

            int n = (int)((Button)sender).Tag;

            BitmapImage opencell = new BitmapImage(new Uri(@"pack://application:,,,/tiles2/floor.png", UriKind.Absolute));
            BitmapImage closecell = new BitmapImage(new Uri(@"pack://application:,,,/tiles/floor_stop2.png", UriKind.Absolute));
            BitmapImage finishcell = new BitmapImage(new Uri(@"pack://application:,,,/tiles/floor_red.jpg", UriKind.Absolute));

            MessageBox.Show(e.LeftButton.ToString());

            if (e.LeftButton == MouseButtonState.Pressed)
            {

                //MessageBox.Show("ПРИВЕТ");
                Image img1 = new Image();
                img1.Source = opencell;
                StackPanel stackPnl1 = new StackPanel();
                stackPnl1.Margin = new Thickness(1);
                stackPnl1.Children.Add(img1);
                ((Button)sender).Content = stackPnl1;
            }
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Image img3 = new Image();
                img3.Source = finishcell;
                StackPanel stackPnl3 = new StackPanel();
                stackPnl3.Margin = new Thickness(1);
                stackPnl3.Children.Add(img3);
                ((Button)sender).Content = stackPnl3;
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Image img2 = new Image();
                img2.Source = closecell;
                StackPanel stackPnl2 = new StackPanel();
                stackPnl2.Margin = new Thickness(1);
                stackPnl2.Children.Add(img2);
                ((Button)sender).Content = stackPnl2;
            }
        }
    }
}
