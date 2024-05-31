﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace VTET.Data.Models;

public partial class Watch
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Type { get; set; }

    public string Picture { get; set; }

    public decimal? Price { get; set; }

    public decimal? EstimatePrice { get; set; }

    public string Condition { get; set; }

    public string Location { get; set; }

    public string Brand { get; set; }

    public string Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}