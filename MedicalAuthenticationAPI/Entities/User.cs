namespace MedicalAuthenticationAPI.Entities
{
    public class User
    {
        public User(string userName, string email, string password, string hashedPassword, string salt, string role)
        {
            UserName = userName;
            Email = email;
            Password = password;
            HashedPassword = hashedPassword;
            Salt = salt;
            Role = role;
            CreatedAt = DateTime.Now;
        }

        public long Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string HashedPassword { get; set; }

        public string Salt { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

