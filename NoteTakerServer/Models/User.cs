namespace NoteTakerServer.Models
{
    public class User
    {
        public string? UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string? AccessToken { get; set; }
        public DateTime ExpireTime { get; set; }
    }
    public class UserError
    {
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }
    }
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
