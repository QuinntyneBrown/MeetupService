// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MeetupService.Core.AggregatesModel.GroupAggregate.Commands;
using MeetupService.Core.AggregatesModel.GroupAggregate.Queries;
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
public class GroupController
{
    private readonly IMediator _mediator;

    private readonly ILogger<GroupController> _logger;

    public GroupController(IMediator mediator,ILogger<GroupController> logger){
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SwaggerOperation(
        Summary = "Update Group",
        Description = @"Update Group"
    )]
    [HttpPut(Name = "updateGroup")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(UpdateGroupResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UpdateGroupResponse>> Update([FromBody]UpdateGroupRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Create Group",
        Description = @"Create Group"
    )]
    [HttpPost(Name = "createGroup")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CreateGroupResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CreateGroupResponse>> Create([FromBody]CreateGroupRequest  request,CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get Groups",
        Description = @"Get Groups"
    )]
    [HttpGet(Name = "getGroups")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetGroupsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetGroupsResponse>> Get(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetGroupsRequest(), cancellationToken);
    }

    [SwaggerOperation(
        Summary = "Get Group by id",
        Description = @"Get Group by id"
    )]
    [HttpGet("{groupId:guid}", Name = "getGroupById")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetGroupByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetGroupByIdResponse>> GetById([FromRoute]Guid groupId,CancellationToken cancellationToken)
    {
        var request = new GetGroupByIdRequest(){GroupId = groupId};

        var response = await _mediator.Send(request, cancellationToken);

        if (response.Group == null)
        {
            return new NotFoundObjectResult(request.GroupId);
        }

        return response;
    }

    [SwaggerOperation(
        Summary = "Delete Group",
        Description = @"Delete Group"
    )]
    [HttpDelete("{groupId:guid}", Name = "deleteGroup")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DeleteGroupResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DeleteGroupResponse>> Delete([FromRoute]Guid groupId,CancellationToken cancellationToken)
    {
        var request = new DeleteGroupRequest() {GroupId = groupId };

        return await _mediator.Send(request, cancellationToken);
    }

}


