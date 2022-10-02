using Microsoft.AspNetCore.Identity;
using WEBUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace WEBUI.Entities
{
    public class CustomIdentityUser:IdentityUser<int>
    {
        public string IpAdress { get; set; }
        public bool KvkkIsSign { get; set; }
        public DateTime KvkkAgreeDate { get; set; }
        public string? ProfilePicture { get; set; }
        public string? ProfileSlider { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static implicit operator UserDetailViewModel(CustomIdentityUser user)
        {
            return new UserDetailViewModel
            {
                IpAdress = user.IpAdress,
                KvkkIsSign = user.KvkkIsSign,
                KvkkAgreeDate=user.KvkkAgreeDate,
                UserId=user.Id,
                ProfilePicture = user.ProfilePicture,
                ProfileSlider = user.ProfileSlider,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
    }
}
