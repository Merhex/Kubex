namespace Kubex.DTO
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public AddressDTO Address { get; set; }
    }
}