using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Dto
{
    public class PersonalDataDto : IDto
    {
        public PersonalDataDto() { Section = new Section(); }
        public string Key { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Section Section { get; set; }
        public bool IsActive { get; set; }=false;
        public string Data { get; set; }
               
    }
}
