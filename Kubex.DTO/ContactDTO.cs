namespace Kubex.DTO
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string UserName { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}