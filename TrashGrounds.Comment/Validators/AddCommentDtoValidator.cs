using FluentValidation;
using TrashGrounds.Comment.Features.Comment.AddComment;
using TrashGrounds.Comment.Infrastructure.Constants;

namespace TrashGrounds.Comment.Validators;

public class AddCommentDtoValidator : AbstractValidator<AddCommentEndpoint.AddCommentDto>
{
    public AddCommentDtoValidator()
    {
        RuleFor(dto => dto.Message)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MaximumLength(300)
            .WithMessage(ValidationFailedMessages.TooLongField);
    }
}