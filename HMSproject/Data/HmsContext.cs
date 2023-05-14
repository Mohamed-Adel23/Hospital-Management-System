using System;
using System.Collections.Generic;
using HMSproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Models;

public partial class HmsContext : IdentityDbContext<Patient>
{
    public HmsContext()
    {
    }

    public HmsContext(DbContextOptions<HmsContext> options) : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }
    
    public virtual DbSet<Patient> AspNetUsers { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Diagnose> Diagnose { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Lab> Labs { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<cash_flow> Cash_Flows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
}