using System;
namespace EFTEC
{
    /// <summary>
    /// Copyright Jorge Castro C. - https://www.eftec.cl
    /// MIT License. (Puede usarlo comercialmente si no borra el copyright)
    /// </summary>
    public class RutChile
    {
        /// <summary>
        /// Valida un rut y devuelve true si el rut es valido. En otro caso, devuelve False.
        /// El rut puede estar en mayuscula, minuscula y contener espacio.
        /// </summary>
        /// <param name="rut">Ejemplo: 123456789-0</param>
        /// <returns></returns>
        public static bool ValidarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper().Trim().Replace("-","").Replace(".","");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));
                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));
                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return validacion;
        }
        /// <summary>
        /// Obtiene el digito verificador de un rut incompleto.
        /// En caso de error, devuelve un "?"
        /// </summary>
        /// <param name="rut">El rut no debe contener el digito verificador</param>
        /// <returns></returns>
        public static string ObtenerDV(string rut)
        {
            string dv;
            try
            {
                rut = rut.ToUpper().Trim().Replace("-", "").Replace(".", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length));
                
                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                dv=((char)(s != 0 ? s + 47 : 75)).ToString();
            }
            catch (Exception)
            {
                return "?";
            }
            return dv;
        }
        /// <summary>
        /// Genera un rut al azar con digito verificador.
        /// </summary>
        /// <param name="desde">Numero inicial</param>
        /// <param name="hasta">Numero final</param>
        /// <returns></returns>
        public static string GeneraRut(int desde=1,int hasta=99999999) 
        {
            string rut;
            var ran=new Random();
            string num=ran.Next(desde,hasta).ToString();
            rut=num+"-"+ObtenerDV(num);
            return rut;
        }
    }
}
