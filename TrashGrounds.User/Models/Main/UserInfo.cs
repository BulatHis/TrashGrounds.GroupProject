namespace TrashGrounds.User.Models.Main;

public class UserInfo
{
    public required Guid Id { get; set; }
    
    public required string Nickname { get; set; }
    
    public required DateTime RegistrationDate { get; set; }
    
    public string? AvatarLink { get; set; }
}