﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Megift.API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Sku { get; set; }

    public decimal Price { get; set; }

    public string Stock { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}