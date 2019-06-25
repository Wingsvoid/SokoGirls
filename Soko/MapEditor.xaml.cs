using Microsoft.Win32;
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
using System.Xml;

namespace Soko
{
    /// <summary>
    /// Логика взаимодействия для MapEditor.xaml
    /// </summary>
    public partial class MapEditor : Window
    {
        Map currentMap;
        BitmapImage img_OpenCell;
        BitmapImage img_CloseCell;
        BitmapImage img_RedFinish;
        BitmapImage img_BlueFinish;
        BitmapImage img_Player_Red;
        BitmapImage img_Player_Blue;
        BitmapImage img_Chest_Red;
        BitmapImage img_Chest_Blue;

        Cell.cellType currentType;
        string currentObj;


        int cellsize;

        public MapEditor()
        {
            InitializeComponent();
            img_CloseCell = new BitmapImage(new Uri(@"pack://application:,,,/tiles/floor_stop2.png", UriKind.Absolute));
            img_OpenCell = new BitmapImage(new Uri(@"pack://application:,,,/tiles2/floor.png", UriKind.Absolute));
            img_RedFinish= new BitmapImage(new Uri(@"pack://application:,,,/tiles/floor_red.jpg", UriKind.Absolute));
            img_BlueFinish = new BitmapImage(new Uri(@"pack://application:,,,/tiles/floor_blue.jpg", UriKind.Absolute));
            img_Player_Red = new BitmapImage(new Uri(@"pack://application:,,,/sprites/redgirl.png", UriKind.Absolute));
            img_Player_Blue = new BitmapImage(new Uri(@"pack://application:,,,/sprites/bluegirl.png", UriKind.Absolute));
            img_Chest_Red = new BitmapImage(new Uri(@"pack://application:,,,/sprites/chest_red.png", UriKind.Absolute));
            img_Chest_Blue = new BitmapImage(new Uri(@"pack://application:,,,/sprites/chest_blue.png", UriKind.Absolute));

            //указыается количество строк и столбцов в сетке
            head.Rows = 28;
            head.Columns = 25;
            cellsize = 20;
            //указываются размеры сетки (число ячеек * (размер кнопки в ячейки + толщина её границ))
            head.Width = head.Columns * cellsize;
            head.Height = head.Rows * cellsize;
            

            currentMap = new Map(head.Rows, head.Columns);
            currentType = Cell.cellType.Close;
            currentObj = "";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
            DrawGrid();  
        }

        public void DrawGrid()
        {
            head.Children.Clear();
            for (int i = 0; i < head.Rows * head.Columns; i++)
            {
                //создание кнопки
                Button btn = new Button();
                //запись номера кнопки
                btn.Tag = i;
                //установка размеров кнопки
                btn.Width = cellsize;
                btn.Height = cellsize;
                //текст на кнопке
                btn.Content = "";
                //толщина границ кнопки
                //btn.Margin = new Thickness(2);
                //при нажатии кнопки, будет вызываться метод Btn_Click
                btn.Click += btn_Click;
                switch (currentMap.GetCell(i%head.Columns, i/head.Columns).Type)
                {
                    case Cell.cellType.Open:
                        btn.Content = ConstructImagePanel(img_OpenCell);
                        break;
                    case Cell.cellType.Close:
                        btn.Content = ConstructImagePanel(img_CloseCell);
                        break;
                    case Cell.cellType.RedFinish:
                        btn.Content = ConstructImagePanel(img_RedFinish);
                        break;
                    case Cell.cellType.BlueFinish:
                        btn.Content = ConstructImagePanel(img_BlueFinish);
                        break;
                    default:
                        btn.Content = ConstructImagePanel(img_CloseCell);
                        break;
                }                //добавление кнопки в сетку
                if ((currentMap.playerRed.yPos*head.Columns+currentMap.playerRed.xPos) == i)
                {
                    btn.Content = ConstructImagePanel(img_Player_Red);
                }
                else if ((currentMap.playerBlue.yPos * head.Columns + currentMap.playerBlue.xPos) == i)
                {
                    btn.Content = ConstructImagePanel(img_Player_Blue);
                }
                else if ((currentMap.chestRed.yPos * head.Columns + currentMap.chestRed.xPos) == i)
                {
                    btn.Content = ConstructImagePanel(img_Chest_Red);
                }
                else if ((currentMap.chestBlue.yPos * head.Columns + currentMap.chestBlue.xPos) == i)
                {
                    btn.Content = ConstructImagePanel(img_Chest_Blue);
                }
                head.Children.Add(btn);
            }
        }
        public StackPanel ConstructImagePanel(BitmapImage source)
        {
            Image img1 = new Image();
            img1.Source = source;
            StackPanel stPanel = new StackPanel();
            //stPanel.Margin = new Thickness(1);
            stPanel.Children.Add(img1);
            return stPanel;
        }


        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int n = (int)((Button)sender).Tag;
            //currentMap.GetCell(n % head.Columns, n / head.Columns).Type = currentType;
            if (currentObj != "")
            {
                currentMap.ChangeCellType(currentMap.GetCell(n % head.Columns, n / head.Columns), Cell.cellType.Open);
                if(currentObj == "Player_Red")
                {
                    currentMap.GetCell(currentMap.playerRed.xPos, currentMap.playerRed.yPos).clearCell();
                    currentMap.playerRed.xPos = n % head.Columns;
                    currentMap.playerRed.yPos = n / head.Columns;
                    currentMap.GetCell(currentMap.playerRed.xPos, currentMap.playerRed.yPos).setObject(currentMap.playerRed);
                }
                else if (currentObj == "Player_Blue")
                {
                    currentMap.GetCell(currentMap.playerBlue.xPos, currentMap.playerBlue.yPos).clearCell();
                    currentMap.playerBlue.xPos = n % head.Columns;
                    currentMap.playerBlue.yPos = n / head.Columns;
                    currentMap.GetCell(currentMap.playerBlue.xPos, currentMap.playerBlue.yPos).setObject(currentMap.playerBlue);
                }
                else if (currentObj == "Chest_Red")
                {
                    currentMap.GetCell(currentMap.chestRed.xPos, currentMap.chestRed.yPos).clearCell();
                    currentMap.chestRed.xPos = n % head.Columns;
                    currentMap.chestRed.yPos = n / head.Columns;
                    currentMap.GetCell(currentMap.chestRed.xPos, currentMap.chestRed.yPos).setObject(currentMap.chestRed);
                }
                else if (currentObj == "Chest_Blue")
                {
                    currentMap.GetCell(currentMap.chestBlue.xPos, currentMap.chestBlue.yPos).clearCell();
                    currentMap.chestBlue.xPos = n % head.Columns;
                    currentMap.chestBlue.yPos = n / head.Columns;
                    currentMap.GetCell(currentMap.chestBlue.xPos, currentMap.chestBlue.yPos).setObject(currentMap.chestBlue);
                }
                DrawGrid();
            }
            else
            {
                currentMap.ChangeCellType(currentMap.GetCell(n % head.Columns, n / head.Columns), currentType);
                switch (currentType)
                {
                    case Cell.cellType.Open:
                        ((Button)sender).Content = ConstructImagePanel(img_OpenCell);
                        break;
                    case Cell.cellType.Close:
                        ((Button)sender).Content = ConstructImagePanel(img_CloseCell);
                        break;
                    case Cell.cellType.RedFinish:
                        ((Button)sender).Content = ConstructImagePanel(img_RedFinish);
                        break;
                    case Cell.cellType.BlueFinish:
                        ((Button)sender).Content = ConstructImagePanel(img_BlueFinish);
                        break;
                }
            }
        }

        private void Bluegirl_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.Open;
            currentObj = "Player_Blue";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void Redgirl_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.Open;
            currentObj = "Player_Red";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void Chestblue_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.Open;
            currentObj = "Chest_Blue";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void Chestred_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.Open;
            currentObj = "Chest_Red";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void Floorblue_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.BlueFinish;
            currentObj = "";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void Floorred_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.RedFinish;
            currentObj = "";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void Opencell_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.Open;
            currentObj = "";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void Stopcell_Click(object sender, RoutedEventArgs e)
        {
            currentType = Cell.cellType.Close;
            currentObj = "";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
        }

        private void newMap_Click(object sender, RoutedEventArgs e)
        {
            currentMap = new Map(head.Rows, head.Columns);
            currentType = Cell.cellType.Close;
            currentObj = "";
            lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
            lbl_currentObj.Content = "Выбранный объект: " + currentObj;
            DrawGrid();
        }

        private void saveMap_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Data (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (saveFileDialog.ShowDialog() == true)
            {
                xDoc = currentMap.SaveToXml();
                xDoc.Save(saveFileDialog.FileName);
            }
        }

        private void loadMap_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            string xPath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Data (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                xPath = openFileDialog.FileName;
                xDoc.Load(xPath);
                currentMap = new Map(xDoc);
                currentType = Cell.cellType.Close;
                currentObj = "";
                lbl_currentType.Content = "Текущий тип клетки: " + currentType.ToString();
                lbl_currentObj.Content = "Выбранный объект: " + currentObj;
                DrawGrid();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите выйти?", " ", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                HelloWindow hellowindow = new HelloWindow();
                hellowindow.Show();

                this.Close();
            }
        }
    }
}
