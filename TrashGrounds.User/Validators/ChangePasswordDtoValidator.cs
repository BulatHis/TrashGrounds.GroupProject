using FluentValidation;
using TrashGrounds.User.Features.Auth.ChangePassword;
using TrashGrounds.User.Infrastructure.Constants;
using TrashGrounds.User.Services.Configs;

namespace TrashGrounds.User.Validators;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordEndpoint.ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(dto => dto.OldPassword)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);

        RuleFor(request => request.NewPassword)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MaximumLength(60)
            .WithMessage(ValidationFailedMessages.TooLongField)
            .MinimumLength(PasswordConfig.MinimumLength)
            .WithMessage(ValidationFailedMessages.TooShortField)
            .Matches(@"^[a-zA-Z0-9\W]+$")
            .WithMessage(ValidationFailedMessages.WrongSymbols)
            .Matches(@"[a-z]")
            .When(_ => PasswordConfig.RequireLowercase, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresLowercase)
            .Matches(@"[A-Z]")
            .When(_ => PasswordConfig.RequireUppercase, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresUppercase)
            .Matches(@"\d")
            .When(_ => PasswordConfig.RequireDigit, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresDigit)
            .Matches(@"\W")
            .When(_ => PasswordConfig.RequireNonAlphanumeric, ApplyConditionTo.CurrentValidator)
            .WithMessage(ValidationFailedMessages.RequiresNonAlphanumeric);
    }
}