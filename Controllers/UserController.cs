using Microsoft.AspNetCore.Mvc;
using MOD9.Data;
using MOD9.Models.Dto;

namespace MOD9.Controllers
{
    [Route("api/UserAPI")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetVillas()
        {
            return Ok(UserData.userList);
        }

        [HttpGet("{Id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]

        public ActionResult<UserDTO> GetUser(int id)
        {
            if (id == 0) return BadRequest();
            var acc = UserData.userList.FirstOrDefault(u => u.Id == id);
            if (acc == null) return NotFound();
            return Ok(acc);
        }   

        [HttpPost("login")]
        public ActionResult<UserDTO> LoginAcc([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Email/Password Invalid");
            }
            
            var user=UserData.userList.FirstOrDefault(u=>u.Email==userDTO.Email);
            if (user == null)
            {
                return NotFound("Email tidak ditemukan");
            }

            if(user.Password!=userDTO.Password)
            {
                return Unauthorized("Password Salah");
            }
            return Ok("berhasil login " );
        }

    }
};