namespace MyLeasing.Web.Helpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using MyLeasing.Web.Models;
    using Web.Data.Entities;
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

    }
}
