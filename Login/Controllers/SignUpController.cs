using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class SignUpController : ControllerBase{

    private readonly SignUpServices signUpServices;
    public SignUpController(SignUpServices signUpServices){
        this.signUpServices=signUpServices;
    }

    [HttpGet("sign_up/")]
    public async Task<IActionResult> SignUp(string? email, string? password){
        UserDTO userDTO = new UserDTO(){email = email, password = password};
        var user = await signUpServices.SignUp(userDTO);
        if(user.id != null){
            var jwt = Jwt.CreateJWT(user.id, user.name, user.email, user.phone, user.password, user.role);
            return Ok(jwt);
        } else {
            return BadRequest(user.name);
        }
    }
}