using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BugProjektV1.Models
{
    public partial class BugProjektContext : DbContext
    {
        public BugProjektContext()
        {
        }

        public BugProjektContext(DbContextOptions<BugProjektContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bug> Bugs { get; set; }
        public virtual DbSet<Mitarbeiter> Mitarbeiters { get; set; }
        public virtual DbSet<Projekt> Projekts { get; set; }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BugProjekt;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bug>(entity =>
            {
                entity.ToTable("Bug");

                entity.Property(e => e.BugId).HasColumnName("BugID");

                entity.Property(e => e.Beschreibung)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EntwicklerId).HasColumnName("EntwicklerID");

                entity.Property(e => e.ProjektId).HasColumnName("ProjektID");

                entity.Property(e => e.TesterId).HasColumnName("TesterID");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Entwickler)
                    .WithMany(p => p.BugEntwicklers)
                    .HasForeignKey(d => d.EntwicklerId)
                    .HasConstraintName("FK_EntwicklerID");

                entity.HasOne(d => d.Projekt)
                    .WithMany(p => p.Bugs)
                    .HasForeignKey(d => d.ProjektId)
                    .HasConstraintName("FK_ProjektID");

                entity.HasOne(d => d.Tester)
                    .WithMany(p => p.BugTesters)
                    .HasForeignKey(d => d.TesterId)
                    .HasConstraintName("FK_TesterID");
            });

            modelBuilder.Entity<Mitarbeiter>(entity =>
            {
                entity.ToTable("Mitarbeiter");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.Bereich)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nachname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Vorname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Projekt>(entity =>
            {
                entity.ToTable("Projekt");

                entity.Property(e => e.ProjektId).HasColumnName("ProjektID");

                entity.Property(e => e.ProjektName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
