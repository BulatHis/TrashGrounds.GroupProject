using FluentValidation;
using TrashGrounds.Post.Features.Post.EditPost;
using TrashGrounds.Post.gRPC.Services;
using TrashGrounds.Post.Infrastructure.Constants;

namespace TrashGrounds.Post.Validators;

public class EditPostDtoValidator : AbstractValidator<EditPostEndpoint.EditPostDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public EditPostDtoValidator(FileExistsService fileExistsService)
    {
        _fileExistsService = fileExistsService;
        
        RuleFor(dto => dto.Text)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MaximumLength(3000)
            .WithMessage(ValidationFailedMessages.TooLongField);

        RuleFor(dto => dto.AssetId)
            .MustAsync(IsImageNullOrExistsAsync)
            .When(dto => dto.ChangeAsset)
            .WithMessage(ValidationFailedMessages.NotExists);
    }
    
    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken) => 
        imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
}