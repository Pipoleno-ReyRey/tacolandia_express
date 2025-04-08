using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public class SignUpServices{
    private readonly LoginDB loginDB;
    public SignUpServices(LoginDB loginDB){
        this.loginDB = loginDB;
    }

    public async Task<User> SignUp(UserDTO userDTO){
        try{
            var user = await loginDB.users.FirstOrDefaultAsync(u => u.email == userDTO.email && u.password == userDTO.password);
            if (user == null){
                return new User(){
                id = null,
                name = "no encontrado"
                };
            } else{
                return user!;
            }
            
        } catch(Exception error){
            return new User(){
                name = error.Message
            };
        }
    }

    public async Task<User> Login(User user){
        try{
            if(loginDB.users.Any(us => us.email == user.email)){
                return new User(){id = null, name = "usuario existente"};
            } else{
                await loginDB.users.AddAsync(user);
                await loginDB.SaveChangesAsync();
                return user;
            }
        }catch(MySqlException error){
            return new User(){name = error.Message}; 
        }
    }
}