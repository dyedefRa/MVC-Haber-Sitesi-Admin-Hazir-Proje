namespace HaberinMerkezi.Data.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HaberContext : DbContext
    {
        public HaberContext()
            : base("name=HaberConStr")
        {
        }

        public virtual DbSet<Etiket> Etiket { get; set; }
        public virtual DbSet<Haber> Haber { get; set; }
        public virtual DbSet<HaberEtiket> HaberEtiket { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Resim> Resim { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etiket>()
                .HasMany(e => e.HaberEtiket)
                .WithRequired(e => e.Etiket)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Haber>()
                .HasMany(e => e.Resim)
                .WithRequired(e => e.Haber)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Haber>()
                .HasMany(e => e.HaberEtiket)
                .WithRequired(e => e.Haber)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Haber)
                .WithRequired(e => e.Kategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Haber)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Kullanici)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);
        }
    }
}
