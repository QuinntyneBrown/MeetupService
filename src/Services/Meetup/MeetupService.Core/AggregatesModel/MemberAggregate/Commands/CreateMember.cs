// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.MemberAggregate.Commands;

public class CreateMemberRequestValidator: AbstractValidator<CreateMemberRequest>
{
    public CreateMemberRequestValidator(){

        RuleFor(x => x.Name).NotNull();

    }

}


public class CreateMemberRequest: IRequest<CreateMemberResponse>
{
    public string Name { get; set; }
}


public class CreateMemberResponse
{
    public required MemberDto Member { get; set; }
}


public class CreateMemberRequestHandler: IRequestHandler<CreateMemberRequest,CreateMemberResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<CreateMemberRequestHandler> _logger;

    public CreateMemberRequestHandler(ILogger<CreateMemberRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateMemberResponse> Handle(CreateMemberRequest request,CancellationToken cancellationToken)
    {
        var member = new Member();

        _context.Members.Add(member);

        member.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            Member = member.ToDto()
        };

    }

}



