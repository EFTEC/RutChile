using System;
using EFTEC;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RutChileTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string rut= "5.126.663-3"; // Sebastian Piñera
            Assert.AreEqual(true,RutChile.ValidarRut(rut));
            rut = "5126663-3"; // Sebastian Piñera otra vez
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut= "5.811.892-3"; // Bachelet
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut = "1-9"; // Primer rut
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut = "1"; // Primer rut
            Assert.AreEqual("9", RutChile.ObtenerDV(rut));
            rut = "5.126.663"; // Sebastian Piñera
            Assert.AreEqual("3", RutChile.ObtenerDV(rut));

            Assert.AreEqual(true, RutChile.ValidarRut(RutChile.GeneraRut()));
            rut= "5.126.663-3";
            Assert.AreEqual("5126663-3",RutChile.LimpiaRut(rut));

            Assert.AreEqual("005.126.663-3",RutChile.ConvierteTipoRut(rut,11,true,true));
            rut= "0000001-9";
            Assert.AreEqual("000.000.001-9",RutChile.ConvierteTipoRut(rut,11,true,true));
        }
    }
}
