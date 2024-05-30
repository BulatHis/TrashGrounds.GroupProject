using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrashGrounds.Rate.Infrastructure.Constants;
using TrashGrounds.Rate.Infrastructure.Routing;
using TrashGrounds.Rate.Infrastructure.ValidationSetup;
using TrashGrounds.Rate.Services.Interfaces;

namespace TrashGrounds.Rate.Features.Track.ChangeTrackRate;

public class ChangeTrackRateEndpoint : IEndpoint
{
    public record ChangeTrackRateDto(int NewRate);
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/",
                async ([FromRoute] Guid trackId, ChangeTrackRateDto dto,
                        IUserService userService, IMediator mediator) =>
                    Results.Ok(await mediator.Send(new ChangeTrackRateCommand(
                        userService.GetUserIdOrThrow(),
                        trackId,
                        dto.NewRate))))
            .RequireAuthorization()
            .AddValidation(builder => builder.AddFor<ChangeTrackRateDto>());
    }
    
    public class ChangeTrackRateDtoValidator : AbstractValidator<ChangeTrackRateDto>
    {
        public ChangeTrackRateDtoValidator()
        {
            RuleFor(dto => dto.NewRate)
                .GreaterThanOrEqualTo(1)
                .WithMessage(ValidationFailedMessages.IncorrectValue)
                .LessThanOrEqualTo(5)
                .WithMessage(ValidationFailedMessages.IncorrectValue);
        }
    }
}