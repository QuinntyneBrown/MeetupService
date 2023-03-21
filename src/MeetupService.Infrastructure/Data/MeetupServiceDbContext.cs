// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MeetupService.Core;
using Microsoft.EntityFrameworkCore;
using MeetupService.Core.AggregatesModel.GroupAggregate;

namespace MeetupService.Infrastructure.Data;

public class MeetupServiceDbContext: DbContext,IMeetupServiceDbContext
{
    public MeetupServiceDbContext(DbContextOptions<MeetupServiceDbContext> options)    : base(options)
    {
    }

    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Meetup");

        base.OnModelCreating(modelBuilder);
    }
}


