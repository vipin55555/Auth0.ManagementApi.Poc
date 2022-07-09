using Auth0.ManagementApi.Api.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Auth0.ManagementApi.Api.Controllers {

    [Consumes("application/json", new string[] { "multipart/form-data" })]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) {
            this._authService = authService;
        }

        /// <summary>
        /// Get Auth0 users
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
        /// Get Auth0 Roles
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
        /// Create Auth0 user
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
        /// Assign Role to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
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
