using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesKeeper.Common.Interfaces.BusinessLayer;
using NotesKeeper.Common.Models.AccountModels;
using NotesKeeper.WebApi.ViewModels;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <remarks>
        /// Sample request
        /// POST
        ///     {
        ///         "Email" : "string",
        ///         "Password" : "string"
        ///     }
        /// </remarks>
        /// <param name="loginViewModel">Login model.</param>
        /// <returns>User with auth Token.</returns>
        /// <response code="200">User is authenticated successfully.</response>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _accountService.LoginUser(_mapper.Map<LoginModel>(loginViewModel));

            if (user == null)
            {
                return BadRequest("User not found.");
            } else
            {
                return Ok(_mapper.Map<ApplicationUserViewModel>(user));
            }
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="registrationViewModel">Registration model.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel registrationViewModel)
        {
            var user = await _accountService.RegisterUser(_mapper.Map<RegisterModel>(registrationViewModel));

            if (user == null)
            {
                return BadRequest("User not found.");
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Login), nameof(AccountController), _mapper.Map<LoginViewModel>(user));
            }
        }
    }
}
