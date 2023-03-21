// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.GroupAggregate.Queries;

public class GetGroupByIdRequest: IRequest<GetGroupByIdResponse>
{
    public Guid GroupId { get; set; }
}


public class GetGroupByIdResponse
{
    public required GroupDto Group { get; set; }
}


public class GetGroupByIdRequestHandler: IRequestHandler<GetGroupByIdRequest,GetGroupByIdResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<GetGroupByIdRequestHandler> _logger;

    public GetGroupByIdRequestHandler(ILogger<GetGroupByIdRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetGroupByIdResponse> Handle(GetGroupByIdRequest request,CancellationToken cancellationToken)
    {
        return new () {
            Group = (await _context.Groups.AsNoTracking().SingleOrDefaultAsync(x => x.GroupId == request.GroupId)).ToDto()
        };

    }

}



