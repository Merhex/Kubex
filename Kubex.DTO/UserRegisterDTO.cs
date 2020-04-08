namespace Kubex.DTO
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HouseNumber { get; set; }
        public string AppartementBus { get; set; }
        public string Street { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
        public bool isAgent { get; set; }
        public bool isCompany { get; set; }
    }
}