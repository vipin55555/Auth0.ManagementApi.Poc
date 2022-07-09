using Auth0.ManagementApi.Api.IServices;
using System.Threading.Tasks;
using Auth0User = Auth0.ManagementApi.Models.User;
using Auth0.ManagementApi.Paging;
using Microsoft.Extensions.Configuration;
using Auth0.ManagementApi.Api.Model;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Auth0.ManagementApi.Models;

namespace Auth0.ManagementApi.Api.Services {
    public class AuthService : IAuthService {
        private IConfiguration _configuration;

        public AuthService(IConfiguration configuration) {
            this._configuration = configuration;
        }

        /// <summary>
        /// Get Auth0 users
        /// </summary>
        /// <returns></returns>
        public async Task<IPagedList<Auth0User>> GetAuth0Users() {
            try {

                //Get Mgt API token
                Auth0TokenVM auth0mgtAPITokenResult = await this.GetAuth0ManagementToken();

                string auth0MgtAPIToken = auth0mgtAPITokenResult.AccessToken;

                //Cretae mgt API instance
                ManagementApiClient managementApiClient = new(auth0MgtAPIToken, _configuration["Auth0:Domain"]);

                //Get Auth0 users
                IPagedList<Auth0User> allAuth0Users = await managementApiClient.Users.GetAllAsync(new GetUsersRequest());

                return allAuth0Users;

            } catch (Exception e) {

                throw;
            }
        }

        /// <summary>
        /// Get Auth0 Roles
        /// </summary>
        /// <returns></returns>
        public async Task<IPagedList<Role>> GetAuth0Roles() {
            try {

                //Get Mgt API token
                Auth0TokenVM auth0mgtAPITokenResult = await this.GetAuth0ManagementToken();

                string auth0MgtAPIToken = auth0mgtAPITokenResult.AccessToken;

                //Cretae mgt API instance
                ManagementApiClient managementApiClient = new(auth0MgtAPIToken, _configuration["Auth0:Domain"]);

                // Get Auth0 roles
                IPagedList<Role> auth0Roles = await managementApiClient.Roles.GetAllAsync(new GetRolesRequest());

                return auth0Roles;

            } catch (Exception e) {

                throw;
            }
        }

        /// <summary>
        /// Create Auth0 user
        /// </summary>
        /// <returns></returns>
        public async Task<Auth0User> CreateAuth0User() {
            try {

                //Get Mgt API token
                Auth0TokenVM auth0mgtAPITokenResult = await this.GetAuth0ManagementToken();

                string auth0MgtAPIToken = auth0mgtAPITokenResult.AccessToken;

                //Cretae mgt API instance
                ManagementApiClient managementApiClient = new(auth0MgtAPIToken, _configuration["Auth0:Domain"]);

                // Create Auth0 user
                Auth0User createUser = await managementApiClient.Users.CreateAsync(new UserCreateRequest {
                    Email = "{Your_Email_Id}",
                    UserName = "{Your_UserName}", // Must not contain space with max limit of 14 characters
                    FirstName = "{Your_FirstName}",
                    Password = "{Your_Password}",
                    Connection = "{Your_Connection}" // By default we can use "Username-Password-Authentication"
                });

                return createUser;

            } catch (Exception e) {

                throw;
            }
        }


        /// <summary>
        /// Assign Role to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Auth0User> AssignRoleToAuth0User(string userId, string roleId) {
            try {

                //Get Mgt API token
                Auth0TokenVM auth0mgtAPITokenResult = await this.GetAuth0ManagementToken();

                string auth0MgtAPIToken = auth0mgtAPITokenResult.AccessToken;

                //Cretae mgt API instance
                ManagementApiClient managementApiClient = new(auth0MgtAPIToken, _configuration["Auth0:Domain"]);

                //Get role by id
                Role auth0Role = await managementApiClient.Roles.GetAsync(roleId);

                //If role found, return error
                if (auth0Role == null) {
                    throw new Exception("Role not found");
                }

                //Get user by id
                Auth0User auth0User = await managementApiClient.Users.GetAsync(userId);

                //If user not found, return error
                if (auth0User == null) {
                    throw new Exception("User not found");
                }

                // Assing role to Auth0 user 
                await managementApiClient.Users.AssignRolesAsync(auth0User.UserId, new AssignRolesRequest { Roles = new string[] { roleId } });

                return await managementApiClient.Users.GetAsync(userId);

            } catch (Exception e) {

                throw;
            }
        }

        /// <summary>
        /// Get Auth0 Management API token
        /// </summary>
        /// <returns></returns>
        private async Task<Auth0TokenVM> GetAuth0ManagementToken() {
            try {

                // Auth0 get access token Url
                string url = $"https://{_configuration["Auth0:Domain"]}/oauth/token";

                // Request body containing auth0 custom machine to machine app clientId, clientSecret and auth0 management Api audience
                var requestBody = new {
                    grant_type = "client_credentials",
                    client_id = _configuration["Auth0:ManagementApp:ClientId"],
                    client_secret = _configuration["Auth0:ManagementApp:ClientSecret"],
                    audience = _configuration["Auth0:ManagementApp:ApiAudience"]
                };

                //Create Http client instance
                HttpClient httpClient = new();

                // Create a post request to fetch access token along with expiry time and scope
                HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

                // Token response VM
                Auth0TokenVM auth0TokenResult = new();

                //If response is successfull then fetch and return auth0 token
                if (response.IsSuccessStatusCode) {
                    string result = await response.Content.ReadAsStringAsync();
                    auth0TokenResult = JsonConvert.DeserializeObject<Auth0TokenVM>(result);
                }

                return auth0TokenResult;

            } catch (Exception e) {

                throw;
            }
        }
    }
}
