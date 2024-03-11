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

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=tcp:ptoapersonal.database.windows.net,1433;Initial Catalog=OA_Project;Persist Security Info=False;User ID=ptarasenko;Password=28081992Asd*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.AscellaHierarchyIdentifier).HasMaxLength(25);
            entity.Property(e => e.Baseline).HasMaxLength(50);
            entity.Property(e => e.HierarchyLevel1).HasMaxLength(100);
            entity.Property(e => e.HierarchyLevel2).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.ProviderId).HasMaxLength(10);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_Person");

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
