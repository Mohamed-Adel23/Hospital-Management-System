using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hmsAdmin.HMSModels;

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

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Timedept> Timedepts { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HM99MKM;Database=HMS;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__appointm__3213E83F33B6A52E");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.FkDeptNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_appointments_departments");

            entity.HasOne(d => d.FkPatNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_appointments_patients");

            entity.HasOne(d => d.FkPayNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_appointments_payment");

            entity.HasOne(d => d.FkStatNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_appointments_status");

            entity.HasOne(d => d.FkTtNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_appointments_timetable");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83FBF388771");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Diagnose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__diagnose__3213E83F7B00B9D0");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.FkAppNavigation).WithMany(p => p.Diagnoses).HasConstraintName("FK_diagnose_appointments");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__doctors__3213E83F918D10AD");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.FkDeptNavigation).WithMany(p => p.Doctors).HasConstraintName("FK_doctors_departments");

            entity.HasOne(d => d.FkStatusNavigation).WithMany(p => p.Doctors).HasConstraintName("FK_doctors_status");
        });

        modelBuilder.Entity<Lab>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lab__3213E83F64C88213");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Result).IsFixedLength();

            entity.HasOne(d => d.FkAppNavigation).WithMany(p => p.Labs).HasConstraintName("FK_lab_appointments");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasOne(d => d.FkDiaNavigation).WithMany().HasConstraintName("FK_medicine_diagnose");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nurses__3213E83F13C92838");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.FkDeptNavigation).WithMany(p => p.Nurses).HasConstraintName("FK_nurses_departments");

            entity.HasOne(d => d.FkStatusNavigation).WithMany(p => p.Nurses).HasConstraintName("FK_nurses_status");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__patients__3213E83F6DEFA952");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Phone).IsFixedLength();
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payment__3213E83FC7C36133");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pharmacy__3213E83FD9C3245B");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__status__3213E83FAC4D67DD");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Timedept>(entity =>
        {
            entity.HasOne(d => d.FkDeptNavigation).WithMany().HasConstraintName("FK_timedept_departments");

            entity.HasOne(d => d.FkTimeNavigation).WithMany().HasConstraintName("FK_timedept_timetable");
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__timetabl__3213E83FD8E7D805");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
