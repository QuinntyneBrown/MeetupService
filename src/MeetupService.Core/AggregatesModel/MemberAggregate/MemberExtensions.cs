// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.MemberAggregate;

public static class MemberExtensions
{
    public static MemberDto ToDto(this Member member)
    {
        return new MemberDto
        {
            MemberId = member.MemberId,
            Name = member.Name,
        };

    }

    public async static Task<List<MemberDto>> ToDtosAsync(this IQueryable<Member> members,CancellationToken cancellationToken)
    {
        return await members.Select(x => x.ToDto()).ToListAsync(cancellationToken);
    }

}


