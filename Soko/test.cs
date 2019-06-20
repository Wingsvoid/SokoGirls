using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Soko
{
   
    [TestFixture]
    class test
    {
        functions f = new functions();


        [TestCase]
            public void Add()
            {
                Assert.AreEqual(33, f.add(3, 30));
            }

        [TestCase]
        public void CalculateCellSize()
        {
            Assert.AreEqual(4, f.CalculateCellSize(2, 2));


        }


    }
}
