﻿using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }
}
