using FluentValidation;
using TrashGrounds.Track.Features.Track.AddTrack;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure.Constants;

namespace TrashGrounds.Track.Validators;

public class AddTrackDtoValidator : AbstractValidator<AddTrackEndpoint.AddTrackDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public AddTrackDtoValidator(FileExistsService fileExistsService)
    {
        _fileExistsService = fileExistsService;
        
        RuleFor(dto => dto.Title)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MinimumLength(2)
            .WithMessage(ValidationFailedMessages.TooShortField)
            .MaximumLength(60)
            .WithMessage(ValidationFailedMessages.TooLongField)
            .Matches(@"^[\w\d\s\p{P}]+$")
            .WithMessage(ValidationFailedMessages.WrongSymbols);

        RuleFor(dto => dto.Description)
            .MinimumLength(3)
            .WithMessage(ValidationFailedMessages.TooShortField)
            .MaximumLength(300)
            .WithMessage(ValidationFailedMessages.TooLongField)
            .Matches(@"^[\w\d\s\p{P}]+$")
            .WithMessage(ValidationFailedMessages.WrongSymbols);

        RuleFor(dto => dto.Genres)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);

        RuleFor(dto => dto.MusicId)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MustAsync(IsMusicExistsAsync)
            .WithMessage(ValidationFailedMessages.NotExists);

        RuleFor(dto => dto.PictureId)
            .MustAsync(IsImageNullOrExistsAsync)
            .WithMessage(ValidationFailedMessages.NotExists);
    }
    
    private async Task<bool> IsMusicExistsAsync(Guid trackId, CancellationToken cancellationToken) => 
        await _fileExistsService.IsTrackExistsAsync(trackId);

    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken) =>
        imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
}