namespace SistemaBancario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_HISTORIAL_TRANSACCIONES
    {
        [Key]
        public int COD_TRANSACCION { get; set; }

        public int? COD_CUENTA { get; set; }

        public DateTime? FECHA { get; set; }

        public decimal? MONTO { get; set; }

        public string TIPO_TRANSACCION { get; set; }

        public decimal MOVIMIENTO { get; set; }

        public string USUARIO_TRA { get; set; }

        public virtual TBL_CUENTAS_BANCARIAS TBL_CUENTAS_BANCARIAS { get; set; }
    }
}
