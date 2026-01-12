namespace EnglishCenterMVC.DTO
{
    public class LoginResponse
    {
        public UserResponseDto User { get; set; }
        public string Token { get; set; }   
        public IList<string> Roles { get; set; }
    }
}
