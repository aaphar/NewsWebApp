using Application.Common.Results;

namespace Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string> GetUserNameAsync(int userId);

    Task<bool> IsInRoleAsync(int userId, string role);

    Task<bool> AuthorizeAsync(int userId, string policyName);

    Task<(Result results, int userId)> CreateUserAync(string userName, string password);

    Task<Result> DeleteUserAsync(int userId);

}

