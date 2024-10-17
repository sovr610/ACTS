namespace StargateAPI.Business.Dtos
{
    public class UserRegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
        public Exception? Exception { get; set; }
    }
}
