﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CarRentalFYP.Models;

public partial class BookingStatus
{
    public int BookingStatusId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}