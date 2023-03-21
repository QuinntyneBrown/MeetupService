// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.GroupAggregate;

public static class GroupExtensions
{
    public static GroupDto ToDto(this Group group)
    {
        return new GroupDto
        {
            GroupId = group.GroupId,
            Name = group.Name,
        };

    }

    public async static Task<List<GroupDto>> ToDtosAsync(this IQueryable<Group> groups,CancellationToken cancellationToken)
    {
        return await groups.Select(x => x.ToDto()).ToListAsync(cancellationToken);
    }

}


