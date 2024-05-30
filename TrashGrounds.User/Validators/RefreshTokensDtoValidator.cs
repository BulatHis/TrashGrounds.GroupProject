using FluentValidation;
using TrashGrounds.User.Features.Auth.RefreshTokens;
using TrashGrounds.User.Infrastructure.Constants;

namespace TrashGrounds.User.Validators;

public class RefreshTokensDtoValidator : AbstractValidator<RefreshTokensEndpoint.RefreshTokensDto>
{
    public RefreshTokensDtoValidator()
    {
        RuleFor(dto => dto.Token)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);

        RuleFor(dto => dto.RefreshToken)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);
    }
}