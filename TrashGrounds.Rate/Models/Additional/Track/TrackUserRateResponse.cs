namespace TrashGrounds.Rate.Models.Additional.Track;

public record TrackUserRateResponse(Guid UserId, Guid TrackId, int Rate);