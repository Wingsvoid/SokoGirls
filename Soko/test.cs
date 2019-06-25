using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using NUnit.Framework;

namespace Soko
{
   
    [TestFixture]
    class test
    {

        [TestCase]
        public void CellIsBusy()
        {
            //проверка того, что клетка с координатами, равными координатам игрока, считается занятой
            Map currentMap = new Map(20, 20);
            Assert.IsTrue(currentMap.GetCell(currentMap.playerRed.xPos, currentMap.playerRed.yPos).isBusy);
        }
   
        [TestCase]
        public void NearCell()
        {
            //проверка того, что клетка, находящаяся в какой-либо стороне от выбранной, имеет верные координаты
            Map currentMap = new Map(20, 20);
            Assert.AreEqual(currentMap.GetCell(0, 1), currentMap.GetNearCell(currentMap.GetCell(0, 0), Creature.Direction.Down));
            
        }
     

    }
}
