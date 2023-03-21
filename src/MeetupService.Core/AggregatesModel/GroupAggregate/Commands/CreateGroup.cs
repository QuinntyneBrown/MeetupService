// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.GroupAggregate.Commands;

public class CreateGroupRequestValidator: AbstractValidator<CreateGroupRequest>
{
    public CreateGroupRequestValidator(){

        RuleFor(x => x.Name).NotNull();

    }

}


public class CreateGroupRequest: IRequest<CreateGroupResponse>
{
    public string Name { get; set; }
}


public class CreateGroupResponse
{
    public required GroupDto Group { get; set; }
}


public class CreateGroupRequestHandler: IRequestHandler<CreateGroupRequest,CreateGroupResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<CreateGroupRequestHandler> _logger;

    public CreateGroupRequestHandler(ILogger<CreateGroupRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateGroupResponse> Handle(CreateGroupRequest request,CancellationToken cancellationToken)
    {
        var group = new Group();

        _context.Groups.Add(group);

        group.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            Group = group.ToDto()
        };

    }

}



