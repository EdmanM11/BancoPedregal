namespace SistemaBancario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_TIPO_CUENTA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_TIPO_CUENTA()
        {
            TBL_CUENTAS_BANCARIAS = new HashSet<TBL_CUENTAS_BANCARIAS>();
        }

        [Key]
        public int COD_TIP_CUENTA { get; set; }

        [StringLength(30)]
        public string TIPO_CUENTA { get; set; }

        [StringLength(30)]
        public string ESTADO_CUENTA { get; set; }

        public string CODIGO_CUENTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_CUENTAS_BANCARIAS> TBL_CUENTAS_BANCARIAS { get; set; }
    }
}
