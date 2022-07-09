using Auth0.ManagementApi.Api.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Auth0.ManagementApi.Api.Controllers {

    [Consumes("application/json", new string[] { "multipart/form-data" })]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller {
        private IConfiguration _configuration;
        private readonly IAuthService _authService;
        public AuthController(
            IConfiguration configuration,
            IAuthService authService) {
            this._configuration = configuration;
            this._authService = authService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Auth0Users")]
        public async Task<IActionResult> GetAuth0Users() {
            try {
                return Ok(await this._authService.GetAuth0Users());

            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Auth0Roles")]
        public async Task<IActionResult> GetAuth0Roles() {
            try {
                return Ok(await this._authService.GetAuth0Roles());

            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("Auth0User")]
        public async Task<IActionResult> CreateAuth0User() {
            try {
                return Ok(await this._authService.CreateAuth0User());

            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut("Assign/User/{userId}/Role/{roleId}")]
        public async Task<IActionResult> AssignRoleToAuth0User([FromRoute] string userId, [FromRoute]string roleId) {
            try {
                return Ok(await this._authService.AssignRoleToAuth0User(userId, roleId));

            } catch (Exception) {
                throw;
            }
        }

    }
}
