using System.Collections.Generic;
using Kubex.Models;

namespace Kubex.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyDTO Company { get; set; }
        public AddressDTO Address { get; set; }
        public LocationDTO Location { get; set; }
        public IEnumerable<DailyActivityReportDTO> DailyActivityReports { get; set; }
        public IEnumerable<PostRole> Roles { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}