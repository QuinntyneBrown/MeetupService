// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MeetupService.Core.AggregatesModel.MemberAggregate.Commands;
using MeetupService.Core.AggregatesModel.MemberAggregate.Queries;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;

namespace MeetupService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class MemberController
{
    private readonly IMediator _mediator;

    private readonly ILogger<MemberController> _logger;

    public MemberController(IMediator mediator,ILogger<MemberController> logger){
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SwaggerOperation(
        Summary = "Update Member",
        Description = @"Update Member"
    )]
    [HttpPut(Name = "updateMember")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(UpdateMemberResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateMemberResponse>> Update([FromBody]UpdateMemberRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Create Member",
        Description = @"Create Member"
    )]
    [HttpPost(Name = "createMember")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CreateMemberResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CreateMemberResponse>> Create([FromBody]CreateMemberRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get Members",
        Description = @"Get Members"
    )]
    [HttpGet(Name = "getMembers")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetMembersResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetMembersResponse>> Get(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetMembersRequest(), cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get Member by id",
        Description = @"Get Member by id"
    )]
    [HttpGet("{memberId:guid}", Name = "getMemberById")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetMemberByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetMemberByIdResponse>> GetById([FromRoute]Guid memberId,CancellationToken cancellationToken)
    {
        var request = new GetMemberByIdRequest(){MemberId = memberId};

        var response = await _mediator.Send(request, cancellationToken);

        if (response.Member == null)
        {
            return new NotFoundObjectResult(request.MemberId);
        }

        return response;
    }

    [SwaggerOperation(
        Summary = "Delete Member",
        Description = @"Delete Member"
    )]
    [HttpDelete("{memberId:guid}", Name = "deleteMember")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DeleteMemberResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DeleteMemberResponse>> Delete([FromRoute]Guid memberId,CancellationToken cancellationToken)
    {
        var request = new DeleteMemberRequest() {MemberId = memberId };

        return await _mediator.Send(request, cancellationToken);
    }

}


