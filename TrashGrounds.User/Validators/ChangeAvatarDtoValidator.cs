using FluentValidation;
using TrashGrounds.User.Features.User.ChangePictureLink;
using TrashGrounds.User.gRPC.Services;
using TrashGrounds.User.Infrastructure.Constants;

namespace TrashGrounds.User.Validators;

public class ChangeAvatarDtoValidator : AbstractValidator<ChangeAvatarEndpoint.ChangeAvatarDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public ChangeAvatarDtoValidator(FileExistsService fileExistsService)
    {
        _fileExistsService = fileExistsService;

        RuleFor(dto => dto.NewAvatarId)
            .MustAsync(IsImageNullOrExistsAsync)
            .WithMessage(ValidationFailedMessages.NotExists);
    }
    
    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken) => 
        imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
}