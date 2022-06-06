namespace SistemaBancario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_CLIENTES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_CLIENTES()
        {
            TBL_CUENTAS_BANCARIAS = new HashSet<TBL_CUENTAS_BANCARIAS>();
        }

        [Key]
        public int COD_CLIENTE { get; set; }

        public int? COD_PERSONA { get; set; }

        public string ESTADO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_CUENTAS_BANCARIAS> TBL_CUENTAS_BANCARIAS { get; set; }

        public virtual TBL_PERSONAS TBL_PERSONAS { get; set; }
    }
}
