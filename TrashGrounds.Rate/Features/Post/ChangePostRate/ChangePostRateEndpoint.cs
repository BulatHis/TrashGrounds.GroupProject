using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Constants;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Infrastructure.ValidationSetup;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Post.ChangePostRate;

public class ChangePostRateEndpoint : IEndpoint
{
    private readonly IUserService _userService;

    public ChangePostRateEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public record ChangePostRateDto(int NewRate);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/",
                async ([FromRoute] Guid postId, ChangePostRateDto dto, IMediator mediator) =>
                    Results.Ok(await mediator.Send(
                        new ChangePostRateCommand(
                            _userService.GetUserIdOrThrow(),
                            postId,
                            dto.NewRate))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<ChangePostRateDto>());
    }
    
    public class ChangePostRateDtoValidator : AbstractValidator<ChangePostRateDto>
    {
        public ChangePostRateDtoValidator()
        {
            RuleFor(dto => dto.NewRate)
                .Must(rate => rate is 1 or -1)
                .WithMessage(ValidationFailedMessages.IncorrectValue);
        }
    }
}