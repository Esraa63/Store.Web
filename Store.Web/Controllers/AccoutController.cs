using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.HandleResponses;
using Store.Service.Services.UserService;
using Store.Service.Services.UserService.Dtos;

namespace Store.Web.Controllers
{
    
    public class AccoutController : BaseController
    {
        private readonly IUserService _userService;

        public AccoutController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var user = await _userService.Login(input);
            if (user is null)
                return BadRequest(new CustomException(400,"Email Dose Not Exists"));
            return Ok(user);
        }
    }
}
