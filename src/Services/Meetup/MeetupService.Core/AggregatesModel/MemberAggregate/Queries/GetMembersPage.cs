// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.MemberAggregate.Queries;

public class GetMembersPageRequest: IRequest<GetMembersPageResponse>
{
    public int PageSize { get; set; }
    public int Index { get; set; }
    public int Length { get; set; }
}


public class GetMembersPageResponse
{
    public required int Length { get; set; }
    public required List<MemberDto> Entities  { get; set; }
}


public class CreateMemberRequestHandler: IRequestHandler<GetMembersPageRequest,GetMembersPageResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<CreateMemberRequestHandler> _logger;

    public CreateMemberRequestHandler(ILogger<CreateMemberRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetMembersPageResponse> Handle(GetMembersPageRequest request,CancellationToken cancellationToken)
    {
        var query = from member in _context.Members
            select member;

        var length = await _context.Members.AsNoTracking().CountAsync();

        var members = await query.Page(request.Index, request.PageSize).AsNoTracking()
            .Select(x => x.ToDto()).ToListAsync();

        return new ()
        {
            Length = length,
            Entities = members
        };

    }

}



