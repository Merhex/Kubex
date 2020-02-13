namespace Kubex.Models
{
    public class UserPost
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}