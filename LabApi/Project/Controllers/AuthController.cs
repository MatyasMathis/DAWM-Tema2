using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class AuthController : Controller
    {
        private UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register/Student")]
        [AllowAnonymous]
        public IActionResult AddStudentUser(UserAddDto payload)
        {
            var result = _userService.RegisterStudent(payload);

            if (result == null)
            {
                return BadRequest("User cannot be added");
            }

            return Ok(result);
        }

        [HttpPost("/register/Professor")]
        [AllowAnonymous]
        public IActionResult AddProfessorUser(UserAddDto payload)
        {
            var result = _userService.RegisterProfessor(payload);

            if (result == null)
            {
                return BadRequest("User cannot be added");
            }

            return Ok(result);
        }

        [HttpGet("/get-all-users")]
        public ActionResult<List<UserAddDto>> GetAll()
        {
            var result = _userService.GetAll();

            return Ok(result);
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Login(UserAddDto payload)
        {
            var jwtToken = _userService.Validate(payload);

            return Ok(new { token = jwtToken });
        }
    }
}
