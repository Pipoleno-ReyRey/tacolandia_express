using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class LoginController : ControllerBase{

    private readonly SignUpServices loginServices;
    public LoginController(SignUpServices loginServices){
        this.loginServices=loginServices;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user1){
        var user = await loginServices.Login(user1);
        if(user.id != null){
            var jwt = Jwt.CreateJWT(user.id, user.name, user.email, user.phone, user.password, user.role);
            return Ok(jwt);
        } else{
            return BadRequest(user.name);
        }
    }
}