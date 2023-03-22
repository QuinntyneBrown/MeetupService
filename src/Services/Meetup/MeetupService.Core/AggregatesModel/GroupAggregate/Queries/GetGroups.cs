// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.GroupAggregate.Queries;

public class GetGroupsRequest: IRequest<GetGroupsResponse> { }

public class GetGroupsResponse
{
    public required List<GroupDto> Groups { get; set; }
}


public class GetGroupsRequestHandler: IRequestHandler<GetGroupsRequest,GetGroupsResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<GetGroupsRequestHandler> _logger;

    public GetGroupsRequestHandler(ILogger<GetGroupsRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetGroupsResponse> Handle(GetGroupsRequest request,CancellationToken cancellationToken)
    {
        return new () {
            Groups = await _context.Groups.AsNoTracking().ToDtosAsync(cancellationToken)
        };

    }

}



