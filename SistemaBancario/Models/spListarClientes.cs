using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBancario.Models
{
    public class spListarClientes
    {
        public int COD_CLIENTE { get; set; } 
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string DIRECCION { get; set; }
        public DateTime FECHA_NAC { get; set; }
        public string NUM_IDENTIDAD { get; set; }
        public string ESTADO { get; set; }
        public int COD_PERSONA { get; set; }
    }
}