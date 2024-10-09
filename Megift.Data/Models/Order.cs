﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Megift.Data.Models;

public partial class Order
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly OrderDate { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; }

    [JsonIgnore]
    public virtual Customer Customer { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; }
}