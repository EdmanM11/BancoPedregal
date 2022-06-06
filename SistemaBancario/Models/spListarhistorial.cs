using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBancario.Models
{
    public class spListarhistorial
    {
        public int COD_CUENTA { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string TIPO_CUENTA { get; set; }
        public string TIPO_TRANSACCION { get; set; }
        public decimal MONTO { get; set; }
        public DateTime FECHA { get; set; }
        public decimal MOVIMIENTO { get; set; }
        public string USUARIO_TRA { get; set; }
    }
}