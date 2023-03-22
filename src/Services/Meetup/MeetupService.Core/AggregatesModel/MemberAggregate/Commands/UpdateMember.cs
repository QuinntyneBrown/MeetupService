// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MeetupService.Core.AggregatesModel.MemberAggregate.Commands;

public class UpdateMemberRequestValidator: AbstractValidator<UpdateMemberRequest>
{
    public UpdateMemberRequestValidator(){

        RuleFor(x => x.MemberId).NotEqual(default(Guid));
        RuleFor(x => x.Name).NotNull();

    }

}


public class UpdateMemberRequest: IRequest<UpdateMemberResponse>
{
    public Guid MemberId { get; set; }
    public string Name { get; set; }
}


public class UpdateMemberResponse
{
    public required MemberDto Member { get; set; }
}


public class UpdateMemberRequestHandler: IRequestHandler<UpdateMemberRequest,UpdateMemberResponse>
{
    private readonly IMeetupServiceDbContext _context;

    private readonly ILogger<UpdateMemberRequestHandler> _logger;

    public UpdateMemberRequestHandler(ILogger<UpdateMemberRequestHandler> logger,IMeetupServiceDbContext context){
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UpdateMemberResponse> Handle(UpdateMemberRequest request,CancellationToken cancellationToken)
    {
        var member = await _context.Members.SingleAsync(x => x.MemberId == request.MemberId);

        member.MemberId = request.MemberId;
        member.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new ()
        {
            Member = member.ToDto()
        };

    }

}



