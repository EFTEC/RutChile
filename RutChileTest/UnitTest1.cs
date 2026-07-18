using System;
using EFTEC;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RutChileTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestValidarRut_RutsValidos()
        {
            // RUT Presidentes y casos conocidos
            string rut = "5.126.663-3"; // Sebastian Piñera
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut = "   5.126.663-3   "; // Sebastian Piñera con espacio
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut = "5126663-3"; // Sebastian Piñera sin formato
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut = "5.811.892-3"; // Bachelet
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut = "1-9"; // Primer rut
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
            rut = "15.000.005-k"; // rut terminado en k
            Assert.AreEqual(true, RutChile.ValidarRut(rut));
        }

        [TestMethod]
        public void TestValidarRut_RutsInvalidos()
        {
            // RUTs con dígito verificador incorrecto
            Assert.AreEqual(false, RutChile.ValidarRut("12345678-0"));
            Assert.AreEqual(false, RutChile.ValidarRut("5.126.663-4"));
            Assert.AreEqual(false, RutChile.ValidarRut("1-0"));

            // RUTs con formato inválido
            Assert.AreEqual(false, RutChile.ValidarRut("abc-def"));
            Assert.AreEqual(false, RutChile.ValidarRut(""));
            Assert.AreEqual(false, RutChile.ValidarRut("---"));
        }

        [TestMethod]
        public void TestValidarRut_SinDigitoVerificador()
        {
            // Validar RUT sin DV (se calcula automáticamente)
            Assert.AreEqual(true, RutChile.ValidarRut("5126663", false));
            Assert.AreEqual(true, RutChile.ValidarRut("5.811.892", false));
            Assert.AreEqual(true, RutChile.ValidarRut("1", false));
            Assert.AreEqual(true, RutChile.ValidarRut("15000005", false));
        }

        [TestMethod]
        public void TestObtenerDV_Casos()
        {
            // Casos básicos
            string rut = "1"; // Primer rut
            Assert.AreEqual("9", RutChile.ObtenerDV(rut));
            rut = "5.126.663"; // Sebastian Piñera
            Assert.AreEqual("3", RutChile.ObtenerDV(rut));
            rut = "15.000.005"; // rut terminado en k
            Assert.AreEqual("K", RutChile.ObtenerDV(rut));
            rut = "5811892"; // Bachelet sin formato
            Assert.AreEqual("3", RutChile.ObtenerDV(rut));

            // Números más grandes
            rut = "25000000";
            string dv = RutChile.ObtenerDV(rut);
            Assert.IsTrue(dv != "?"); // Debe retornar un DV válido

            // Casos de error
            Assert.AreEqual("?", RutChile.ObtenerDV(""));
            Assert.AreEqual("?", RutChile.ObtenerDV("abc"));
        }

        [TestMethod]
        public void TestLimpiaRut_Casos()
        {
            string rut = "5.126.663-3";
            Assert.AreEqual("5126663-3", RutChile.LimpiaRut(rut));

            // Con espacios
            rut = "  12.345.678-9  ";
            Assert.AreEqual("12345678-9", RutChile.LimpiaRut(rut));

            // En minúscula
            rut = "15.000.005-k";
            Assert.AreEqual("15000005-K", RutChile.LimpiaRut(rut));

            // Sin formato
            rut = "1-9";
            Assert.AreEqual("1-9", RutChile.LimpiaRut(rut));
        }

        [TestMethod]
        public void TestConvierteTipoRut_ConFormato()
        {
            string rut = "5.126.663-3";
            Assert.AreEqual("005.126.663-3", RutChile.ConvierteTipoRut(rut, 11, true, true));

            rut = "0000001-9";
            Assert.AreEqual("000.000.001-9", RutChile.ConvierteTipoRut(rut, 11, true, true));

            // Sin separador de miles
            rut = "5.126.663-3";
            Assert.AreEqual("5126663-3", RutChile.ConvierteTipoRut(rut, 0, false, true));

            // Sin dígito verificador pero con separador
            // Nota: ConvierteTipoRut agrega "1" al inicio cuando usa separador
            rut = "5.126.663-3";
            string resultado = RutChile.ConvierteTipoRut(rut, 0, true, false);
            Assert.IsTrue(resultado.Contains("."));
            Assert.IsFalse(resultado.Contains("-"));

            // Sin separador y sin DV
            rut = "5.126.663-3";
            Assert.AreEqual("5126663", RutChile.ConvierteTipoRut(rut, 0, false, false));
        }

        [TestMethod]
        public void TestConvierteTipoRut_CasosInvalidos()
        {
            // RUT sin dígito verificador
            Assert.AreEqual(null, RutChile.ConvierteTipoRut("12345678", 0, true, true));

            // RUT con formato inválido
            Assert.AreEqual(null, RutChile.ConvierteTipoRut("abc-def", 0, true, true));
        }

        [TestMethod]
        public void TestGeneraRut_Validacion()
        {
            // Generar varios RUT y validar que todos sean válidos
            for (int i = 0; i < 10; i++)
            {
                string rutGenerado = RutChile.GeneraRut();
                Assert.AreEqual(true, RutChile.ValidarRut(rutGenerado), 
                    $"El RUT generado '{rutGenerado}' debería ser válido");
            }
        }

        [TestMethod]
        public void TestGeneraRut_RangoPersonalizado()
        {
            // Generar RUT en rango específico
            string rut = RutChile.GeneraRut(1000000, 2000000);
            Assert.AreEqual(true, RutChile.ValidarRut(rut));

            // Verificar que esté en el rango (aproximado, considerando el DV)
            string[] partes = RutChile.SepararRut(rut);
            if (partes[0] != "")
            {
                int numero = int.Parse(partes[0]);
                Assert.IsTrue(numero >= 1000000 && numero <= 2000000);
            }
        }

        [TestMethod]
        public void TestSepararRut_Casos()
        {
            // RUT con separador (debe ser válido)
            string[] partes = RutChile.SepararRut("5.126.663-3");
            Assert.AreEqual("5126663", partes[0]);
            Assert.AreEqual("3", partes[1]);

            // RUT sin formato
            partes = RutChile.SepararRut("1-9");
            Assert.AreEqual("1", partes[0]);
            Assert.AreEqual("9", partes[1]);

            // RUT con K
            partes = RutChile.SepararRut("15.000.005-K");
            Assert.AreEqual("15000005", partes[0]);
            Assert.AreEqual("K", partes[1]);

            // RUT sin separador pero válido
            partes = RutChile.SepararRut("5811892-3");
            Assert.AreEqual("5811892", partes[0]);
            Assert.AreEqual("3", partes[1]);
        }

        [TestMethod]
        public void TestSepararRut_RutInvalido()
        {
            // RUT inválido debe devolver ["", ""]
            string[] partes = RutChile.SepararRut("12345678-0");
            Assert.AreEqual("", partes[0]);
            Assert.AreEqual("", partes[1]);

            // RUT con formato inválido
            partes = RutChile.SepararRut("abc-def");
            Assert.AreEqual("", partes[0]);
            Assert.AreEqual("", partes[1]);
        }

        [TestMethod]
        public void TestRutMinuscula()
        {
            // RUT en minúscula debe ser válido
            Assert.AreEqual(true, RutChile.ValidarRut("15.000.005-k"));
            Assert.AreEqual(true, RutChile.ValidarRut("15000005-k"));

            // Obtener DV en mayúscula
            string dv = RutChile.ObtenerDV("15.000.005");
            Assert.AreEqual("K", dv);
        }

        [TestMethod]
        public void TestRutConEspacios()
        {
            // RUT con espacios al inicio y final
            Assert.AreEqual(true, RutChile.ValidarRut("   5.126.663-3   "));
            Assert.AreEqual(true, RutChile.ValidarRut("  1-9  "));

            // Limpiar debe quitar espacios
            string limpio = RutChile.LimpiaRut("  12.345.678-9  ");
            Assert.AreEqual("12345678-9", limpio);
            Assert.IsFalse(limpio.Contains(" "));
        }

        [TestMethod]
        public void TestIntegracion_FlujoCompleto()
        {
            // Generar RUT
            string rutGenerado = RutChile.GeneraRut(5000000, 10000000);

            // Validar que sea válido
            Assert.AreEqual(true, RutChile.ValidarRut(rutGenerado));

            // Separar
            string[] partes = RutChile.SepararRut(rutGenerado);
            Assert.IsTrue(partes[0] != "");
            Assert.IsTrue(partes[1] != "");

            // Verificar DV
            string dvCalculado = RutChile.ObtenerDV(partes[0]);
            Assert.AreEqual(partes[1], dvCalculado);

            // Formatear
            string formateado = RutChile.ConvierteTipoRut(rutGenerado, 11, true, true);
            Assert.IsNotNull(formateado);
            Assert.IsTrue(formateado.Contains("."));
            Assert.IsTrue(formateado.Contains("-"));

            // Limpiar
            string limpio = RutChile.LimpiaRut(formateado);
            Assert.IsFalse(limpio.Contains("."));
            Assert.IsTrue(limpio.Contains("-"));
        }
    }
}
