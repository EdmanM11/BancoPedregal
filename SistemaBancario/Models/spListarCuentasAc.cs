using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBancario.Models
{
    public class spListarCuentasAc
    {
        public int COD_TIP_CUENTA { get; set; }
        public string TIPO_CUENTA { get; set; }
        public string ESTADO_CUENTA { get; set; }
        public string CODIGO_CUENTA { get; set; }
    }
}