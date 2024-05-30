using FluentValidation;
using TrashGrounds.Track.Features.Track.UpdateTrack;
using TrashGrounds.Track.gRPC.Services;
using TrashGrounds.Track.Infrastructure.Constants;

namespace TrashGrounds.Track.Validators;

public class UpdateTrackDtoValidator : AbstractValidator<UpdateTrackEndpoint.UpdateTrackDto>
{
    private readonly FileExistsService _fileExistsService;
    
    public UpdateTrackDtoValidator(FileExistsService fileExistsService)
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

        RuleFor(dto => dto.NewMusicId)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MustAsync(IsMusicExistsAsync)
            .When(dto => dto.ChangeMusic)
            .WithMessage(ValidationFailedMessages.NotExists);
        
        RuleFor(dto => dto.NewPictureId)
            .MustAsync(IsImageNullOrExistsAsync)
            .When(dto => dto.ChangePicture)
            .WithMessage(ValidationFailedMessages.NotExists);;
    }

    private async Task<bool> IsMusicExistsAsync(Guid? trackId, CancellationToken cancellationToken)
    {
        return trackId != null && await _fileExistsService.IsTrackExistsAsync(trackId.Value);
    }

    private async Task<bool> IsImageNullOrExistsAsync(Guid? imageId, CancellationToken cancellationToken)
    {
        return imageId == null || await _fileExistsService.IsImageExistsAsync(imageId.Value);
    }
}