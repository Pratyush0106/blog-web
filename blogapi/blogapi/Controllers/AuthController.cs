
//using BlogApi.Data;
//using BlogApi.Model;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System.Threading.Tasks;
//using blogapi.Model;
//using blogapi.Service;

//namespace BlogApi.Controllers
//{
//    [Route("api/auth")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly AppDbContext _context;
//        private readonly IConfiguration _config;
//        private readonly EmailService _emailService;
//        private readonly ILogger<AuthController> _logger;
//        public AuthController(AppDbContext context, IConfiguration config, EmailService emailService, ILogger<AuthController> logger)
//        {
//            _context = context;
//            _config = config;
//            _emailService = emailService;
//            _logger = logger;

//        }

//        //[HttpPost("SignUp")]
//        //public async Task<IActionResult> SignupAsync([FromBody] User user)
//        //{
//        //    if (_context.Users.Any(u => u.Username == user.Username))
//        //    {
//        //        return BadRequest(new { message = "Username already exists" });
//        //    }
//        //    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
//        //    user.CreationDate = DateTime.UtcNow; // Set the creation date
//        //    _context.Users.Add(user);
//        //    _context.SaveChanges();

//        //    var subject = "Registration Successful";
//        //    var message = $"Hello {user.FullName},\n\nYou have successfully registered at BloodBank.";
//        //    await _emailService.SendEmailAsync(user.Email, subject, message);

//        //    return CreatedAtAction(nameof(SignupAsync), new { id = user.Id });
//        //}


//        [HttpGet("{id}")]
//        [Authorize( policy: "AdminOnly")]
//         public async Task<ActionResult<IEnumerable<User>>> GetUser()
//        {
//            return await _context.Users
//                 .Select(p => new User
//                 {
//                     Id = p.Id,
//                     Username = p.Username,
//                     FullName = p.FullName,
//                     Email = p.Email

//                 })
//            .ToListAsync();
//        }



//        [HttpPost("SignUp")]
//        public async Task<IActionResult> SignupAsync([FromBody] User user)
//        {
//            if (_context.Users.Any(u => u.Username == user.Username || u.Email == user.Email))
//            {
//                return BadRequest(new { message = "Username or Email already exists" });
//            }
//            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
//            user.CreationDate = DateTime.UtcNow; // Set the creation date
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            var subject = "Registration Successful";
//            var message = $"Hello {user.FullName},\n\nYou have successfully registered at Dynamic Blog Web.";
//            await _emailService.SendEmailAsync(user.Email, subject, message);

//            _logger.LogInformation($"User {user.Username} created a new account.");
//            return Ok(new { message = "Account Created successfully" });

//            //return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);


//        }



//        // Admin Password = adminpassword



//        // Login user

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginRequest loginRequest)
//        {
//            var user = _context.Users.FirstOrDefault(u => u.Username == loginRequest.Username);
//            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
//            {
//                _logger.LogWarning($"User failed to login.");
//                return Unauthorized(new { message = "Invalid Credentials." });
//            }
//            var token = GenerateJwtToken(user);
//            _logger.LogInformation($"User {user.Username} logged in.");
//            return Ok(new { message = "Login successfully", token, user.Id });
//        }

//        //update user information

//        [Authorize]
//        [HttpPut("UpdateUser")]
//        public IActionResult UpdateUser([FromBody] UpdateUser updateUserRequest)
//        {
//            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (username == null)
//            {
//                return Unauthorized(new { message = "User not found" });
//            }

//            var user = _context.Users.FirstOrDefault(u => u.Username == username);
//            if (user == null)
//            {
//                return NotFound(new { message = "User not found" });
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            user.FullName = updateUserRequest.FullName;


//            user.Email = updateUserRequest.Email;
//            if (!string.IsNullOrEmpty(updateUserRequest.Password))
//            {
//                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserRequest.Password);
//            }

//            _context.Users.Update(user);
//            _context.SaveChanges();

//            return Ok(new { message = "User information updated successfully" });
//        }


//        //[Route("ForgotPassword")]
//        //[HttpPost]
//        // public async Task<IActionResult> ForgotPassword(ForgotPassWordDto request)
//        // {
//        //     if (ModelState.IsValid)
//        //     {
//        //         var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
//        //         if (user == null)
//        //             return BadRequest("Invalid Username.");

//        //         var token = GeneratePasswordResetToken(user);

//        //         if (string.IsNullOrEmpty(token))
//        //             return BadRequest("Failed to generate password reset token.");

//        //         var callbackUrl = $"http://localhost:9999/restore?code={token}&email={user.Username}";

//        //         return Ok(new
//        //         {
//        //             message = "Password reset token generated successfully.",
//        //             token = token,
//        //             username = user.Username



//        //         });





//        //     }


//        //     return BadRequest("Invalid payload.");


//        // }



//        [Route("ForgotPassword")]
//        [HttpPost]
//        public async Task<IActionResult> ForgotPassword(ForgotPassWordDto request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(new { message = "Invalid request", errors = ModelState });
//            }

//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
//            if (user == null)
//            {
//                return BadRequest(new { message = "Invalid Username." });
//            }

//            var token = GeneratePasswordResetToken(user);

//            if (string.IsNullOrEmpty(token))
//            {
//                return BadRequest(new { message = "Failed to generate password reset token." });
//            }

//            var callbackUrl = $"http://localhost:9999/restore?code={token}&email={user.Username}";

//            // Send the token via email
//            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Please reset your password by clicking here: {token}");

//            return Ok(new
//            {
//                message = "Password reset token generated and sent to your email successfully.",
//                //token = token,
//                username = user.Username
//            });
//        }

//        // Reset Password Code


//        [Route("ResetPassword")]
//        [HttpPost]
//        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(new { message = "Invalid request", errors = ModelState });
//            }

//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
//            if (user == null)
//            {
//                return BadRequest(new { message = "Invalid  Username." });
//            }

//            Console.WriteLine(request.Token);
//            // Verify the token
//            var isTokenValid = VerifyPasswordResetToken(user, request.Token);
//            if (!isTokenValid)
//            {
//                return BadRequest(new { message = "Invalid or expired token." });
//            }

//            // Hash the new password
//            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
//            _context.Users.Update(user);
//            await _context.SaveChangesAsync();

//            return Ok(new { message = "Password has been reset successfully." });
//        }




//        //private bool VerifyPasswordResetToken(User user, string token)
//        //{
//        //    var tokenHandler = new JwtSecurityTokenHandler();
//        //    var key = Encoding.ASCII.GetBytes(_config["jwtSettings:key"]);

//        //    try
//        //    {
//        //        var tokenValidationParameters = new TokenValidationParameters
//        //        {
//        //            ValidateIssuerSigningKey = true,
//        //            IssuerSigningKey = new SymmetricSecurityKey(key),
//        //            ValidateIssuer = false,  // Keep these enabled for production
//        //            ValidIssuer = _config["jwtSettings:issuer"], // Make sure these match the token generation
//        //            ValidateAudience = false, // Keep these enabled for production
//        //            ValidAudience = _config["jwtSettings:audience"], // Make sure these match the token generation
//        //            ClockSkew = TimeSpan.Zero // Or a small value like TimeSpan.FromMinutes(1) if needed
//        //        };

//        //        SecurityToken validatedToken;
//        //        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

//        //        var jwtToken = (JwtSecurityToken)validatedToken;

//        //        //// CRITICAL: Log these values for debugging!
//        //        //Console.WriteLine($"Token: {token}");
//        //        //Console.WriteLine($"Decoded Token: {jwtToken}");
//        //        //Console.WriteLine($"User ID from Token: {jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value}");
//        //        //Console.WriteLine($"User ID from Database: {user.Id}");

//        //        //if (jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value == null)
//        //        //{
//        //        //    Console.WriteLine("ClaimTypes.NameIdentifier is null in token");
//        //        //    return false;
//        //        //}

//        //        var userIdFromToken = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

//        //        return true;
//        //    }
//        //    catch
//        //    {

//        //        return false;
//        //    }
//        //}


//        private bool VerifyPasswordResetToken(User user, string token)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_config["jwtSettings:key"]);

//            try
//            {
//                var tokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidIssuer = _config["jwtSettings:issuer"],
//                    ValidateAudience = false,
//                    ValidAudience = _config["jwtSettings:audience"],
//                    ClockSkew = TimeSpan.Zero
//                };

//                SecurityToken validatedToken;
//                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

//                var jwtToken = (JwtSecurityToken)validatedToken;

//                // Log token details for debugging
//                Console.WriteLine($"Token: {token}");
//                Console.WriteLine($"Decoded Token: {jwtToken}");

//                // Map the claim type if necessary
//                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
//                if (userIdClaim == null)
//                {
//                    Console.WriteLine("JwtRegisteredClaimNames.Sub is missing in the token");
//                    return false;
//                }

//                var userIdFromToken = int.Parse(userIdClaim.Value);

//                Console.WriteLine($"User ID from Token: {userIdFromToken}");
//                Console.WriteLine($"User ID from Database: {user.Id}");

//                return userIdFromToken == user.Id;
//            }
//            catch (Exception ex)
//            {
//                // Log the exception for debugging
//                Console.WriteLine($"Token validation failed: {ex.Message}");
//                return false;
//            }
//        }

//        // generate token for the reset password code
//        //private string GeneratePasswordResetToken(User user)
//        //{
//        //    var tokenHandler = new JwtSecurityTokenHandler();
//        //    var key = Encoding.ASCII.GetBytes(_config["jwtSettings:key"]);
//        //    var tokenDescriptor = new SecurityTokenDescriptor
//        //    {
//        //        Subject = new ClaimsIdentity(new Claim[]
//        //        {
//        //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
//        //        }),
//        //        Expires = DateTime.Now.AddMinutes(30),
//        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//        //    };
//        //    var token = tokenHandler.CreateToken(tokenDescriptor);
//        //    return tokenHandler.WriteToken(token);
//        //}

//        private string GeneratePasswordResetToken(User user)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_config["jwtSettings:key"]);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()) // Use JwtRegisteredClaimNames.Sub for the subject claim
//                }),
//                Expires = DateTime.Now.AddMinutes(30),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }




//        // delete user code 

//        [Authorize(Policy = "AdminOnly")]
//        [HttpDelete("deleteUser/{id}")]
//        public IActionResult DeleteUser(int id)
//        {
//            var user = _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.Id == id);
//            if (user == null)
//            {
//                return NotFound(new { message = "User not found" });
//            }

//            _context.Users.Remove(user);
//            _context.SaveChanges();
//            return Ok(new { message = "User and associated posts deleted successfully" });
//        }


//        // Jwt token generation code
//        private string GenerateJwtToken(User user)
//        {
//            var jwtKey = _config["jwtSettings:key"];
//            if (string.IsNullOrEmpty(jwtKey))
//            {
//                throw new ArgumentNullException(nameof(jwtKey), "Jwt:mypersonalkeyforaccessallthefile");
//            }

//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                new Claim(ClaimTypes.NameIdentifier, user.Username),
//                new Claim(ClaimTypes.Role, user.Username == "admin" ? "Admin" : "User")
//            };

//            var token = new JwtSecurityToken(
//                issuer: _config["jwtSettings:issuer"],
//                audience: _config["jwtSettings:audience"],
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(120),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }


//    }
//}

using BlogApi.Data;
using BlogApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using blogapi.Model;
using blogapi.Service;

namespace BlogApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly EmailService _emailService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(AppDbContext context, IConfiguration config, EmailService emailService, ILogger<AuthController> logger)
        {
            _context = context;
            _config = config;
            _emailService = emailService;
            _logger = logger;

        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users
                .Select(p => new User
                {
                    Id = p.Id,
                    Username = p.Username,
                    FullName = p.FullName,
                    Email = p.Email
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Policy = "UserOrAdmin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .Select(p => new User
                {
                    Id = p.Id,
                    Username = p.Username,
                    FullName = p.FullName,
                    Email = p.Email
                })
                .ToListAsync();
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignupAsync([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username || u.Email == user.Email))
            {
                return BadRequest(new { message = "Username or Email already exists" });
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreationDate = DateTime.UtcNow; // Set the creation date
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var subject = "Registration Successful";
            var message = $"Hello {user.FullName},\n\nYou have successfully registered at Dynamic Blog Web.";
            await _emailService.SendEmailAsync(user.Email, subject, message);

            _logger.LogInformation($"User {user.Username} created a new account.");
            return Ok(new { message = "Account Created successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginRequest.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                _logger.LogWarning($"User failed to login.");
                return Unauthorized(new { message = "Invalid Credentials." });
            }
            var token = GenerateJwtToken(user);
            _logger.LogInformation($"User {user.Username} logged in.");
            return Ok(new { message = "Login successfully", token, user.Id, user.FullName, user.Email, user.Username });
        }


        [Authorize]
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUser updateUserRequest)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (username == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(updateUserRequest.FullName))
            {
                user.FullName = updateUserRequest.FullName;
            }

            if (!string.IsNullOrEmpty(updateUserRequest.Email))
            {
                user.Email = updateUserRequest.Email;
            }

            if (!string.IsNullOrEmpty(updateUserRequest.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserRequest.Password);
            }

            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok(new { message = "User information updated successfully" });
        }


        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPassWordDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid request", errors = ModelState });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid Username." });
            }

            var token = GeneratePasswordResetToken(user);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Failed to generate password reset token." });
            }

            var callbackUrl = $"http://localhost:9999/restore?code={token}&email={user.Username}";

            // Send the token via email
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Please reset your password by clicking here: {token}");

            return Ok(new
            {
                message = "Password reset token generated and sent to your email successfully.",
                username = user.Username
            });
        }

        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid request", errors = ModelState });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid  Username." });
            }

            Console.WriteLine(request.Token);
            // Verify the token
            var isTokenValid = VerifyPasswordResetToken(user, request.Token);
            if (!isTokenValid)
            {
                return BadRequest(new { message = "Invalid or expired token." });
            }

            // Hash the new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Password has been reset successfully." });
        }

        private bool VerifyPasswordResetToken(User user, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["jwtSettings:key"]);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidIssuer = _config["jwtSettings:issuer"],
                    ValidateAudience = false,
                    ValidAudience = _config["jwtSettings:audience"],
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Log token details for debugging
                Console.WriteLine($"Token: {token}");
                Console.WriteLine($"Decoded Token: {jwtToken}");

                // Map the claim type if necessary
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
                if (userIdClaim == null)
                {
                    Console.WriteLine("JwtRegisteredClaimNames.Sub is missing in the token");
                    return false;
                }

                var userIdFromToken = int.Parse(userIdClaim.Value);

                Console.WriteLine($"User ID from Token: {userIdFromToken}");
                Console.WriteLine($"User ID from Database: {user.Id}");

                return userIdFromToken == user.Id;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return false;
            }
        }

        private string GeneratePasswordResetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["jwtSettings:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()) // Use JwtRegisteredClaimNames.Sub for the subject claim
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpDelete("deleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Include(u => u.Posts).FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(new { message = "User and associated posts deleted successfully" });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _config["jwtSettings:key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException(nameof(jwtKey), "Jwt:mypersonalkeyforaccessallthefile");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Username == "admin" ? "Admin" : "User")
            };

            var token = new JwtSecurityToken(
                issuer: _config["jwtSettings:issuer"],
                audience: _config["jwtSettings:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
