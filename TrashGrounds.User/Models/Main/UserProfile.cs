namespace TrashGrounds.User.Models.Main;

public record UserProfile(
    Guid Id,
    string Nickname,
    DateTime RegistrationDate,
    Guid? AvatarId,
    string? Status);