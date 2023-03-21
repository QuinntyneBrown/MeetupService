// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.GroupAggregate.Commands;

public class DeleteGroupRequestValidator: AbstractValidator<DeleteGroupRequest>
{
    public DeleteGroupRequestValidator(){

        RuleFor(x => x.GroupId).NotEqual(default(Guid));

    }

}


public class DeleteGroupRequest: IRequest<DeleteGroupResponse>
{
    public Guid GroupId { get; set; }
}


public class DeleteGroupResponse
{
    public required GroupDto Group { get; set; }
}


public class DeleteGroupRequestHandler: IRequestHandler<DeleteGroupRequest,DeleteGroupResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<DeleteGroupRequestHandler> _logger;

    public DeleteGroupRequestHandler(ILogger<DeleteGroupRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<DeleteGroupResponse> Handle(DeleteGroupRequest request,CancellationToken cancellationToken)
    {
        var group = await _context.Groups.FindAsync(request.GroupId);

        _context.Groups.Remove(group);

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            Group = group.ToDto()
        };
    }

}



