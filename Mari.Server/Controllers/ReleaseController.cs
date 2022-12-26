using MapsterMapper;
using Mari.Application.Releases.Commands.CreateDraftRelease;
using Mari.Application.Releases.Commands.CreateRelease;
using Mari.Application.Releases.Commands.CreateReleaseFromDraft;
using Mari.Application.Releases.Commands.DeleteRelease;
using Mari.Application.Releases.Commands.SetCompleteStatus;
using Mari.Application.Releases.Commands.SetInWorkStatus;
using Mari.Application.Releases.Commands.UpdateDraftRelease;
using Mari.Application.Releases.Commands.UpdateRelease;
using Mari.Application.Releases.Queries.GetAllPlatforms;
using Mari.Application.Releases.Queries.GetCurrentReleases;
using Mari.Application.Releases.Queries.GetInWorkReleases;
using Mari.Application.Releases.Queries.GetObsoleteReleases;
using Mari.Application.Releases.Queries.GetPlannedReleases;
using Mari.Application.Releases.Queries.GetRelease;
using Mari.Contracts.Common.Routes.Server;
using Mari.Contracts.Releases.DeleteRequests;
using Mari.Contracts.Releases.GetRequests;
using Mari.Contracts.Releases.PatchRequests;
using Mari.Contracts.Releases.PostRequests;
using Mari.Contracts.Releases.PutRequests;
using Mari.Contracts.Releases.Responses;
using Mari.Server.Controllers.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mari.Server.Controllers;

[Route(ServerRoutes.Controllers.Release)]
public class ReleaseController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ReleaseController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet(GetAllPlatformsRequest.ConstRouteTemplate)]
    public async Task<ActionResult<IEnumerable<PlatformResponse>>> GetAllPlatforms(CancellationToken token)
    {
        var query = new GetAllPlatformsQuery();
        var result = await _sender.Send(query, token);

        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<IEnumerable<PlatformResponse>>(result.Value);

        return Ok(response);
    }

    [HttpGet(GetReleaseByIdRequest.ConstRouteTemplate)]
    public async Task<ActionResult<ReleaseResponse>> GetReleaseById(
        [FromRoute] GetReleaseByIdRequest.Route route,
        CancellationToken token)
    {
        var query = _mapper.Map<GetReleaseByIdQuery>(route);
        var result = await _sender.Send(query, token);

        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<ReleaseResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet(GetCurrentReleasesRequest.ConstRouteTemplate)]
    public async Task<ActionResult<IEnumerable<ReleaseResponse>>> GetCurrentReleases(CancellationToken token)
    {
        var query = new GetCurrentReleasesQuery();
        var result = await _sender.Send(query, token);

        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<IEnumerable<ReleaseResponse>>(result.Value);

        return Ok(response);
    }

    [HttpGet(GetObsoleteReleasesRequest.ConstRouteTemplate)]
    public async Task<ActionResult<IEnumerable<ReleaseResponse>>> GetObsoleteReleases(CancellationToken token)
    {
        var query = new GetObsoleteReleasesQuery();
        var result = await _sender.Send(query, token);

        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<IEnumerable<ReleaseResponse>>(result.Value);

        return Ok(response);
    }

    [HttpGet(GetInWorkReleasesRequest.ConstRouteTemplate)]
    public async Task<ActionResult<IEnumerable<ReleaseResponse>>> GetInWorkReleases(CancellationToken token)
    {
        var query = new GetInWorkReleasesQuery();
        var result = await _sender.Send(query, token);

        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<IEnumerable<ReleaseResponse>>(result.Value);

        return Ok(response);
    }

    [HttpGet(GetPlannedReleasesRequest.ConstRouteTemplate)]
    public async Task<ActionResult<IEnumerable<ReleaseResponse>>> GetPlannedReleases(CancellationToken token)
    {
        var query = new GetPlannedReleasesQuery();
        var result = await _sender.Send(query, token);

        if (result.IsError) return Problem(result.Errors);
        var response = _mapper.Map<IEnumerable<ReleaseResponse>>(result.Value);

        return Ok(response);
    }

    [HttpPost(CreateReleaseRequest.ConstRouteTemplate)]
    public async Task<ActionResult> CreateRelease(
        [FromBody] CreateReleaseRequest.Body body,
        CancellationToken token)
    {
        var command = _mapper.Map<CreateReleaseCommand>(body);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok(); //TODO заменить на Created
    }

    [HttpPost(CreateDraftReleaseRequest.ConstRouteTemplate)]
    public async Task<ActionResult> CreateDraftRelease(
        [FromBody] CreateDraftReleaseRequest.Body body,
        CancellationToken token)
    {
        var command = _mapper.Map<CreateDraftReleaseCommand>(body);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpPost(CreateReleaseFromDraftRequest.ConstRouteTemplate)]
    public async Task<ActionResult> CreateReleaseFromDraft(
        [FromRoute] CreateReleaseFromDraftRequest.Route route,
        CancellationToken token)
    {
        var command = _mapper.Map<CreateReleaseFromDraftCommand>(route);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpPut(UpdateReleaseRequest.ConstRouteTemplate)]
    public async Task<ActionResult> UpdateRelease(
        [FromBody] UpdateReleaseRequest.Body body,
        CancellationToken token)
    {
        var command = _mapper.Map<UpdateReleaseCommand>(body);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpPut(UpdateDraftReleaseRequest.ConstRouteTemplate)]
    public async Task<ActionResult> UpdateDraftRelease(
        [FromBody] UpdateDraftReleaseRequest.Body body,
        CancellationToken token)
    {
        var command = _mapper.Map<UpdateDraftReleaseCommand>(body);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpDelete(DeleteReleaseRequest.ConstRouteTemplate)]
    public async Task<ActionResult> DeleteRelease(
        [FromRoute] DeleteReleaseRequest.Route route,
        CancellationToken token)
    {
        var command = _mapper.Map<DeleteReleaseCommand>(route);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpPatch(SetCompleteStatusRequest.ConstRouteTemplate)]
    public async Task<ActionResult> SetCompleteStatus(
        [FromRoute] SetCompleteStatusRequest.Route route,
        CancellationToken token)
    {
        var command = _mapper.Map<SetCompleteStatusCommand>(route);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }

    [HttpPatch(SetInWorkStatusRequest.ConstRouteTemplate)]
    public async Task<ActionResult> SetInWorkStatus(
        [FromRoute] SetInWorkStatusRequest.Route route,
        CancellationToken token)
    {
        var command = _mapper.Map<SetInWorkStatusCommand>(route);
        var result = await _sender.Send(command, token);

        if (result.IsError) return Problem(result.Errors);

        return Ok();
    }
}
