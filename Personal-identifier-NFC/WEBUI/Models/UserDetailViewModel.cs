using WEBUI.Entities;

namespace WEBUI.Models
{
    public class UserDetailViewModel
    {
        public string IpAdress { get; set; }
        public bool KvkkIsSign { get; set; }
        public string? ProfilePicture { get; set; }
        public string? ProfileSlider { get; set; }
        public DateTime KvkkAgreeDate { get; set; }
        public int UserId { get; set; }

        public static implicit operator CustomIdentityUser(UserDetailViewModel userDetail)
        {
            return new CustomIdentityUser
            {
                IpAdress = userDetail.IpAdress,
                KvkkIsSign = userDetail.KvkkIsSign,
                KvkkAgreeDate=userDetail.KvkkAgreeDate,
                ProfilePicture=userDetail.ProfilePicture,
                ProfileSlider=userDetail.ProfileSlider,
            };
        }
    }
}
