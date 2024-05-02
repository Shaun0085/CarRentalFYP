﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarRentalFYP.Models;

public partial class CarRentalDatabaseContext : DbContext
{
    public CarRentalDatabaseContext(DbContextOptions<CarRentalDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarStatus> CarStatuses { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleCategory> VehicleCategories { get; set; }

    public virtual DbSet<VehicleFuel> VehicleFuels { get; set; }

    public virtual DbSet<VehicleTransmission> VehicleTransmissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE488F056C740");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD95A86460");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingStatusId).HasColumnName("BookingStatusID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.DropOffDate).HasColumnType("date");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Icnumber).HasColumnName("ICNumber");
            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.OrderId)
                .IsUnicode(false)
                .HasColumnName("OrderID");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.PaymentType).IsUnicode(false);
            entity.Property(e => e.PickUpDate).HasColumnType("date");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.BookingStatus).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingStatusId)
                .HasConstraintName("FK__Bookings__Bookin__0E6E26BF");

            entity.HasOne(d => d.Car).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK__Bookings__CarID__10566F31");

            entity.HasOne(d => d.Location).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Bookings__Locati__0F624AF8");

            entity.HasOne(d => d.Payment).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Bookings__Paymen__0D7A0286");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Bookings__Vehicl__0C85DE4D");
        });

        modelBuilder.Entity<BookingStatus>(entity =>
        {
            entity.HasKey(e => e.BookingStatusId).HasName("PK__BookingS__54F9C0BDB423AB07");

            entity.Property(e => e.BookingStatusId).HasColumnName("BookingStatusID");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0340E6D3E44F1");

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.CarStatusId).HasColumnName("CarStatusID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.CarStatus).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarStatusId)
                .HasConstraintName("FK__Cars__CarStatusI__160F4887");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Cars)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Cars__VehicleID__17036CC0");
        });

        modelBuilder.Entity<CarStatus>(entity =>
        {
            entity.HasKey(e => e.CarStatusId).HasName("PK__CarStatu__4A328CE6653F7E0D");

            entity.Property(e => e.CarStatusId).HasColumnName("CarStatusID");
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA477BED680B7");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58CA18DF5C");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.ReferenceNumber).IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54B24314CA8E");

            entity.HasIndex(e => e.VehicleCategoryId, "IX_Vehicles_VehicleCategoryID");

            entity.HasIndex(e => e.VehicleTransmissionId, "IX_Vehicles_VehicleTransmissionID");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.VehicleCategoryId).HasColumnName("VehicleCategoryID");
            entity.Property(e => e.VehicleFuelId).HasColumnName("VehicleFuelID");
            entity.Property(e => e.VehicleTransmissionId).HasColumnName("VehicleTransmissionID");

            entity.HasOne(d => d.VehicleCategory).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleCategoryId)
                .HasConstraintName("FK_Vehicles_ToVehicleCategories");

            entity.HasOne(d => d.VehicleFuel).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleFuelId)
                .HasConstraintName("FK_Vehicles_ToVehicleFuels");

            entity.HasOne(d => d.VehicleTransmission).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleTransmissionId)
                .HasConstraintName("FK_Vehicles_ToVehicleTransmissions");
        });

        modelBuilder.Entity<VehicleCategory>(entity =>
        {
            entity.HasKey(e => e.VehicleCategoryId).HasName("PK__VehicleC__55CCA547F059D6DB");

            entity.Property(e => e.VehicleCategoryId).HasColumnName("VehicleCategoryID");
        });

        modelBuilder.Entity<VehicleFuel>(entity =>
        {
            entity.HasKey(e => e.VehicleFuelId).HasName("PK__VehicleF__72A121D014FD36B4");

            entity.Property(e => e.VehicleFuelId).HasColumnName("VehicleFuelID");
        });

        modelBuilder.Entity<VehicleTransmission>(entity =>
        {
            entity.HasKey(e => e.VehicleTransmissionId).HasName("PK__VehicleT__4614AA11057F38E1");

            entity.Property(e => e.VehicleTransmissionId).HasColumnName("VehicleTransmissionID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}