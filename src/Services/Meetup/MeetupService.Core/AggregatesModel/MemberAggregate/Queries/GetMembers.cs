// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.MemberAggregate.Queries;

public class GetMembersRequest: IRequest<GetMembersResponse> { }

public class GetMembersResponse
{
    public required List<MemberDto> Members { get; set; }
}


public class GetMembersRequestHandler: IRequestHandler<GetMembersRequest,GetMembersResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<GetMembersRequestHandler> _logger;

    public GetMembersRequestHandler(ILogger<GetMembersRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetMembersResponse> Handle(GetMembersRequest request,CancellationToken cancellationToken)
    {
        return new () {
            Members = await _context.Members.AsNoTracking().ToDtosAsync(cancellationToken)
        };

    }

}



