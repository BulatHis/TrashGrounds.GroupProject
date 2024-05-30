using FluentValidation;
using TrashGrounds.Post.Features.Post.AddPost;
using TrashGrounds.Post.gRPC.Services;
using TrashGrounds.Post.Infrastructure.Constants;

namespace TrashGrounds.Post.Validators;

public class AddPostDtoValidator : AbstractValidator<AddPostEndpoint.AddPostDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public AddPostDtoValidator(FileExistsService fileExistsService)
    {
        _fileExistsService = fileExistsService;
        
        RuleFor(dto => dto.Text)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MaximumLength(3000)
            .WithMessage(ValidationFailedMessages.TooLongField);

        RuleFor(dto => dto.AssetId)
            .MustAsync(IsImageNullOrExistsAsync)
            .WithMessage(ValidationFailedMessages.NotExists);
    }
    
    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken) => 
        imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
}