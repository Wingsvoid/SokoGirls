using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Soko
{
    class Map
    {
        private int height;
        private int width;
        public List<Cell> grid;
        public Creature playerRed;
        public Creature playerBlue;
        public Creature chestRed;
        public Creature chestBlue;

        public int Height
        {
            get
            {
                return height;
            }
        }
        public int Width
        {
            get
            {
                return width;
            }
        }

        // создание карты исходя из ширины и высоты
        public Map(int h, int w)
        {
            height = h;
            width = w;
            grid = new List<Cell>();
            //playerRed = new Creature(0, 0);
            //playerBlue = new Creature(1, 0);
            //chestRed = new Creature(0, 1);
            //chestBlue = new Creature(1, 1);
            for (int rownumber=0; rownumber < height; rownumber++)
            {
                for (int colnumber=0; colnumber < width; colnumber++)
                {
                    grid.Add(new Cell(colnumber, rownumber, Cell.cellType.Open));
                }
            }
            //GetCell(playerRed.xPos, playerRed.yPos).setObject(playerRed);
            //GetCell(playerBlue.xPos, playerBlue.yPos).setObject(playerBlue);
            //GetCell(chestRed.xPos, chestRed.yPos).setObject(chestRed);
            //GetCell(chestBlue.xPos, chestBlue.yPos).setObject(chestBlue);
            //GetCell(0, 2).Type = Cell.cellType.RedFinish;
            //GetCell(1, 2).Type = Cell.cellType.BlueFinish;
        }

        // создание карты исходя из массива координат открытых клеток
        public Map(int[,] mapArr)
        {
            height = 28;
            width = 25;
            grid = new List<Cell>();
            playerRed = new Creature(2, 16);
            playerBlue = new Creature(11, 16);
            chestRed = new Creature(9, 22);
            chestBlue = new Creature(5, 25);

            // генерация поля со всеми "закрытыми" клетками
            for (int rownumber = 0; rownumber < height; rownumber++)
            {
                for (int colnumber = 0; colnumber < width; colnumber++)
                {
                    grid.Add(new Cell(colnumber, rownumber, Cell.cellType.Close));
                }
            }

            // "открытие" клеток по координатам ( i номер строки )
            int p = mapArr.Length;
            for (int i = 0; i < (mapArr.Length/2); i++) // mapArr.Length == количество строк * количество элементов строки
            {
                int x = mapArr[i, 0];
                int y = mapArr[i, 1];
                GetCell(x, y).Type = Cell.cellType.Open;
            }

            GetCell(playerRed.xPos, playerRed.yPos).setObject(playerRed);
            GetCell(playerBlue.xPos, playerBlue.yPos).setObject(playerBlue);
            GetCell(chestRed.xPos, chestRed.yPos).setObject(chestRed);
            GetCell(chestBlue.xPos, chestBlue.yPos).setObject(chestBlue);
            GetCell(23, 3).Type = Cell.cellType.RedFinish;
            GetCell(23, 4).Type = Cell.cellType.BlueFinish;

           SaveToXml(); 
        }

        // создание карты исходя из содержимого Xml-документа
        public Map(XmlDocument xDoc)
        {
            height = 0;
            width = 0;
            grid = new List<Cell>();
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xbranch in xRoot)
            {
                if (xbranch.Name == "params")
                {
                    foreach (XmlElement xseed in xbranch.ChildNodes)
                    {
                        if (xseed.Name == "height")
                        {
                            Int32.TryParse(xseed.InnerText, out height);
                        }
                        if (xseed.Name == "width")
                        {
                            Int32.TryParse(xseed.InnerText, out width);
                        }
                    }
                }
                if (xbranch.Name == "cells")
                {
                    foreach (XmlElement xleaf in xbranch.ChildNodes)
                    {
                        int _xPos = 0;
                        int _yPos = 0;
                        Cell.cellType _type = Cell.cellType.Close;
                        foreach (XmlElement xseed in xleaf.ChildNodes)
                        {
                            if (xseed.Name == "xPos")
                            {
                                Int32.TryParse(xseed.InnerText, out _xPos);
                            }
                            if (xseed.Name == "yPos")
                            {
                                Int32.TryParse(xseed.InnerText, out _yPos);
                            }
                            if (xseed.Name == "Type")
                            {

                                // список типов клеток, добавлять сюда новые названия клеток 
                                switch (xseed.InnerText)
                                {
                                    case "Open":
                                        _type = Cell.cellType.Open;
                                        break;
                                    case "Close":
                                        _type = Cell.cellType.Close;
                                        break;
                                    case "RedFinish":
                                        _type = Cell.cellType.RedFinish;
                                        break;
                                    case "BlueFinish":
                                        _type = Cell.cellType.BlueFinish;
                                        break;
                                    default:
                                        _type = Cell.cellType.Close;
                                        break;
                                }
                            }

                        }
                        grid.Add(new Cell(_xPos, _yPos, _type));
                    }
                }

                if (xbranch.Name == "objects")
                {
                    foreach (XmlElement xleaf in xbranch.ChildNodes)
                    {
                        int _xPos = 0;
                        int _yPos = 0;
                        foreach (XmlElement xseed in xleaf.ChildNodes)
                        {
                            if (xseed.Name == "xPos")
                            {
                                Int32.TryParse(xseed.InnerText, out _xPos);
                            }
                            if (xseed.Name == "yPos")
                            {
                                Int32.TryParse(xseed.InnerText, out _yPos);
                            }
                        }

                        switch (xleaf.Name)
                        {
                            case "Red_Player":
                                playerRed = new Creature(_xPos, _yPos);
                                break;
                            case "Blue_Player":
                                playerBlue = new Creature(_xPos, _yPos);
                                break;
                            case "Red_Chest":
                                chestRed = new Creature(_xPos, _yPos);
                                break;
                            case "Blue_Chest":
                                chestBlue = new Creature(_xPos, _yPos);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            GetCell(playerRed.xPos, playerRed.yPos).setObject(playerRed);
            GetCell(playerBlue.xPos, playerBlue.yPos).setObject(playerBlue);
            GetCell(chestRed.xPos, chestRed.yPos).setObject(chestRed);
            GetCell(chestBlue.xPos, chestBlue.yPos).setObject(chestBlue);
            BuildWalls();
        }

        public void BuildWalls()
        {
            foreach (Cell cell in grid)
            {
                if (cell.Type == Cell.cellType.Close)
                {
                    if (GetNearCell(cell, Creature.Direction.Down).Type == Cell.cellType.Open)
                    {
                        cell.Type = Cell.cellType.TopWall;
                    }
                    else if ((GetNearCell(cell, Creature.Direction.Up).Type == Cell.cellType.Open) &&
                        (GetNearCell(cell, Creature.Direction.Right).Type == Cell.cellType.Open))
                    {
                        cell.Type = Cell.cellType.LeftCorner;
                    }
                    else if ((GetNearCell(cell, Creature.Direction.Up).Type == Cell.cellType.Open) &&
                        (GetNearCell(cell, Creature.Direction.Left).Type == Cell.cellType.Open))
                    {
                        cell.Type = Cell.cellType.RightCorner;
                    }
                    else if(GetNearCell(cell, Creature.Direction.Up).Type == Cell.cellType.Open)
                    {
                        cell.Type = Cell.cellType.BottomWall;
                    }
                    else if ((GetNearCell(cell, Creature.Direction.Right).Type == Cell.cellType.Open)||
                        (GetNearCell(cell, Creature.Direction.Right).Type == Cell.cellType.TopWall))
                    {
                        cell.Type = Cell.cellType.LeftWall;
                    }
                    else if ((GetNearCell(cell, Creature.Direction.Left).Type == Cell.cellType.Open)||
                        (GetNearCell(cell, Creature.Direction.Left).Type == Cell.cellType.TopWall))
                    {
                        cell.Type = Cell.cellType.RightWall;
                    }
                    else if (((GetNearCell(cell, Creature.Direction.Right).Type == Cell.cellType.Open) || (GetNearCell(cell, Creature.Direction.Right).Type == Cell.cellType.TopWall)) &&
                        ((GetNearCell(cell, Creature.Direction.Left).Type == Cell.cellType.Open) || (GetNearCell(cell, Creature.Direction.Left).Type == Cell.cellType.TopWall)))
                    {
                        cell.Type = Cell.cellType.LeftRightWall;
                    }
                }
            }
        }

        public Cell GetCell(int x, int y) // ищет клетку по её координатам и возвращает её
        {
            return grid.Find(z => z.xPos == x && z.yPos == y);
        }

        public Cell GetNearCell(Cell currentCell, Creature.Direction direction) 
            //возвращает клетку, которая находится в выбранном направлении от текущей клетки
        {
            Cell near;
            switch(direction)
            {
                case Creature.Direction.Right:
                    near =  grid.Find(z => z.xPos == currentCell.xPos+1 && z.yPos == currentCell.yPos);
                    break;
                case Creature.Direction.Left:
                    near =  grid.Find(z => z.xPos == currentCell.xPos-1 && z.yPos == currentCell.yPos);
                    break;
                case Creature.Direction.Up:
                    near =  grid.Find(z => z.xPos == currentCell.xPos && z.yPos == currentCell.yPos-1);
                    break;
                case Creature.Direction.Down:
                    near =  grid.Find(z => z.xPos == currentCell.xPos && z.yPos == currentCell.yPos+1);
                    break;
                default:
                    near =  grid.Find(z => z.xPos == currentCell.xPos && z.yPos == currentCell.yPos);
                    break;
            }
            if (near is null) return currentCell;
            else return near;
        }
        public void MoveTo(Creature player, Creature.Direction direction)
            //перемещает игрока в выбранном направлении
        {
            Cell currentCell = GetCell(player.xPos, player.yPos);
            Cell nextCell = GetNearCell(currentCell, direction);

            if ((nextCell != null)&&(player.currentState!=Creature.State.Moving)) // если следующая клетка существует и игрок не двигается, то...
            {
                if ((nextCell.Type == Cell.cellType.Open) || 
                    (nextCell.Type == Cell.cellType.RedFinish)||
                    (nextCell.Type == Cell.cellType.BlueFinish))
                {
                    if (nextCell.isBusy) //если следующая клетка занята кем-то, то переместить его в том же направлении
                    {
                        MoveTo(nextCell.getObject(), direction); //запустить функцию перемещения для объекта, стоящего на следующей клетке
                        if(!nextCell.isBusy) //если после смещения объекта со следующей клетки она освободилась, то спокойно перемещаемся на нее
                        {
                            player.MovingTo(direction);
                            nextCell.setObject(player);
                            player.xPos = nextCell.xPos;
                            player.yPos = nextCell.yPos;
                            currentCell.clearCell();
                        }
                    }
                    else //если свободна, то спокойно перемещаемся
                    {
                        player.MovingTo(direction); //записать в информацию игрока направление его движения и состояние движения
                        nextCell.setObject(player); //записать игрока в следующую клетку
                        player.xPos = nextCell.xPos; //записать координаты следующей клетки в координаты игрока
                        player.yPos = nextCell.yPos;
                        currentCell.clearCell(); //освободить текущую клетку
                    }
                }
            }
        }

        // сохранение карты в Xml-документ
        public void SaveToXml()
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement xRoot = xDoc.CreateElement("map");
            XmlElement xbranch;
            XmlElement xleaf;
            XmlElement xseed;

            //сохранение раздела параметров карты
            xbranch = xDoc.CreateElement("params");

            xseed = xDoc.CreateElement("height");
            xseed.InnerText = height.ToString();
            xbranch.AppendChild(xseed);

            xseed = xDoc.CreateElement("width");
            xseed.InnerText = width.ToString();
            xbranch.AppendChild(xseed);
            xRoot.AppendChild(xbranch);

            //сохранение клеток поля
            xbranch = xDoc.CreateElement("cells");
            foreach (Cell _cell in grid)
            {
                xleaf = xDoc.CreateElement("cell");
                xseed = xDoc.CreateElement("xPos");
                xseed.InnerText = _cell.xPos.ToString();
                xleaf.AppendChild(xseed);
                xseed = xDoc.CreateElement("yPos");
                xseed.InnerText = _cell.yPos.ToString();
                xleaf.AppendChild(xseed);
                xseed = xDoc.CreateElement("Type");
                xseed.InnerText = _cell.Type.ToString();
                xleaf.AppendChild(xseed);
                xbranch.AppendChild(xleaf);
            }
            xRoot.AppendChild(xbranch);

            //сохранение позиции игроков
            xbranch = xDoc.CreateElement("objects");

            xleaf = xDoc.CreateElement("Red_Player");
            xseed = xDoc.CreateElement("xPos");
            xseed.InnerText = playerRed.xPos.ToString();
            xleaf.AppendChild(xseed);
            xseed = xDoc.CreateElement("yPos");
            xseed.InnerText = playerRed.yPos.ToString();
            xleaf.AppendChild(xseed);
            xbranch.AppendChild(xleaf);


            xleaf = xDoc.CreateElement("Blue_Player");
            xseed = xDoc.CreateElement("xPos");
            xseed.InnerText = playerBlue.xPos.ToString();
            xleaf.AppendChild(xseed);
            xseed = xDoc.CreateElement("yPos");
            xseed.InnerText = playerBlue.yPos.ToString();
            xleaf.AppendChild(xseed);
            xbranch.AppendChild(xleaf);


            xleaf = xDoc.CreateElement("Red_Chest");
            xseed = xDoc.CreateElement("xPos");
            xseed.InnerText = chestRed.xPos.ToString();
            xleaf.AppendChild(xseed);
            xseed = xDoc.CreateElement("yPos");
            xseed.InnerText = chestRed.yPos.ToString();
            xleaf.AppendChild(xseed);
            xbranch.AppendChild(xleaf);


            xleaf = xDoc.CreateElement("Blue_Chest");
            xseed = xDoc.CreateElement("xPos");
            xseed.InnerText = chestBlue.xPos.ToString();
            xleaf.AppendChild(xseed);
            xseed = xDoc.CreateElement("yPos");
            xseed.InnerText = chestBlue.yPos.ToString();
            xleaf.AppendChild(xseed);
            xbranch.AppendChild(xleaf);

            xRoot.AppendChild(xbranch);

            xDoc.AppendChild(xRoot);
            xDoc.PreserveWhitespace = true;
            string xPath = "data.xml";
            xDoc.Save(xPath);
        }

        public bool isWinnable()
        {
            Cell redCell = grid.Find(z => z.Type == Cell.cellType.RedFinish);
            Cell blueCell = grid.Find(z => z.Type == Cell.cellType.BlueFinish);
            if (redCell.getObject() == chestRed && blueCell.getObject() == chestBlue)
            {
                return true;
            }

            else
            {
                return false;
            }

        }
    }
}
