using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBancario.Models
{
    public class spListaCuentaCod
    {
            public int COD_CUENTA { get; set; }
            public int NUMERO_CTA { get; set; }
            public string NOMBRE { get; set; }
            public string APELLIDO { get; set; }
            public string TIPO_CUENTA { get; set; }
            public decimal SALDO { get; set; }     
    }
}