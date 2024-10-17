using Microsoft.IdentityModel.Tokens;
using StargateAPI.Business.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Interfaces;


namespace StargateAPI.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly StargateContext _context;

        public UserService(StargateContext context, IConfiguration configuration, ILogger<UserService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }


        public async Task<UserDataResult> GetUserData(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new UserDataResult
                {
                    Success = false,
                    Message = "Username cannot be null or empty"
                };
            }

            try
            {
                var user = await _context.Accounts.FirstOrDefaultAsync(x => x.UserName == username);

                if (user == null)
                {
                    return new UserDataResult
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                return new UserDataResult
                {
                    Success = true,
                    Message = "User data retrieved successfully",
                    UserData = user
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UserService.GetUserData for username {Username}", username);
                return new UserDataResult
                {
                    Success = false,
                    Message = "An error occurred while retrieving user data",
                    Exception = ex
                };
            }
        }

        public async Task<UserRegistrationResult> RegisterUser(RegisterModel model)
        {
            if (model == null)
            {
                return new UserRegistrationResult { Success = false, Message = "Registration model is null" };
            }

            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Email))
            {
                return new UserRegistrationResult { Success = false, Message = "Username, password, and email are required" };
            }

            var existingUser = await _context.Accounts.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);
            if (existingUser != null)
            {
                return new UserRegistrationResult
                {
                    Success = false,
                    Message = existingUser.UserName == model.UserName ? "Username already exists" : "Email already in use"
                };
            }

            var user = new Account
            {
                UserName = model.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Email = model.Email,
                AccountCreated = DateTime.UtcNow,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            try
            {
                _context.Accounts.Add(user);
                await _context.SaveChangesAsync();

                var token = await AuthenticateUser(user.UserName, model.Password);
                if (token.Success == false)
                {
                    return new UserRegistrationResult { Success = false, Message = token.Message };
                }

                return new UserRegistrationResult { Success = true, Message = "User registered successfully", Token = token.Token };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UserService.RegisterUser for user {UserName}", model.UserName);
                return new UserRegistrationResult { Success = false, Message = "An error occurred during registration" };
            }
        }

        public async Task<AuthenticationResult> AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "Username and password are required"
                };
            }

            try
            {
                var user = await _context.Accounts.SingleOrDefaultAsync(u => u.UserName == username);

                if (user == null)
                {
                    return new AuthenticationResult
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new AuthenticationResult
                    {
                        Success = false,
                        Message = "Invalid password"
                    };
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, user.UserName)
            }),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenString = tokenHandler.WriteToken(token);

                // Update last login time or any other user properties if needed
                user.LastLoginDate = DateTime.UtcNow;
                _context.Accounts.Update(user);
                await _context.SaveChangesAsync();

                return new AuthenticationResult
                {
                    Success = true,
                    Message = "Authentication successful",
                    Token = tokenString
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UserService.AuthenticateUser for username {Username}", username);
                return new AuthenticationResult
                {
                    Success = false,
                    Message = "An error occurred during authentication"
                };
            }
        }
    }
}
