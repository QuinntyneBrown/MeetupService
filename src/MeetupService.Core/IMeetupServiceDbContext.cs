// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MeetupService.Core.AggregatesModel.GroupAggregate;

namespace MeetupService.Core;

public interface IMeetupServiceDbContext
{
    DbSet<Group> Groups { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}


