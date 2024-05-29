﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VTET.Data.Models;

public partial class Net1704_221_8_VTETPlatformContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("data source=TRANQUOCBAO;initial catalog=Net1704_221_8_VTETPlatform;user id=sa;password=12345;Integrated Security=True;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Watch> Watches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC077FF8F3E4");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Birth).HasColumnType("date");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(20);
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluati__3214EC07CFBB4B68");

            entity.Property(e => e.Id).HasMaxLength(20);
            entity.Property(e => e.CreateBy).HasMaxLength(100);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.EstimatePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WatchId)
                .HasMaxLength(20)
                .HasColumnName("Watch_ID");

            entity.HasOne(d => d.Watch).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.WatchId)
                .HasConstraintName("FK__Evaluatio__Watch__5DCAEF64");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__F1E4639B743EA854");

            entity.Property(e => e.OrderId)
                .HasMaxLength(20)
                .HasColumnName("Order_ID");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__60A75C0F");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__53D880E05CFFC4F2");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(20)
                .HasColumnName("OrderDetail_ID");
            entity.Property(e => e.OrderId)
                .HasMaxLength(20)
                .HasColumnName("Order_ID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WatchId)
                .HasMaxLength(20)
                .HasColumnName("Watch_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetail_Orders");

            entity.HasOne(d => d.Watch).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.WatchId)
                .HasConstraintName("FK__OrderDeta__Watch__5EBF139D");
        });

        modelBuilder.Entity<Watch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watch__3214EC07BF70B406");

            entity.ToTable("Watch");

            entity.Property(e => e.Id).HasMaxLength(20);
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.Condition).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.EstimatePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Watches)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Watch__User_ID__619B8048");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}