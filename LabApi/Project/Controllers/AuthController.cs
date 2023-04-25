using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register/Student")]
        public IActionResult AddStudentUser(UserAddDto payload)
        {
            var result = _userService.AddStudent(payload);

            if (result == null)
            {
                return BadRequest("User cannot be added");
            }

            return Ok(result);
        }

        [HttpPost("/register/Professor")]
        public IActionResult AddProfessorUser(UserAddDto payload)
        {
            var result = _userService.AddProfessor(payload);

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
    }
}
