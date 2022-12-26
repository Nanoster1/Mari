using ErrorOr;
using Mari.Application.Common.Interfaces.Persistence;
using MediatR;
using Mari.Domain.Common;
using FluentValidation;
using Mari.Domain.Releases;
using Mari.Application.Common.Extensions;

namespace Mari.Application.Releases.Commands.CreateReleaseFromDraft;

internal class CreateReleaseFromDraftCommandHandler : IRequestHandler<CreateReleaseFromDraftCommand, ErrorOr<Created>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IValidator<Release> _releaseValidator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReleaseFromDraftCommandHandler(
        IReleaseRepository releaseRepository,
        IValidator<Release> releaseValidator,
        IUnitOfWork unitOfWork)
    {
        _releaseRepository = releaseRepository;
        _releaseValidator = releaseValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Created>> Handle(CreateReleaseFromDraftCommand request, CancellationToken cancellationToken)
    {
        var draftRelease = await _releaseRepository.GetById(request.ReleaseId);
        if (draftRelease is null) return Errors.Release.ReleaseNotFound;

        //Необходимо проверить релиз, т.к. черновик вообще не валидируется
        var validationResult = await _releaseValidator.ValidateAsync(draftRelease);
        if (!validationResult.IsValid) return validationResult.Errors.ToDomainErrors();

        var result = draftRelease.CreateFromDraft();
        if (result.IsError) return result.Errors;

        await _releaseRepository.Update(draftRelease, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Created;
    }
}
