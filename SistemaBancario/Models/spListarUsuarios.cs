using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBancario.Models
{
    public class spListarUsuarios
    {
        public int COD_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string PASS { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string IDENTIDAD { get; set; }
        public int COD_ROL { get; set; }
        public string ROL { get; set; }
        public string ESTADO_USER { get; set; }
    }
}