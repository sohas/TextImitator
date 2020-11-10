using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using static Generator.Generator;
using static Generator.Letters;
using Generator;

namespace Teximitator.Test
{
    [TestClass]
    public class TextimitatorTest
    {
        [TestMethod]
        public void TestIsInputNun()
        {
            var c1 = " ";
            var result1 = false;
            var c2 = "1hk";
            var result2 = false;
            var c3 = "012";
            var result3 = false;
            var c4 = "1";
            var result4 = true;
            var c5 = "125";
            var result5 = true;

            Assert.AreEqual(result1, c1.IsInputNum());
            Assert.AreEqual(result2, c2.IsInputNum());
            Assert.AreEqual(result3, c3.IsInputNum());
            Assert.AreEqual(result4, c4.IsInputNum());
            Assert.AreEqual(result5, c5.IsInputNum());
        }

        //[TestMethod]
        //public void TestIsGoodLetter()
        //{
        //    var c1 = 'a';
        //    var result1 = false;
        //    var c2 = 'ф';
        //    var result2 = true;
        //    var c3 = 'р';
        //    var result3 = true;
        //    var c4 = '1';
        //    var result4 = false;
        //    var c5 = '%';
        //    var result5 = false;

        //    Assert.AreEqual(result1, c1.IsGoodSymbol(L));
        //    Assert.AreEqual(result2, c2.IsGoodSymbol(L));
        //    Assert.AreEqual(result3, c3.IsGoodSymbol(L));
        //    Assert.AreEqual(result4, c4.IsGoodSymbol(L));
        //    Assert.AreEqual(result5, c5.IsGoodSymbol(L));
        //}

        //[TestMethod]
        //public void TestFindSample()
        //{
        //    var t1 = new StringBuilder("абвг");
        //    var s1 = 2;
        //    var sh1 = 2;

        //    var result1 = "бв";

        //    Assert.AreEqual(result1, FindSample(t1, s1, sh1));
        //}

        //[TestMethod]
        //public void TestGetNumberLetter()
        //{
        //    var a1 = new double[4] {0.1, 1.98, 1.98, 2.00};
        //    var r1 = 0.99;
            
        //    var result1 = 1;

        //    Assert.AreEqual(result1, GetNumberLetter(r1, a1));
        //}

        //[TestMethod]
        //public void TestGetLetter()
        //{
        //    var a1 = new double[4] { 0.00, 1.00, 2.00, 2.00 };

        //    var result1 = 'а';

        //    Assert.AreEqual(result1, GetLetter(a1));
        //}






        //[TestMethod]
        //public void TestSB()
        //{
        //    var c1 = "            ";
        //    var result1 = " ";
        //    var c2 = "а";
        //    var result2 = "а";
        //    var c3 = "а 45 п";
        //    var result3 = "а п";
        //    var c4 = "1";
        //    var result4 = " ";
        //    var c5 = "22";
        //    var result5 = " ";

        //    Assert.AreEqual(result1, GGG(c1));
        //    //Assert.AreEqual(result2, GGG(c2));
        //    //Assert.AreEqual(result3, GGG(c3));
        //    //Assert.AreEqual(result4, GGG(c4));
        //    //Assert.AreEqual(result5, GGG(c5));

        //}




    }
}
