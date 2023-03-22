// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.MemberAggregate.Queries;

public class GetMemberByIdRequest: IRequest<GetMemberByIdResponse>
{
    public Guid MemberId { get; set; }
}


public class GetMemberByIdResponse
{
    public required MemberDto Member { get; set; }
}


public class GetMemberByIdRequestHandler: IRequestHandler<GetMemberByIdRequest,GetMemberByIdResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<GetMemberByIdRequestHandler> _logger;

    public GetMemberByIdRequestHandler(ILogger<GetMemberByIdRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetMemberByIdResponse> Handle(GetMemberByIdRequest request,CancellationToken cancellationToken)
    {
        return new () {
            Member = (await _context.Members.AsNoTracking().SingleOrDefaultAsync(x => x.MemberId == request.MemberId)).ToDto()
        };

    }

}



