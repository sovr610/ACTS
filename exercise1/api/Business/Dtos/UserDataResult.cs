using StargateAPI.Business.Data;

namespace StargateAPI.Business.Dtos
{
    public class UserDataResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Account? UserData { get; set; }
        public Exception? Exception { get; set; }
    }
}
