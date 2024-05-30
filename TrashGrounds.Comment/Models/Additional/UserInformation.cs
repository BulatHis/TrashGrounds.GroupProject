namespace TrashGrounds.Comment.Models.Additional;

public class UserInformation
{
    public required Guid Id { get; set; }
    
    public required string Nickname { get; set; }
    
    public Guid? AvatarId { get; set; }
    
    public DateTime RegistrationDate { get; set; }
}