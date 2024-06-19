using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eSya.ConfigPharmacy.DL.Entities
{
    public partial class eSyaEnterprise : DbContext
    {
        public static string _connString = "";

        public eSyaEnterprise()
        {
        }

        public eSyaEnterprise(DbContextOptions<eSyaEnterprise> options)
            : base(options)
        {
        }

        public virtual DbSet<GtEavncd> GtEavncds { get; set; } = null!;
        public virtual DbSet<GtEcapcd> GtEcapcds { get; set; } = null!;
        public virtual DbSet<GtEphdco> GtEphdcos { get; set; } = null!;
        public virtual DbSet<GtEphdcp> GtEphdcps { get; set; } = null!;
        public virtual DbSet<GtEphdfm> GtEphdfms { get; set; } = null!;
        public virtual DbSet<GtEphdfp> GtEphdfps { get; set; } = null!;
        public virtual DbSet<GtEphdfr> GtEphdfrs { get; set; } = null!;
        public virtual DbSet<GtEphdrc> GtEphdrcs { get; set; } = null!;
        public virtual DbSet<GtEphdtc> GtEphdtcs { get; set; } = null!;
        public virtual DbSet<GtEphmnf> GtEphmnfs { get; set; } = null!;
        public virtual DbSet<GtEphmvl> GtEphmvls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GtEavncd>(entity =>
            {
                entity.HasKey(e => e.VendorId);

                entity.ToTable("GT_EAVNCD");

                entity.Property(e => e.VendorId)
                    .ValueGeneratedNever()
                    .HasColumnName("VendorID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.CreditPeriod).HasColumnType("numeric(3, 0)");

                entity.Property(e => e.CreditType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.VendorName).HasMaxLength(75);
            });

            modelBuilder.Entity<GtEcapcd>(entity =>
            {
                entity.HasKey(e => e.ApplicationCode)
                    .HasName("PK_GT_ECAPCD_1");

                entity.ToTable("GT_ECAPCD");

                entity.Property(e => e.ApplicationCode).ValueGeneratedNever();

                entity.Property(e => e.CodeDesc).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ShortCode).HasMaxLength(15);
            });

            modelBuilder.Entity<GtEphdco>(entity =>
            {
                entity.HasKey(e => e.DrugClass);

                entity.ToTable("GT_EPHDCO");

                entity.Property(e => e.DrugClass).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DrugClassDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEphdcp>(entity =>
            {
                entity.HasKey(e => new { e.CompositionId, e.ParameterId });

                entity.ToTable("GT_EPHDCP");

                entity.Property(e => e.CompositionId).HasColumnName("CompositionID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEphdfm>(entity =>
            {
                entity.HasKey(e => new { e.CompositionId, e.FormulationId, e.ManufacturerId });

                entity.ToTable("GT_EPHDFM");

                entity.Property(e => e.CompositionId).HasColumnName("CompositionID");

                entity.Property(e => e.FormulationId).HasColumnName("FormulationID");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEphdfp>(entity =>
            {
                entity.HasKey(e => new { e.CompositionId, e.FormulationId, e.ParameterId });

                entity.ToTable("GT_EPHDFP");

                entity.Property(e => e.CompositionId).HasColumnName("CompositionID");

                entity.Property(e => e.FormulationId).HasColumnName("FormulationID");

                entity.Property(e => e.ParameterId).HasColumnName("ParameterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.ParmDesc)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ParmPerc).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ParmValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<GtEphdfr>(entity =>
            {
                entity.HasKey(e => new { e.CompositionId, e.FormulationId });

                entity.ToTable("GT_EPHDFR");

                entity.Property(e => e.CompositionId).HasColumnName("CompositionID");

                entity.Property(e => e.FormulationId).HasColumnName("FormulationID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.FormulationDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.Volume)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GtEphdrc>(entity =>
            {
                entity.HasKey(e => e.CompositionId);

                entity.ToTable("GT_EPHDRC");

                entity.Property(e => e.CompositionId)
                    .ValueGeneratedNever()
                    .HasColumnName("CompositionID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DrugCompDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEphdtc>(entity =>
            {
                entity.HasKey(e => e.DrugTherapeutic);

                entity.ToTable("GT_EPHDTC");

                entity.Property(e => e.DrugTherapeutic).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.DrugTherapeuticDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEphmnf>(entity =>
            {
                entity.HasKey(e => e.ManufacturerId);

                entity.ToTable("GT_EPHMNF");

                entity.Property(e => e.ManufacturerId)
                    .ValueGeneratedNever()
                    .HasColumnName("ManufacturerID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ManfShortName).HasMaxLength(10);

                entity.Property(e => e.ManufacturerName).HasMaxLength(75);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);
            });

            modelBuilder.Entity<GtEphmvl>(entity =>
            {
                entity.HasKey(e => e.ManufacturerId);

                entity.ToTable("GT_EPHMVL");

                entity.Property(e => e.ManufacturerId)
                    .ValueGeneratedNever()
                    .HasColumnName("ManufacturerID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedTerminal).HasMaxLength(50);

                entity.Property(e => e.FormId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FormID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedTerminal).HasMaxLength(50);

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
