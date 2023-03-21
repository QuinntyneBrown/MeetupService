// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.GroupAggregate.Commands;

public class UpdateGroupRequestValidator: AbstractValidator<UpdateGroupRequest>
{
    public UpdateGroupRequestValidator(){

        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.Name).NotNull();

    }

}


public class UpdateGroupRequest: IRequest<UpdateGroupResponse>
{
    public Guid GroupId { get; set; }
    public string Name { get; set; }
}


public class UpdateGroupResponse
{
    public required GroupDto Group { get; set; }
}


public class UpdateGroupRequestHandler: IRequestHandler<UpdateGroupRequest,UpdateGroupResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<UpdateGroupRequestHandler> _logger;

    public UpdateGroupRequestHandler(ILogger<UpdateGroupRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UpdateGroupResponse> Handle(UpdateGroupRequest request,CancellationToken cancellationToken)
    {
        var group = await _context.Groups.SingleAsync(x => x.GroupId == request.GroupId);

        group.GroupId = request.GroupId;
        group.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            Group = group.ToDto()
        };

    }

}



