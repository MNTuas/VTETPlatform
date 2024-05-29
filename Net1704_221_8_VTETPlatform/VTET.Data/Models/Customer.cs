﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace VTET.Data.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public DateTime? Birth { get; set; }

    public string Gender { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Watch> Watches { get; set; } = new List<Watch>();
}