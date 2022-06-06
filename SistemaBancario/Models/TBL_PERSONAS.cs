namespace SistemaBancario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBL_PERSONAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_PERSONAS()
        {
            TBL_CLIENTES = new HashSet<TBL_CLIENTES>();
        }

        [Key]
        public int COD_PERSONA { get; set; }

        [StringLength(20)]
        public string NOMBRE { get; set; }

        [StringLength(20)]
        public string APELLIDO { get; set; }

        [StringLength(20)]
        public string DIRECCION { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_NAC { get; set; }

        [StringLength(13)]
        public string NUM_IDENTIDAD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_CLIENTES> TBL_CLIENTES { get; set; }
    }
}
