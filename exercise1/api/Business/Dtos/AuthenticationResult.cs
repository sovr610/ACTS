namespace StargateAPI.Business.Dtos
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        public Exception? Exception { get; set; }
    }
}
