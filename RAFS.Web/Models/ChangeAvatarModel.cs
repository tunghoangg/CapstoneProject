namespace RAFS.Web.Models
{
    public class ChangeAvatarModel
    {
        public string UserId { get; set; }

        public IFormFile Avatar { get; set;}
    }
}
