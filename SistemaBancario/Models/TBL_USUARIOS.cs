
namespace SistemaBancario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class TBL_USUARIOS
    {
        [Key]
        public int COD_USUARIO { get; set; }

        [StringLength(20)]
        public string USUARIO { get; set; }

        [StringLength(100)]
        public string PASS { get; set; }

        [StringLength(20)]
        public string NOMBRES { get; set; }

        [StringLength(20)]
        public string APELLIDOS { get; set; }

        [StringLength(13)]
        public string IDENTIDAD { get; set; }

        public int COD_ROL { get; set; }

        [StringLength(20)]
        public string ESTADO_USER { get; set; }

        public virtual TBL_ROLES TBL_ROLES { get; set; }
    }
}