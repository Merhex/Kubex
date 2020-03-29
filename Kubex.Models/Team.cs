namespace Kubex.Models
{
    public class Team
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int RoleTypeId { get; set; }
        public virtual RoleType RoleType { get; set; }
    }
}