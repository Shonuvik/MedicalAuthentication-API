namespace MedicalAuthenticationAPI.Controllers.V1
{
    public class UserCreateDto
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string CRM { get; set; }
    }
}
