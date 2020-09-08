using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IspitMVCProjekat.Models
{
    public partial class IspitDBContext : DbContext
    {
        public IspitDBContext()
        {
        }

        public IspitDBContext(DbContextOptions<IspitDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ispit> Ispit { get; set; }
        public virtual DbSet<Predmet> Predmet { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server= DESKTOP-5AOTR9Q\\SQL2014EXPRESS; Database = IspitDB; Trusted_Connection=True;Integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ispit>(entity =>
            {
                entity.Property(e => e.IspitId).HasColumnName("IspitID");

                entity.Property(e => e.BrojIndexa)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PredmetId).HasColumnName("PredmetID");

                entity.HasOne(d => d.BrojIndexaNavigation)
                    .WithMany(p => p.Ispit)
                    .HasForeignKey(d => d.BrojIndexa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ispit_Student");

                entity.HasOne(d => d.Predmet)
                    .WithMany(p => p.Ispit)
                    .HasForeignKey(d => d.PredmetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ispit_Predmet");
            });

            modelBuilder.Entity<Predmet>(entity =>
            {
                entity.Property(e => e.PredmetId).HasColumnName("PredmetID");

                entity.Property(e => e.ImePredmeta)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.BrojIndexa);

                entity.Property(e => e.BrojIndexa).HasMaxLength(10);

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Grad)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
