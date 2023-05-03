using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hmsAdmin.Models;

namespace hmsAdmin.Data;

public partial class HmsContext : DbContext
{
    public HmsContext()
    {
    }

    public HmsContext(DbContextOptions<HmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Diagnose> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Lab> Labs { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HM99MKM;Database=HMS;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__appointm__3213E83F33B6A52E");

            entity.HasOne(d => d.FkDeptNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_appointments_departments");

            entity.HasOne(d => d.FkPatNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_appointments_patients");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83FBF388771");
        });

        modelBuilder.Entity<Diagnose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__diagnose__3213E83F7B00B9D0");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.FkAppNavigation).WithMany(p => p.Diagnoses).HasConstraintName("FK_diagnose_appointments");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__doctors__3213E83F918D10AD");

            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.FkDeptNavigation).WithMany(p => p.Doctors).HasConstraintName("FK_doctors_departments");
        });

        modelBuilder.Entity<Lab>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lab__3213E83F64C88213");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Result).IsFixedLength();

            entity.HasOne(d => d.FkAppNavigation).WithMany(p => p.Labs).HasConstraintName("FK_lab_appointments");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasOne(d => d.FkDiaNavigation).WithMany(p => p.Medicines).HasConstraintName("FK_medicine_diagnose");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nurses__3213E83F13C92838");

            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.FkDeptNavigation).WithMany(p => p.Nurses).HasConstraintName("FK_nurses_departments");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patients__3213E83F6DEFA952");

            entity.Property(e => e.Phone).IsFixedLength();
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pharmacy__3213E83FD9C3245B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
