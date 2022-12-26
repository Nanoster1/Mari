using Mapster;
using Mari.Application.Releases.Commands.CreateDraftRelease;
using Mari.Application.Releases.Commands.CreateRelease;
using Mari.Application.Releases.Commands.CreateReleaseFromDraft;
using Mari.Application.Releases.Commands.DeleteRelease;
using Mari.Application.Releases.Commands.SetCompleteStatus;
using Mari.Application.Releases.Commands.SetInWorkStatus;
using Mari.Application.Releases.Commands.UpdateDraftRelease;
using Mari.Application.Releases.Commands.UpdateRelease;
using Mari.Application.Releases.Queries.GetRelease;
using Mari.Contracts.Releases.DeleteRequests;
using Mari.Contracts.Releases.GetRequests;
using Mari.Contracts.Releases.PatchRequests;
using Mari.Contracts.Releases.PostRequests;
using Mari.Contracts.Releases.PutRequests;
using Mari.Server.Mapping.Extensions;

namespace Mari.Server.Mapping.Configurations;

public class ReleaseMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.MapRequests<CreateReleaseRequest.Body, CreateReleaseCommand>();
        config.MapRequests<CreateDraftReleaseRequest.Body, CreateDraftReleaseCommand>();
        config.MapRequests<CreateReleaseFromDraftRequest.Route, CreateReleaseFromDraftCommand>();
        config.MapRequests<GetReleaseByIdRequest.Route, GetReleaseByIdQuery>();
        config.MapRequests<UpdateReleaseRequest.Body, UpdateReleaseCommand>();
        config.MapRequests<UpdateDraftReleaseRequest.Body, UpdateDraftReleaseCommand>();
        config.MapRequests<DeleteReleaseRequest.Route, DeleteReleaseCommand>();
        config.MapRequests<SetCompleteStatusRequest.Route, SetCompleteStatusCommand>();
        config.MapRequests<SetInWorkStatusRequest.Route, SetInWorkStatusCommand>();
    }
}
