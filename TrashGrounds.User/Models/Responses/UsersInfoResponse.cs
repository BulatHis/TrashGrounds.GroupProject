using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Models.Responses;

public record UsersInfoResponse(IEnumerable<UserInfo> UsersInfo);