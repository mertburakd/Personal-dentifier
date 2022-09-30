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
        public PersonalDataDto() { section = new Section(); }
        public string Key { get; set; }
        public string Title { get; set; }
        public Section section { get; set; }
        public bool IsActive { get; set; }
        public string Value { get; set; }
    }
}
