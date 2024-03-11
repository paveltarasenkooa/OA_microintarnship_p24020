using System;
using System.Collections.Generic;

namespace OA_Example_Project.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string HierarchyLevel1 { get; set; } = null!;

    public string HierarchyLevel2 { get; set; } = null!;

    public string? ProviderId { get; set; }

    public bool IsActive { get; set; }

    public DateTime ClientStartDate { get; set; }

    public DateTime? ClientEndDate { get; set; }

    public string? Baseline { get; set; }

    public int? TypeId { get; set; }

    public string? AscellaHierarchyIdentifier { get; set; }

    public int? KppDownstreamClientIdValue { get; set; }

    public DateOnly? EarliestFillDate { get; set; }

    public DateOnly? LatestFillDate { get; set; }

    public DateOnly FirstRebatesSubmittedDate { get; set; }

    public DateOnly? LastRebatesSubmittedDate { get; set; }
}
