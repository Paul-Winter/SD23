using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Albert_PiTPM_Practica;

namespace UnitTestProject4
{
    [TestClass]
    public class UnitTest1
    {
        Summa summa = new Summa();
        [TestMethod]
        public void MethodSlojenia_aPlusb_return45Test()
        {
                      
            summa.Slojenie();

            Assert.AreEqual(45, summa.Slojenie());
        }
    }
}
