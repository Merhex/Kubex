namespace Kubex.Models
{
    public class PostRole 
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string RoleId { get; set; }
    }
}