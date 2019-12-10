using System;
using System.Globalization;

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
        /// El rut puede estar en mayuscula, minuscula y contener puntos.
        /// </summary>
        /// <param name="rut">Ejemplo: 123456789-0</param>
        /// <returns></returns>
        public static bool ValidarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = LimpiaRut(rut).Replace("-","");
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
        /// <returns>Devuelve el digito verificador, ejemplo: 1,2,3,K</returns>
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
        /// <returns>El resultado es de la forma 123456-D</returns>
        public static string GeneraRut(int desde=1,int hasta=99999999) 
        {
            string rut;
            var ran=new Random();
            string num=ran.Next(desde,hasta).ToString();
            rut=num+"-"+ObtenerDV(num);
            return rut;
        }
        
        /// <summary>
        /// Convierte un rut en un rut con/sin largo determinado (ceros a la izquierda)
        /// , con/sin separador de miles y con/sin digito verificador
        /// </summary>
        /// <param name="rut">Rut a procesar. Puede tener espacio, puntos pero debe tener el digito verificador</param>
        /// <param name="largo">Si es cero entonces no se especifica un largo. Si no es cero, se llena de "0" a la izquierda</param>
        /// <param name="separador">Si incluye o no el separador de miles</param>
        /// <param name="dv">Si incluye o no el digito verificador</param>
        /// <returns></returns>
        public static string ConvierteTipoRut(string rut,int largo=0,bool separador=true,bool dv=true) 
        {
     
            rut=LimpiaRut(rut); // limpiamos el rut
            var partes=rut.Split(new[] {'-'},StringSplitOptions.None);
            if(partes.Length!=2)
            {
                return null; // rut invalido no tiene separador.
            }

            string numS = partes[0];
            int num;
            if (!int.TryParse(numS, out num))
            {
                return null; // rut invalido, no contiene numeros.
            }
            if (separador)
            {
                 num=1000000000+num;
                numS=num.ToString("N0",CultureInfo.InvariantCulture).Replace(',','.'); // por defecto invariant usa comas
            }
            if(largo>0)
            {
                if(largo>numS.Length)
                {
                    return null; // rut invalido, resultado mas lrgo que largo indicado
                }
                numS=numS.Substring(numS.Length-largo);
            }
            if(dv)
            {
                numS=numS+"-"+partes[1];
            }

            return numS;
        }
        /// <summary>
        /// Limpia un rut de puntos, espacios al comienzo y final y lo devuelve en mayuscula 
        /// y con el digito verificador
        /// </summary>
        /// <param name="rut">Rut a convertir. Ejemplo 123.456-k -> 123456-K</param>
        /// <returns></returns>
        public static string LimpiaRut(string rut)
        {            
            return rut.ToUpper().Trim().Replace(".","");
        }
    }
}
