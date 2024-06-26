﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VTET.Data.Models;

public partial class Net1704_221_8_VTETPlatformContext : DbContext
{
    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));


    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Watch> Watches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC075E2FB946");

            entity.ToTable("Customer");

            entity.Property(e => e.Birth).HasColumnType("date");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(20);
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluati__3214EC07CF11ECBE");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.EstimatePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EvaluationType).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.WatchId).HasColumnName("Watch_ID");

            entity.HasOne(d => d.Watch).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.WatchId)
                .HasConstraintName("FK__Evaluatio__Watch__5441852A");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC077AC77A5D");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.PaymentMethod).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__571DF1D5");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC079619EFB3");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("date");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ShipmentDate).HasColumnType("date");
            entity.Property(e => e.ShippingCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WatchId).HasColumnName("Watch_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__5535A963");

            entity.HasOne(d => d.Watch).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.WatchId)
                .HasConstraintName("FK__OrderDeta__Watch__5629CD9C");
        });

        modelBuilder.Entity<Watch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watch__3214EC07CA0A01AA");

            entity.ToTable("Watch");

            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.Condition).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.EstimatePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.Location).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Type).HasMaxLength(30);

            entity.HasOne(d => d.Customer).WithMany(p => p.Watches)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Watch__Customer___5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}