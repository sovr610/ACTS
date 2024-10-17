using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;


namespace StargateAPI.Business.Interfaces
{
    public interface IUserService
    {
        public Task<UserDataResult> GetUserData(string username);
        public Task<UserRegistrationResult> RegisterUser(RegisterModel model);
        public Task<AuthenticationResult> AuthenticateUser(string username, string password);

    }
}
