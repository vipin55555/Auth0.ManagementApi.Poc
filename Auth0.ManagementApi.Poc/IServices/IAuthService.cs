using System.Threading.Tasks;
using Auth0User = Auth0.ManagementApi.Models.User;
using Auth0.ManagementApi.Paging;
using Auth0.ManagementApi.Models;

namespace Auth0.ManagementApi.Api.IServices {
    public interface IAuthService {
        Task<IPagedList<Auth0User>> GetAuth0Users();
        Task<IPagedList<Role>> GetAuth0Roles();
        Task<Auth0User> CreateAuth0User();
        Task<Auth0User> AssignRoleToAuth0User(string userId, string roleId);
    }
}
