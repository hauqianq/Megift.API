﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Megift.Data.Models;

public partial class Review
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Review1 { get; set; }

    [JsonIgnore]

    public virtual Customer Customer { get; set; }
}