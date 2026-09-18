using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tala.Core.Entities.Identity;
using Tala.Core.Services;
using Talab.Api.Dtos;
using Talab.Api.Extensions;

namespace Talab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> LogIn(LogInDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (EmailExists(model.Email).Result.Value)
                return BadRequest("Email already exists");

            var user = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0]
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<Address>> GetUserAddress()
        {
            var user = await _userManager.FindUserAddressByEmailAsync(User);
            var address = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            var address = _mapper.Map<AddressDto, Address>(updatedAddress);
            var user = await _userManager.FindUserAddressByEmailAsync(User);
            address.Id = user.Address.Id;
            user.Address = address;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Problem updating the user");

            return Ok(result);
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> EmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
