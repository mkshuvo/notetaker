using NoteTakerServer.DataStorage;
using NoteTakerServer.Models;
namespace NoteTakerServer.Services
{
    public class AuthService
    {
        private readonly List<User> _users;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(JwtTokenService jwtTokenService)
        {
            _users = FileStorage.LoadUsers();
            _jwtTokenService = jwtTokenService;
        }
        public List<User> Signup(User user)
        {
            try
            {
                var token = _jwtTokenService.GenerateJwtToken(user);
                user.AccessToken = token;
                _users.Add(user);
                return FileStorage.SaveUsers(_users);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during signup", ex);
            }
        }
        public User ValidateUser(string email, string password)
        {
            return _users.FirstOrDefault(u => u.UserName == email && u.Password == password);
        }
        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email) ?? new UserError() { IsError = true, Message = "User not found"};
        }
        public bool IsUserExists(string email)
        {
            return _users.Any(u => u.Email == email);
        }
        public List<User> GetUsers()
        {
            return _users;
        }
        public string UpdateAccessToken(User user)
        {
            try
            {
                var token = _jwtTokenService.GenerateJwtToken(user);
                var existingUser = _users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    existingUser.AccessToken = token;
                    FileStorage.SaveUsers(_users);
                    return token;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating access token", ex);
            }
            return string.Empty;
        }
        public void UpdateUser(User user)
        {
            try
            {
                var existingUser = _users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    existingUser.UserName = user.UserName;
                    existingUser.DateOfBirth = user.DateOfBirth;
                    existingUser.Password = user.Password;
                    FileStorage.SaveUsers(_users);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating user", ex);
            }
        }
        public void DeleteUser(string email) 
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    _users.Remove(user);
                    FileStorage.SaveUsers(_users);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting user", ex);
            }
        }
    }
}
