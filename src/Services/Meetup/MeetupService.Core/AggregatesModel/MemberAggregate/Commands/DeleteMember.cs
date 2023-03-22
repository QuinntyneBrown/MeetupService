// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.MemberAggregate.Commands;

public class DeleteMemberRequestValidator: AbstractValidator<DeleteMemberRequest>
{
    public DeleteMemberRequestValidator(){

        RuleFor(x => x.MemberId).NotEqual(default(Guid));

    }

}


public class DeleteMemberRequest: IRequest<DeleteMemberResponse>
{
    public Guid MemberId { get; set; }
}


public class DeleteMemberResponse
{
    public required MemberDto Member { get; set; }
}


public class DeleteMemberRequestHandler: IRequestHandler<DeleteMemberRequest,DeleteMemberResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<DeleteMemberRequestHandler> _logger;

    public DeleteMemberRequestHandler(ILogger<DeleteMemberRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<DeleteMemberResponse> Handle(DeleteMemberRequest request,CancellationToken cancellationToken)
    {
        var member = await _context.Members.FindAsync(request.MemberId);

        _context.Members.Remove(member);

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            Member = member.ToDto()
        };
    }

}



