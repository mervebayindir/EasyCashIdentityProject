using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos
{
    public class AppUserEditDto
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        //Kullanıcı adı güncellenmiyor
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
