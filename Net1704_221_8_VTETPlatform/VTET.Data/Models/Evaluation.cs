﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace VTET.Data.Models;

public partial class Evaluation
{
    public int Id { get; set; }

    public string Comment { get; set; }

    public string Status { get; set; }

    public int? Rarity { get; set; }

    public string Attachments { get; set; }

    public string EvaluationType { get; set; }

    public int? Rate { get; set; }

    public decimal? EstimatePrice { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? WatchId { get; set; }

    public virtual Watch Watch { get; set; }
}