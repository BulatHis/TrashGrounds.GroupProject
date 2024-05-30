namespace TrashGrounds.Rate.Models.Additional.Post;

public record PostUserRateResponse(Guid UserId, Guid PostId, int Rate);