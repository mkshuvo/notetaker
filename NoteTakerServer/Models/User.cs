namespace NoteTakerServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string? AccessToken { get; set; }
    }
    public class UserError : User
    {
        public bool IsError { get; set; }
        public string? StatusCode { get; set; }
        public string? Message { get; set; }
    }
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
