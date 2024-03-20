using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OA_Example_Project.Models;

public partial class OaProjectContext : DbContext
{
    public OaProjectContext()
    {
    }

    public OaProjectContext(DbContextOptions<OaProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<HospitalType> HospitalTypes { get; set; }

    public virtual DbSet<MedicalTransaction> MedicalTransactions { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientType> PatientTypes { get; set; }

    public virtual DbSet<VHospital> VHospitals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:ptoapersonal.database.windows.net,1433;Initial Catalog=OA_Project;Persist Security Info=False;User ID=ptarasenko;Password=28081992Asd*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.ToTable("Hospital");

            entity.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.OpeningDate).HasDefaultValue(new DateOnly(1999, 1, 1));

            entity.HasOne(d => d.HopitalType).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.HopitalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hospital_HopitalTypeId");
        });

        modelBuilder.Entity<HospitalType>(entity =>
        {
            entity.ToTable("HospitalType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedicalTransaction>(entity =>
        {
            entity.ToTable("MedicalTransaction");

            entity.Property(e => e.Ndc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NDC");
            entity.Property(e => e.Quantity).HasColumnType("numeric(18, 5)");
            entity.Property(e => e.RxNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalTransactions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalTr__Patie__3C34F16F");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Hospital).WithMany(p => p.Patients)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patient__Hospita__2DE6D218");
        });

        modelBuilder.Entity<PatientType>(entity =>
        {
            entity.ToTable("PatientType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VHospital>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_Hospital");

            entity.Property(e => e.HospitalName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HospitalType)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Maxdob).HasColumnName("MAXDOB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
