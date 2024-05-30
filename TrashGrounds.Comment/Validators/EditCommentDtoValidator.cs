using FluentValidation;
using TrashGrounds.Comment.Features.Comment.EditComment;
using TrashGrounds.Comment.Infrastructure.Constants;

namespace TrashGrounds.Comment.Validators;

public class EditCommentDtoValidator : AbstractValidator<EditCommentEndpoint.EditCommentDto>
{
    public EditCommentDtoValidator()
    {
        RuleFor(dto => dto.Message)
            .NotEmpty()
            .WithMessage(ValidationFailedMessages.EmptyField)
            .MaximumLength(300)
            .WithMessage(ValidationFailedMessages.TooLongField);
    }
}