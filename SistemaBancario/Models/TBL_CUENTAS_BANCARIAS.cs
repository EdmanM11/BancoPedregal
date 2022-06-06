namespace SistemaBancario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_CUENTAS_BANCARIAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_CUENTAS_BANCARIAS()
        {
            TBL_HISTORIAL_TRANSACCIONES = new HashSet<TBL_HISTORIAL_TRANSACCIONES>();
        }

        [Key]
        public int COD_CUENTA { get; set; }

        public int? COD_CLIENTE { get; set; }

        public int? COD_TIP_CUENTA { get; set; }

        [StringLength(30)]
        public string ESTADO { get; set; }

        public decimal? SALDO { get; set; }

        public int NUMERO_CTA { get; set; }

        public virtual TBL_CLIENTES TBL_CLIENTES { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_HISTORIAL_TRANSACCIONES> TBL_HISTORIAL_TRANSACCIONES { get; set; }

        public virtual TBL_TIPO_CUENTA TBL_TIPO_CUENTA { get; set; }
    }
}
