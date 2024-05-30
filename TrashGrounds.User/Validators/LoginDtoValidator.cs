using FluentValidation;
using TrashGrounds.User.Features.Auth.Login;
using TrashGrounds.User.Infrastructure.Constants;

namespace TrashGrounds.User.Validators;

public class LoginDtoValidator : AbstractValidator<LoginEndpoint.LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .EmailAddress()
            .WithMessage(ValidationFailedMessages.IncorrectEmail);

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField);
    }
}