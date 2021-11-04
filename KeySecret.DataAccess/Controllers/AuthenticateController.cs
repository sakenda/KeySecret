using KeySecret.DataAccess.Data;
using KeySecret.DataAccess.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KeySecret.DataAccess.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            ILogger<AuthenticateController> logger
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = new Response();
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                response.Status = "401 Unauthorized";
                response.Message = "Failed to authenticate. Check your credencials";

                _logger.LogError(response.Status + ": " + response.Message);
                return Unauthorized(response);
            }

            await GetUserCredencials(model, user);

            if (!model.IsValid())
            {
                response.Status = "401 Unauthorized";
                response.Message = "The stored data is not valid. Please";

                _logger.LogInformation(response.Status + ": " + response.Message);
                return Unauthorized(response);
            }

            response.Status = "200 OK";
            response.Message = "Successful authenticated.";

            _logger.LogInformation(response.Status + ": " + response.Message);
            return Ok(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = new Response();

            var resultFind = await _userManager.FindByNameAsync(model.Username);
            if (resultFind != null)
            {
                response.Status = "500 Internal Server Error";
                response.Message = "User already exists";

                _logger.LogError(response.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var resultCreate = await _userManager.CreateAsync(user, model.Password);
            if (!resultCreate.Succeeded)
            {
                response.Status = "500 Internal Server Error";
                response.Message = "User creation failed! Please check user details and try again.";

                _logger.LogError(response.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            response.Status = "200 OK";
            response.Message = "User successfully created";
            _logger.LogInformation(response.ToString());

            return Ok(response);
        }

        private async Task GetUserCredencials(LoginModel model, ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // Get JWT-Bearer-Token
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

            model.Token = new JwtSecurityTokenHandler().WriteToken(token);
            model.Expires = token.ValidTo;
        }
    }
}