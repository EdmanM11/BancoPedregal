using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Base64Url;

namespace SistemaBancario.Controllers
{
    public class CodigoURLController : Controller
    {
        //Codifica la cadena recibida
        public string Encode32(string Variable)
        {
            var Encript = Base64.GetBase64(Variable);
            //var Encript = Base32Url.ToBase32String(Encoding.ASCII.GetBytes(Variable));
            return Encript;
        }

        //Decodifica la cadena encriptada y la convierte en string
        public string Decode32(string Variable)
        {

            /*byte[] decodificado = Base32Url.FromBase32String(Variable);
            for(int i=0; i<decodificado.Count();i++)
            {
                Cadena += Convert.ToString(Encoding.Default.GetString(new byte[] { decodificado[i] }));
            }*/

            string Cadena = Base64.ToString(Variable);
            return Cadena;
        }
    }
}