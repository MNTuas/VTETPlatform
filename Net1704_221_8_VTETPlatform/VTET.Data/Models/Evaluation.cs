﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace VTET.Data.Models;
using System.ComponentModel.DataAnnotations;

public partial class Evaluation
{
    public int Id { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
    public string Comment { get; set; }

    
    [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
    public string Status { get; set; }

    public int? Rarity { get; set; }

    [StringLength(1000, ErrorMessage = "Attachments cannot exceed 1000 characters.")]
    public string Attachments { get; set; }

    
    [StringLength(100, ErrorMessage = "EvaluationType cannot exceed 100 characters.")]
    public string EvaluationType { get; set; }

    [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
    public int? Rate { get; set; }

    [Range(0, 99999999.99, ErrorMessage = "EstimatePrice must be a positive number.")]
    public decimal? EstimatePrice { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? WatchId { get; set; }

    public virtual Watch Watch { get; set; }
}