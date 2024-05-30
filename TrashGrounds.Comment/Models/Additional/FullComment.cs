namespace TrashGrounds.Comment.Models.Additional;

public class FullComment
{
    public Main.Comment? Comment { get; set; }
    
    public UserInformation? UserInfo { get; set; }
}