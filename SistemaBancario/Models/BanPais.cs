using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SistemaBancario.Models
{
    public partial class BanPais : DbContext
    {
        public BanPais()
            : base("name=BanPais")
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TBL_CLIENTES> TBL_CLIENTES { get; set; }
        public virtual DbSet<TBL_CUENTAS_BANCARIAS> TBL_CUENTAS_BANCARIAS { get; set; }
        public virtual DbSet<TBL_HISTORIAL_TRANSACCIONES> TBL_HISTORIAL_TRANSACCIONES { get; set; }
        public virtual DbSet<TBL_PERSONAS> TBL_PERSONAS { get; set; }
        public virtual DbSet<TBL_TIPO_CUENTA> TBL_TIPO_CUENTA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBL_CUENTAS_BANCARIAS>()
                .Property(e => e.ESTADO)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_CUENTAS_BANCARIAS>()
                .Property(e => e.SALDO)
                .HasPrecision(10, 2);

            modelBuilder.Entity<TBL_HISTORIAL_TRANSACCIONES>()
                .Property(e => e.MONTO)
                .HasPrecision(10, 2);

            modelBuilder.Entity<TBL_PERSONAS>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_PERSONAS>()
                .Property(e => e.APELLIDO)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_PERSONAS>()
                .Property(e => e.DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_PERSONAS>()
                .Property(e => e.NUM_IDENTIDAD)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_TIPO_CUENTA>()
                .Property(e => e.TIPO_CUENTA)
                .IsUnicode(false);

            modelBuilder.Entity<TBL_TIPO_CUENTA>()
                .Property(e => e.ESTADO_CUENTA)
                .IsUnicode(false);
        }
    }
}
