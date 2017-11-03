using COmpStore.Schema.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokenAuthWebApiCore.Server.Filters;
using TokenAuthWebApiCore.Server.Models;

namespace TokenAuthWebApiCore.Server.Controllers.Web
{
    [Route("api/authen")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private IPasswordHasher<User> _passwordHasher;
        private IConfiguration _configuration;
        private ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager
            , IPasswordHasher<User> passwordHasher, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error", error.Description);
            }
            return BadRequest(result.Errors);
        }

        [ValidateForm]
        [HttpPost("CreateToken")]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized();
                }
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    var userClaims = await _userManager.GetClaimsAsync(user);

                    var claims = new[]
                    {
                          new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.Email, user.Email)
                        }.Union(userClaims);
                    Console.WriteLine(_configuration.GetSection("TokenAuthentication:SecretKey").Value);
                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("TokenAuthentication:SecretKey").Value));
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    Console.WriteLine("Now datetime {0}", DateTime.Now);
                    var a = DateTime.Now.AddDays(2);

                    var jwtSecurityToken = new JwtSecurityToken(
                      issuer: _configuration.GetSection("TokenAuthentication:Issuer").Value,
                      audience: _configuration.GetSection("TokenAuthentication:Audience").Value,
                      claims: claims,
                      expires: a,//DateTime.UtcNow.AddMinutes(60)
                      signingCredentials: signingCredentials
                      );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        expiration = a
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while creating token: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }
    }
}