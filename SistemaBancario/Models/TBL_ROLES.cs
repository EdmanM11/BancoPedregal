
namespace SistemaBancario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class TBL_ROLES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_ROLES()
        {
            TBL_USUARIOS = new HashSet<TBL_USUARIOS>();
        }

        [Key]
        public int COD_ROL { get; set; }

        [StringLength(30)]
        public string ROL { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_USUARIOS> TBL_USUARIOS { get; set; }

    }
}